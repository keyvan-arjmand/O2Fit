using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Utilities;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Data.Repositories;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using System.IO;
using FoodStuff.Service.Formula;
using Service.v1.Command;
using Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;
using Data.Repositories;
using FoodStuff.Service.v1.Query;
using Microsoft.AspNetCore.Hosting;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Service.v1.Query.GetPersonalFoods;
using FoodStuff.Service.v1.Command.DeletePersonalFoods;

namespace FoodStuff.API.Controllers.v1
{

    [ApiVersion("1")]
    public class PersonalFoodController : BaseController
    {
        private readonly IFoodMeasureUnitRepository _foodMeasurUnitrepository;
        private readonly IPersonalFoodRepository _personalFoodRepository;
        private readonly IPersonalFoodIngredientRepository _personalFoodIngredientRepository;
        private readonly IRepositoryRedis<List<PersonalFoodSelectDTO>> _personalFoodListRepositoryRedis;
        private readonly IRepositoryRedis<PersonalFoodSelectDTO> _personalFoodRepositoryRedis;
        private readonly IWebHostEnvironment _environment;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PersonalFoodController(IMediator mediator, IMapper mapper,
            IPersonalFoodRepository personalFoodRepository,
            IPersonalFoodIngredientRepository personalFoodIngredientRepository,
            IFoodMeasureUnitRepository foodMeasurUnitrepository,
            IRepositoryRedis<List<PersonalFoodSelectDTO>> personalFoodListRepositoryRedis,
            IRepositoryRedis<PersonalFoodSelectDTO> personalFoodRepositoryRedis,
            IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _mediator = mediator;
            _environment = environment;
            _personalFoodRepository = personalFoodRepository;
            _foodMeasurUnitrepository = foodMeasurUnitrepository;
            _personalFoodRepositoryRedis = personalFoodRepositoryRedis;
            _personalFoodListRepositoryRedis = personalFoodListRepositoryRedis;
            _personalFoodIngredientRepository = personalFoodIngredientRepository;
        }

        [HttpPost]
        public virtual async Task<ApiResult<PersonalFoodSelectDTO>> Post(PersonalFoodDTO PersonalFoodDTO,/*IFormFile image,*/CancellationToken cancellationToken)
        {
            PersonalFood foodModel = PersonalFoodDTO.ToEntity(_mapper);
            foodModel._id = PersonalFoodDTO._id;
            foodModel.Name = PersonalFoodDTO.FoodName;
            if (PersonalFoodDTO.Ingredients == null) { return null; }
            var _cal = await _mediator.Send(new GetIngredientCalculateQuery { IngredientModels = PersonalFoodDTO.Ingredients });
            var foodFactors = Formula.FoodWeightsCalculation(new FoodParameters()
            {
                foodWeight = _cal.SumWeight,
                BakingRatio = ((BakingType)PersonalFoodDTO.BakingType).ToDescription().ToDouble(),
                BakingTimeInMinute = PersonalFoodDTO.BakingTime.TotalMinutes,
                foodNutrients = _cal.IngredientCalculate
            });
            var remainedWater = _cal.IngredientCalculate[1] - foodFactors.EvaporatedWater ;
            if (remainedWater>0)
            {
                foodModel.WeightBeforBaking = _cal.SumWeight;
                foodModel.EvaporatedWater = foodFactors.EvaporatedWater;
                foodModel.WeightAfterBaking = foodFactors.AfterBaking;
                foodModel.BakingTime = PersonalFoodDTO.BakingTime;
                List<double> _foodNutrients = new List<double>();
                foreach (var nut in _cal.IngredientCalculate)
                {
                    _foodNutrients.Add(nut * 100 / foodFactors.AfterBaking);
                }
                _foodNutrients[1] = remainedWater * 100 / foodFactors.AfterBaking;
                foodModel.NutrientValue = StringConvertor.DoubleToString(_foodNutrients);
                foodModel.IsCopyable = false;
                if (PersonalFoodDTO.ImageUri != null)
                {
                    var base64EncodedBytes = Convert.FromBase64String(PersonalFoodDTO.ImageUri);

                    string _Path = Path.Combine(_environment.WebRootPath, "PersonalFoodImage");

                    DirectoryInfo destination;

                    if (!Directory.Exists(_Path))
                    {
                        destination = Directory.CreateDirectory(_Path);
                    }
                    else
                    {
                        destination = new DirectoryInfo(_Path);
                    }

                    string _FileName = Guid.NewGuid().ToString() + ".jpg";

                    var _Address = Path.Combine(_Path, _FileName);

                    System.IO.File.WriteAllBytes(_Address, base64EncodedBytes);

                    foodModel.ImageUri = _FileName;
                    foodModel.ThumbUri = _FileName;
                }
                var foodId = await _personalFoodRepository.AddAsync(foodModel, cancellationToken);
                foodModel.Id = foodId;

                //Add PersonalFood Ingredient
                var foodIngList = new List<PersonalFoodIngredient>();
                foreach (var ing in PersonalFoodDTO.Ingredients)
                {
                    var foodIng = new PersonalFoodIngredient()
                    {
                        IngredientId = ing.Id,
                        IngredientValue = ing.Value,
                        MeasureUnitId = ing.MeasureUnitId,
                        PersonalFoodId = foodId
                    };
                    foodIngList.Add(foodIng);
                }
                //---------------------------------------------------------------------------------------------------------
                await _personalFoodIngredientRepository.AddRangeAsync(foodIngList, cancellationToken);
                var foodIngs = await _personalFoodIngredientRepository.GetIngsByFoodIdAsync(foodId,cancellationToken);
                //--------------------------------------Food Ingredients-----------------------------------------
                var ingredients = new List<IngredientAdminModel>();
                foreach (var item in foodIngs)
                {
                    var ing = new IngredientAdminModel()
                    {
                        Id = item.IngredientId,
                        Name = new Domain.Entities.Translation.Translation
                        {
                            Persian = item.Ingredient.Translation.Persian,
                            Arabic = item.Ingredient.Translation.Arabic,
                            English = item.Ingredient.Translation.English,
                        },
                        MeasureUnitId = item.MeasureUnitId,
                        Value = item.IngredientValue,
                        MeasureUnitList = item.Ingredient.IngredientMeasureUnits.Select(m => m.MeasureUnitId).ToList(),
                    };
                    ingredients.Add(ing);
                }
                //-------------------------------------------------------------------------------------------------
                var MeasureUnits = new List<int> { 36, 37, 58 };
                if (foodModel.ParentFoodId > 0)
                {
                    var foodMeasureUnits = await _foodMeasurUnitrepository.GetFoodMeasureUnits(foodModel.ParentFoodId ?? 0, cancellationToken);
                    if (foodMeasureUnits.Count() > 0)
                    {
                        MeasureUnits.AddRange(foodMeasureUnits.Select(m => m.MeasureUnitId).ToList());
                    }
                }
                //-----------------------------------------------------------------------------------------------------
                PersonalFoodSelectDTO personalFood = new PersonalFoodSelectDTO()
                {
                    PersonalFoodId = foodModel.Id,
                    FoodName = foodModel.Name,
                    BakingTime = foodModel.BakingTime,
                    BakingType = (int)foodModel.BakingType,
                    BakingTypeName = foodModel.BakingType.ToString(),
                    ImageUri = (foodModel.ImageUri == null) ?null: Common.CommonStrings.CommonUrl + "PersonalFoodImage/" + foodModel.ImageUri,
                    Recipe = foodModel.Recipe,
                    _id = foodModel._id,
                    MeasureUnits = MeasureUnits,
                    NutrientValue = _foodNutrients,
                    Ingredients = ingredients
                };

               List<PersonalFoodSelectDTO> userPersonalFoods = new List<PersonalFoodSelectDTO>();
                List<PersonalFoodSelectDTO> userPersonalFoodsRedis =await _personalFoodListRepositoryRedis.GetAsync($"UserPersonalFoods_{PersonalFoodDTO.UserId}");
                if (userPersonalFoodsRedis!=null)
                {
                    userPersonalFoods.AddRange(userPersonalFoodsRedis);
                }
                userPersonalFoods.Add(personalFood);
                await _personalFoodListRepositoryRedis.UpdateAsync($"UserPersonalFoods_{PersonalFoodDTO.UserId}", userPersonalFoods);
                await _personalFoodRepositoryRedis.UpdateAsync($"PersonalFood_{personalFood.PersonalFoodId}", personalFood);
               
                return personalFood;
            }
            return null;
        }

        [HttpGet]
        public virtual async Task<ApiResult<PersonalFoodSelectDTO>> Get(int foodId, CancellationToken cancellationToken)
        { 
            return await _mediator.Send(new GetPersonalFoodByIdQuery(){Id= foodId});
        }

        [HttpPost("[action]")]
        public virtual async Task<ApiResult<List<PersonalFoodSelectDTO>>> GetByRengId(List<int> foodIds, CancellationToken cancellationToken)
        {
            List<PersonalFoodSelectDTO> personalFoodList = new List<PersonalFoodSelectDTO>();
            foreach (var foodId in foodIds)
            {
                var food = await _personalFoodRepository.GetByIdAsync(foodId,cancellationToken);
                //--------------------------------------Food Ingredients-----------------------------------------
                var ingredients = new List<IngredientAdminModel>();
                foreach (var item in food.PersonalFoodIngredients)
                {
                    var ing = new IngredientAdminModel()
                    {
                        Id = item.IngredientId,
                        Name = new Domain.Entities.Translation.Translation
                        {
                            Persian = item.Ingredient.Translation.Persian,
                            Arabic = item.Ingredient.Translation.Arabic,
                            English = item.Ingredient.Translation.English,
                        },
                        MeasureUnitId = item.MeasureUnitId,
                        Value = item.IngredientValue,
                        MeasureUnitList = item.Ingredient.IngredientMeasureUnits.Select(m => m.MeasureUnitId).ToList(),
                    };
                    ingredients.Add(ing);
                }
                PersonalFoodSelectDTO personalFood = new PersonalFoodSelectDTO()
                {
                    PersonalFoodId = food.Id,
                    FoodName = food.Name,
                    BakingTime = food.BakingTime,
                    BakingType = (int)food.BakingType,
                    BakingTypeName = food.BakingType.ToString(),
                    ImageUri = (food.ImageUri != null) ? Common.CommonStrings.CommonUrl + "PersonalFoodImage/" + food.ImageUri : null,
                    Recipe = food.Recipe,
                    _id = food._id,
                    MeasureUnits = new List<int> { 36, 37 },
                    NutrientValue = StringConvertor.ToNumber(food.NutrientValue),
                    Ingredients = ingredients
                };
                personalFoodList.Add(personalFood);
            }
            return personalFoodList;
        }

        [HttpGet("[action]")]
        public virtual async Task<ApiResult<List<PersonalFoodSelectDTO>>> GetByUserId(CancellationToken cancellationToken, int userId)
        {
            return await _mediator.Send(new GetPersonalFoodsQuery() {userId=userId });
        }

        [HttpPost("[action]")]
        public async Task<ApiResult<List<double>>> CalculateNutrients(PersonalFoodDTO PersonalFoodDTO, CancellationToken cancellationToken)
        {
            if (PersonalFoodDTO.Ingredients == null) { return null; }
            var _cal = await _mediator.Send(new GetIngredientCalculateQuery { IngredientModels = PersonalFoodDTO.Ingredients });
            var foodFactors = Formula.FoodWeightsCalculation(new FoodParameters()
            {
                foodWeight = _cal.SumWeight,
                BakingRatio = ((BakingType)PersonalFoodDTO.BakingType).ToDescription().ToDouble(),
                BakingTimeInMinute = PersonalFoodDTO.BakingTime.TotalMinutes,
                foodNutrients = _cal.IngredientCalculate
            });
            var remainedWater = _cal.IngredientCalculate[1] - foodFactors.EvaporatedWater;
            if (remainedWater <= 0) { return null; }
            List<double> _foodNutrients = new List<double>();
            foreach (var nut in _cal.IngredientCalculate)
            {
                _foodNutrients.Add(nut * 100 / foodFactors.AfterBaking);
            }
            _foodNutrients[1] = remainedWater*100 / foodFactors.AfterBaking;
            return _foodNutrients;
        }

        [HttpPut("[action]")]
        public async Task<Unit> Delete(int Id,CancellationToken cancellationToken)
        {
          return await _mediator.Send(new DeletePersonalFoodCommand {Id=Id});
        }
        [HttpGet("[action]")]
        public async Task<ApiResult<List<int>>> GetPesonalMeasureUnits(CancellationToken cancellationToken)
        {
            return await Task.Run(() => new List<int>
                    {   36,
                        37,
                        58,
                        1125 ,
                        1124 ,
                        1123 ,
                        1122 ,
                        1121 ,
                        1120 ,
                        1119 ,
                        1118 ,
                        1117 ,
                        1116 ,
                        1115 ,
                        1114 ,
                        1113 ,
                        1112 ,
                        1111 ,
                        1110 ,
                        1109 ,
                        1108 ,
                        1107 ,
                        1106 ,
                        1105 ,
                        1104 ,
                        1103 ,
                        1102 ,
                        1101 ,
                        1100
                    });
        }


        [HttpPut("[action]")]
        public virtual async Task<ApiResult<PersonalFoodSelectDTO>> Put(PersonalFoodDTO PersonalFoodDTO,/*IFormFile image,*/CancellationToken cancellationToken)
        {
            PersonalFood oldFood = await _personalFoodRepository.GetByIdAsync(cancellationToken,PersonalFoodDTO.Id);
            _personalFoodRepository.Detach(oldFood);
            PersonalFood foodModel = PersonalFoodDTO.ToEntity(_mapper);
            foodModel._id = PersonalFoodDTO._id;
            foodModel.Name = PersonalFoodDTO.FoodName;
            if (PersonalFoodDTO.Ingredients == null) { return null; }
            var _cal = await _mediator.Send(new GetIngredientCalculateQuery { IngredientModels = PersonalFoodDTO.Ingredients });
            var foodFactors = Formula.FoodWeightsCalculation(new FoodParameters()
            {
                foodWeight = _cal.SumWeight,
                BakingRatio = ((BakingType)PersonalFoodDTO.BakingType).ToDescription().ToDouble(),
                BakingTimeInMinute = PersonalFoodDTO.BakingTime.TotalMinutes,
                foodNutrients = _cal.IngredientCalculate
            });
            var remainedWater = _cal.IngredientCalculate[1] - foodFactors.EvaporatedWater;
            if (remainedWater > 0)
            {
                foodModel.WeightBeforBaking = _cal.SumWeight;
                foodModel.EvaporatedWater = foodFactors.EvaporatedWater;
                foodModel.WeightAfterBaking = foodFactors.AfterBaking;
                foodModel.BakingTime = PersonalFoodDTO.BakingTime;
                List<double> _foodNutrients = new List<double>();
                foreach (var nut in _cal.IngredientCalculate)
                {
                    _foodNutrients.Add(nut * 100 / foodFactors.AfterBaking);
                }
                _foodNutrients[1] = remainedWater * 100 / foodFactors.AfterBaking;
                foodModel.NutrientValue = StringConvertor.DoubleToString(_foodNutrients);
                foodModel.IsCopyable = false;
                if (PersonalFoodDTO.ImageUri != null)
                {
                   
                    var base64EncodedBytes = Convert.FromBase64String(PersonalFoodDTO.ImageUri);

                    string _Path = Path.Combine(_environment.WebRootPath, "PersonalFoodImage");

                    DirectoryInfo destination;

                    if (!Directory.Exists(_Path))
                    {
                        destination = Directory.CreateDirectory(_Path);
                    }
                    else
                    {
                        destination = new DirectoryInfo(_Path);
                    }
                    if (oldFood.ImageUri != null)
                    {
                        DeleteFile deleteFile = new DeleteFile(_environment);
                        var oldFile = Path.Combine(_Path, oldFood.ImageUri);
                        if (System.IO.File.Exists(oldFile))
                        {
                            deleteFile.DeleteFiles(oldFood.ImageUri, _Path);
                        }
                    }
                    string _FileName = Guid.NewGuid().ToString() + ".jpg";

                    var _Address = Path.Combine(_Path, _FileName);

                    System.IO.File.WriteAllBytes(_Address, base64EncodedBytes);

                    foodModel.ImageUri = _FileName;
                    foodModel.ThumbUri = _FileName;
                }
                foodModel.Id = oldFood.Id;
                await _personalFoodRepository.UpdateAsync(foodModel, cancellationToken);

                //-----------------Add food Ingredient-------------------------------------------------------------------------
                var foodIngList = new List<PersonalFoodIngredient>();
                var deleteIngList = new List<PersonalFoodIngredient>();
                var updatedIngs = new List<int>();
                foreach (var oldIng in oldFood.PersonalFoodIngredients)
                {
                    var existIng = PersonalFoodDTO.Ingredients.Where(i => i.Id == oldIng.IngredientId);
                    if (existIng.Count() == 0)
                    {
                        deleteIngList.Add(oldIng);
                    }
                    else if (existIng.Count() == 1)
                    {
                        var doubleIng = oldFood.PersonalFoodIngredients.Where(i => i.IngredientId == oldIng.IngredientId);
                        if (doubleIng.Count() < 2)
                        {
                            var ExistfoodIng = existIng.FirstOrDefault();
                            oldIng.IngredientValue = ExistfoodIng.Value;
                            oldIng.MeasureUnitId = ExistfoodIng.MeasureUnitId;
                            await _personalFoodIngredientRepository.UpdateAsync(oldIng, cancellationToken);
                            updatedIngs.Add(oldIng.IngredientId);
                        }
                        else
                        {
                            deleteIngList.Add(oldIng);
                        }
                    }
                }
                await _personalFoodIngredientRepository.DeleteRangeAsync(deleteIngList, cancellationToken);
                foreach (var ing in PersonalFoodDTO.Ingredients)
                {
                    if (!updatedIngs.Any(i => i == ing.Id))
                    {

                        var foodIng = new PersonalFoodIngredient()
                        {
                            IngredientId = ing.Id,
                            IngredientValue = ing.Value,
                            MeasureUnitId = ing.MeasureUnitId,
                            PersonalFoodId = oldFood.Id
                        };
                        foodIngList.Add(foodIng);
                    }
                }
                await _personalFoodIngredientRepository.AddRangeAsync(foodIngList, cancellationToken);
                //-----------------------------------------------------------------------------------------------
                var foodIngs = await _personalFoodIngredientRepository.GetIngsByFoodIdAsync(oldFood.Id, cancellationToken);
                //--------------------------------------Food Ingredients-----------------------------------------
                var ingredients = new List<IngredientAdminModel>();
                foreach (var item in foodIngs)
                {
                    var ing = new IngredientAdminModel()
                    {
                        Id = item.IngredientId,
                        Name = new Domain.Entities.Translation.Translation
                        {
                            Persian = item.Ingredient.Translation.Persian,
                            Arabic = item.Ingredient.Translation.Arabic,
                            English = item.Ingredient.Translation.English,
                        },
                        MeasureUnitId = item.MeasureUnitId,
                        Value = item.IngredientValue,
                        MeasureUnitList = item.Ingredient.IngredientMeasureUnits.Select(m => m.MeasureUnitId).ToList(),
                    };
                    ingredients.Add(ing);
                }
                //-------------------------------------------------------------------------------------------------
                var MeasureUnits = new List<int> { 36, 37, 58 };
                if (foodModel.ParentFoodId > 0)
                {
                    var foodMeasureUnits = await _foodMeasurUnitrepository.GetFoodMeasureUnits(foodModel.ParentFoodId ?? 0, cancellationToken);
                    if (foodMeasureUnits.Count() > 0)
                    {
                        MeasureUnits.AddRange(foodMeasureUnits.Select(m => m.MeasureUnitId).ToList());
                    }
                }
                else
                {
                    MeasureUnits = new List<int>
                    {
                        36,
                        37,
                        58,
                        1125 ,
                        1124 ,
                        1123 ,
                        1122 ,
                        1121 ,
                        1120 ,
                        1119 ,
                        1118 ,
                        1117 ,
                        1116 ,
                        1115 ,
                        1114 ,
                        1113 ,
                        1112 ,
                        1111 ,
                        1110 ,
                        1109 ,
                        1108 ,
                        1107 ,
                        1106 ,
                        1105 ,
                        1104 ,
                        1103 ,
                        1102 ,
                        1101 ,
                        1100
                    };
                }
                //-----------------------------------------------------------------------------------------------------
                PersonalFoodSelectDTO personalFood = new PersonalFoodSelectDTO()
                {
                    PersonalFoodId = foodModel.Id,
                    FoodName = foodModel.Name,
                    BakingTime = foodModel.BakingTime,
                    BakingType = (int)foodModel.BakingType,
                    BakingTypeName = foodModel.BakingType.ToString(),
                    ImageUri = (foodModel.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "PersonalFoodImage/" + foodModel.ImageUri,
                    Recipe = foodModel.Recipe,
                    _id = foodModel._id,
                    MeasureUnits = MeasureUnits,
                    NutrientValue = _foodNutrients,
                    Ingredients = ingredients
                };

                List<PersonalFoodSelectDTO> userPersonalFoods = new List<PersonalFoodSelectDTO>();
                List<PersonalFoodSelectDTO> userPersonalFoodsRedis = await _personalFoodListRepositoryRedis.GetAsync($"UserPersonalFoods_{PersonalFoodDTO.UserId}");
                if (userPersonalFoodsRedis != null)
                {
                    var oldPersonalFood = userPersonalFoodsRedis.FirstOrDefault(f=>f.ParentFoodId== personalFood.ParentFoodId);
                    userPersonalFoodsRedis.Remove(oldPersonalFood);
                    userPersonalFoods.AddRange(userPersonalFoodsRedis);
                }
                userPersonalFoods.Add(personalFood);
                await _personalFoodListRepositoryRedis.UpdateAsync($"UserPersonalFoods_{PersonalFoodDTO.UserId}", userPersonalFoods);
                await _personalFoodRepositoryRedis.UpdateAsync($"PersonalFood_{personalFood.PersonalFoodId}", personalFood);

                return personalFood;
            }
            return null;
        }

    }
}
