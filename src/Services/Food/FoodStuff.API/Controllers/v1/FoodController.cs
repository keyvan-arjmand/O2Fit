using AutoMapper;
using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts;
using FoodStuff.API.Models;
using FoodStuff.API.Models.DTOs;
using FoodStuff.API.Models.ViewModels;
using FoodStuff.Common;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using FoodStuff.Domain.Filter;
using FoodStuff.Service.Contracts;
using FoodStuff.Service.CustomPageResult;
using FoodStuff.Service.Formula;
using FoodStuff.Service.Services;
using FoodStuff.Service.v1.Command;
using FoodStuff.Service.v1.Command.DeleteAllFoodRedis;
using FoodStuff.Service.v1.Query;
using FoodStuff.Service.v1.Query.GetVersion;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.v1.Command;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using CategoryViewModel = FoodStuff.Domain.Entities.ViewModels.CategoryViewModel;
using DietCategoryViewModel = FoodStuff.Domain.Entities.ViewModels.DietCategoryViewModel;
using FoodDTO = FoodStuff.API.Models.FoodDTO;
using NationalityViewModel = FoodStuff.Domain.Entities.ViewModels.NationalityViewModel;


namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class FoodController : BaseController
    {
        private readonly IFoodMeasureUnitRepository _foodMeasurUnitrepository;
        private readonly IRepository<MeasureUnit> _measureUnitRepository;
        private readonly IFoodIngredientRepository _foodIngredientRepository;
        private readonly IRepository<IngredientMeasureUnit> _IngMessrepository;
        private readonly IIngeredientRepository _IngredientRepository;
        private readonly ITranslationRepository _translationRepository;
        private readonly IRepository<ExcelTable> _ExcelTable;
        private readonly IBrandRepository _brandRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly IFileService _fileService;
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IRepository<Nationality> _nationalityRepository;

        public FoodController(IFoodMeasureUnitRepository foodMeasurUnitrepository,
            IRepository<MeasureUnit> measureUnitRepository,
            IFoodIngredientRepository foodIngredientRepository,
            IRepository<IngredientMeasureUnit> ingMessrepository,
            IIngeredientRepository ingredientRepository,
            ITranslationRepository translationRepository,
            IRepository<ExcelTable> excelTable,
            IBrandRepository brandRepository,
            IFoodRepository foodRepository, IMediator mediator,
            IMapper mapper, IWebHostEnvironment environment,
            IFileService fileService,
            IRedisCacheClient redisCacheClient, IRepository<Nationality> nationalityRepository)
        {
            _foodMeasurUnitrepository = foodMeasurUnitrepository;
            _measureUnitRepository = measureUnitRepository;
            _foodIngredientRepository = foodIngredientRepository;
            _IngMessrepository = ingMessrepository;
            _IngredientRepository = ingredientRepository;
            _translationRepository = translationRepository;
            _ExcelTable = excelTable;
            _brandRepository = brandRepository;
            _foodRepository = foodRepository;
            _mediator = mediator;
            _mapper = mapper;
            _environment = environment;
            _fileService = fileService;
            _redisCacheClient = redisCacheClient;
            _nationalityRepository = nationalityRepository;
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post(FoodDTO foodDTO, CancellationToken cancellationToken)
        {
            try
            {
                var isExistCode = _foodRepository.TableNoTracking
                    .Any(f => f.FoodCode == foodDTO.FoodCode);
                if (isExistCode)
                    throw new AppException(ApiResultStatusCode.BadRequest, "FoodCode is duplicated");

                var cal = new SelectIngredient();
                if ((FoodType)foodDTO.FoodTypeId == FoodType.Homemade ||
                    (FoodType)foodDTO.FoodTypeId == FoodType.Restaurant)
                {
                    if (!foodDTO.Ingredients.Any() && (FoodType)foodDTO.FoodTypeId != FoodType.Supermarket)
                        throw new AppException(ApiResultStatusCode.BadRequest, "Ingredients Is Required");

                    cal = await _mediator.Send(new GetIngredientCalculateQuery { IngredientModels = foodDTO.Ingredients }, cancellationToken);
                }
                else
                {
                    var isExistBarcode = await _foodRepository.TableNoTracking
                         .Where(f => f.BarcodeGs1 == foodDTO.BarcodeGs1 ||
                         f.BarcodeNational == foodDTO.BarcodeNational)
                         .ToListAsync(cancellationToken);
                    if (isExistBarcode.Any())
                    {
                        return new ApiResult(false, ApiResultStatusCode.BadRequest, "The barcode is duplicated");
                    }

                    cal.SumWeight = 100;
                    cal.IngredientCalculate = foodDTO.Nutrients;
                    foodDTO.BakingTime = new TimeSpan(0);

                }


                var foodName = await _mediator.Send(new CreateTranslationCommand
                {
                    Translation = foodDTO.Name.ToEntity(_mapper)
                }, cancellationToken);

                Translation foodRecipe = new Translation();
                if (foodDTO.FoodTypeId != 3 && (foodDTO.Recipe.Persian != null || foodDTO.Recipe.Arabic != null || foodDTO.Recipe.English != null))
                {

                    foodRecipe = await _mediator.Send(new CreateTranslationCommand
                    {
                        Translation = foodDTO.Recipe.ToEntity(_mapper)
                    });
                }

                var foodId = await _mediator.Send(new CreateFoodCommand
                {
                    NameId = foodName.Id,
                    RecipeId = foodDTO.Recipe == null ? 0 : foodRecipe.Id,

                    IngredientCalculate = cal.IngredientCalculate,
                    SumWeight = cal.SumWeight,

                    NutrientValue = StringConvertor.DoubleToString(foodDTO.Nutrients),
                    BakingType = (BakingType)foodDTO.BakingType,

                    FoodType = (FoodType)foodDTO.FoodTypeId,
                    Tag = foodDTO.Tag == null ? null : foodDTO.Tag.ToLower(),
                    TagArEn = foodDTO.TagArEn == null ? null : foodDTO.TagArEn.ToLower(),
                    IsIngredient = foodDTO.IsIngredient,
                    PersonCount = foodDTO.PersonCount,

                    ImageUri = foodDTO.ImageUri,
                    ImageThumb = foodDTO.ImageThumb,
                    BakingTime = foodDTO.BakingTime,
                    BarcodeGs1 = foodDTO.BarcodeGs1,
                    BarcodeNational = foodDTO.BarcodeNational,
                    BrandId = foodDTO.BrandId,
                    FoodCode = foodDTO.FoodCode,
                    FoodMeals = foodDTO.FoodMealIds,
                    IsActive = foodDTO.IsActive,
                    Version = foodDTO.Version,
                    IsDelete = false,
                    IsUpdate = false,
                    GI = foodDTO.GI,
                    UseInDiet = foodDTO.UseInDiet,
                    DefaultMeasureUnitId = foodDTO.DefaultMeasureUnitId,
                    IsRecipe = foodDTO.IsRecipe,

                }, cancellationToken);


                //=====================Add food MessureUnits=========================

                List<FoodMeasureUnit> foodMeasureList = new List<FoodMeasureUnit>();
                foreach (var item in foodDTO.MeasureUnits)
                {
                    if (item != 36 && item != 37 && item != 58)
                    {
                        FoodMeasureUnit foodMeasureUnit = new FoodMeasureUnit();
                        foodMeasureUnit.FoodId = foodId;
                        foodMeasureUnit.MeasureUnitId = item;
                        foodMeasureList.Add(foodMeasureUnit);

                    }
                }

                await _foodMeasurUnitrepository.AddRangeAsync(foodMeasureList, cancellationToken);


                //========================Add food Ingredient===========================
                var foodIngList = new List<FoodIngredient>();
                if (foodDTO.Ingredients != null)
                {
                    foreach (var ing in foodDTO.Ingredients)
                    {
                        var foodIng = new FoodIngredient
                        {
                            IngredientId = ing.Id,
                            IngredientValue = ing.Value,
                            MeasureUnitId = ing.MeasureUnitId,
                            FoodId = foodId
                        };
                        foodIngList.Add(foodIng);
                    }

                    await _foodIngredientRepository.AddRangeAsync(foodIngList, cancellationToken);
                }


                if (foodDTO.NationalityIds.Any())
                {
                    await _mediator.Send(new CreateFoodNationalityCommand
                    {
                        FoodId = foodId,
                        NationalityIds = foodDTO.NationalityIds
                    }, cancellationToken);
                }

                if (foodDTO.FoodCategoryIds.Any())
                {
                    await _mediator.Send(new CreateFoodCategoryCommand
                    {
                        FoodId = foodId,
                        CategoryIds = foodDTO.FoodCategoryIds
                    }, cancellationToken);
                }

                if (foodDTO.DietCategoryIds.Any())
                {
                    await _mediator.Send(new CreateFoodDietCategoryCommand
                    {
                        FoodId = foodId,
                        FoodDietCategoryIds = foodDTO.DietCategoryIds
                    }, cancellationToken);
                }

                if (foodDTO.FoodHabitIds.Any())
                {
                    await _mediator.Send(new CreateFoodHabitCommand
                    {
                        FoodId = foodId,
                        FoodHabit = foodDTO.FoodHabitIds
                    });
                }

                if (foodDTO.SpecialDiseases.Any())
                {
                    await _mediator.Send(new CreateFoodSpecialDiseasesCommand
                    {
                        FoodId = foodId,
                        FoodSpecialDiseases = foodDTO.SpecialDiseases
                    });
                }


                return new ApiResult(true, ApiResultStatusCode.Success);
            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError, e.Message);
            }

        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put(UpdateFoodAdminDTO foodDTO, CancellationToken cancellationToken)
        {
            try
            {

                if (string.IsNullOrEmpty(foodDTO.Id.ToString()) || foodDTO.Id == 0)
                {
                    throw new AppException(ApiResultStatusCode.BadRequest, "food Id Not Valid");
                }

                var oldFood = await _foodRepository.GetByIdAsync(cancellationToken, foodDTO.Id);

                if (oldFood == null)
                    return new ApiResult(false, ApiResultStatusCode.NotFound);


                // var name = await _mediator.Send(new GetTranslationByIdQuery
                // {
                //     Id = oldFood.NameId,
                // }, cancellationToken);

                //---------------------------------------Add file-----------------------------------------------------------
                if (!string.IsNullOrEmpty(oldFood.ImageUri))
                {
                    _fileService.RemoveImage(oldFood.ImageUri, "FoodImage");
                }


                if (!string.IsNullOrEmpty(foodDTO.ImageUri))
                {
                    var imageName = _fileService.AddImage(foodDTO.ImageUri, "FoodImage", foodDTO.FoodCode.ToString());
                    oldFood.ImageUri = imageName;
                }


                if (!string.IsNullOrEmpty(oldFood.ImageThumb))
                {
                    _fileService.RemoveImage(oldFood.ImageThumb, "FoodThumb");
                }

                if (!string.IsNullOrEmpty(foodDTO.ImageThumb))
                {
                    var imageThumb = _fileService.AddImage(foodDTO.ImageThumb, "FoodThumb", foodDTO.FoodCode.ToString() + "-2");
                    oldFood.ImageThumb = imageThumb;
                }


                _translationRepository.Detach(oldFood.TranslationName);

                await _mediator.Send(new UpdateTranslationCommand
                {
                    Translation = new Translation
                    {
                        Id = oldFood.NameId,
                        Arabic = foodDTO.Name.Arabic,
                        English = foodDTO.Name.English,
                        Persian = foodDTO.Name.Persian
                    }
                }, cancellationToken);

                if (oldFood.RecipeId > 0)
                {
                    _translationRepository.Detach(oldFood.TranslationRecipe);

                    if (foodDTO.Recipe != null)
                    {
                        await _mediator.Send(new UpdateTranslationCommand
                        {
                            Translation = foodDTO.Recipe.ToEntity(_mapper)
                        });
                    }
                    else
                    {
                        List<int> ids = new List<int>();
                        ids.Add(oldFood.RecipeId ?? 0);

                        await _mediator.Send(new DeleteTranslationCommand
                        {
                            Ids = ids
                        });
                    }
                }
                else if (foodDTO.Recipe != null)
                {
                    var recipe = await _mediator.Send(new CreateTranslationCommand
                    {
                        Translation = foodDTO.Recipe.ToEntity(_mapper)
                    });
                    oldFood.RecipeId = recipe.Id;
                }

                var cal = new SelectIngredient();
                if ((FoodType)foodDTO.FoodType == FoodType.Homemade ||
                    (FoodType)foodDTO.FoodType == FoodType.Restaurant)
                {
                    if (foodDTO.Ingredients == null)
                    {
                        return null;
                    }

                    List<IngMeasurModel> ingMeasurModels = foodDTO.Ingredients
                        .Select(i => new IngMeasurModel()
                        {
                            Id = i.Id,
                            Value = i.Value,
                            MeasureUnitId = i.MeasureUnitId
                        }).ToList();
                    cal = await _mediator.Send(new GetIngredientCalculateQuery
                    {
                        IngredientModels = ingMeasurModels
                    });
                }
                else
                {
                    cal.SumWeight = 100;
                    cal.IngredientCalculate = foodDTO.NutrientValue;
                    foodDTO.BakingTime = new TimeSpan(0);
                }



                List<double> foodNutrients = new List<double>();

                var foodFactors = Formula.FoodWeightsCalculation(new FoodParameters()
                {
                    foodWeight = cal.SumWeight,
                    BakingRatio = ((BakingType)foodDTO.BakingType).ToDescription().ToDouble(),
                    BakingTimeInMinute = foodDTO.BakingTime.TotalMinutes,
                    foodNutrients = cal.IngredientCalculate
                });

                if (foodFactors.AfterBaking - foodFactors.DryIngredient <= 0
                    && (FoodType)foodDTO.FoodType != FoodType.Supermarket &&
                    (BakingType)foodDTO.BakingType != BakingType.NoTypeOfBaking)
                    throw new AppException(ApiResultStatusCode.BadRequest, "Your food is burnt");



                foreach (var nut in cal.IngredientCalculate)
                {
                    foodNutrients.Add(nut * 100 / foodFactors.AfterBaking);
                }

                foodNutrients[1] = (foodFactors.AfterBaking - foodFactors.DryIngredient) * 100 /
                                   foodFactors.AfterBaking;

                if (foodNutrients[1] <= 0 &&
                    (FoodType)foodDTO.FoodType != FoodType.Supermarket &&
                    (BakingType)foodDTO.BakingType != BakingType.NoTypeOfBaking)
                {
                    throw new AppException(ApiResultStatusCode.BadRequest, "Your food is burnt");
                }

                if ((FoodType)foodDTO.FoodType == FoodType.Supermarket)
                {
                    foodNutrients = foodDTO.NutrientValue;
                }



                oldFood.WeightBeforBaking = cal.SumWeight;
                oldFood.EvaporatedWater = foodFactors.EvaporatedWater;
                oldFood.WeightAfterBaking = foodFactors.AfterBaking;
                oldFood.BakingTime = foodDTO.BakingTime;
                oldFood.DryIngredient = foodFactors.DryIngredient;

                oldFood.NutrientValue = StringConvertor.DoubleToString(foodNutrients);
                oldFood.Version = foodDTO.Version;
                oldFood.BakingType = (BakingType)foodDTO.BakingType;

                oldFood.FoodType = (FoodType)foodDTO.FoodType;
                if (foodDTO.BrandId > 0)
                {
                    oldFood.BrandId = foodDTO.BrandId;
                }

                oldFood.BarcodeGs1 = foodDTO.BarcodeGs1;
                oldFood.BarcodeNational = foodDTO.BarcodeNational;
                oldFood.IsActive = foodDTO.IsActive;
                oldFood.FoodCode = foodDTO.FoodCode;
                oldFood.PersonCount = foodDTO.PersonCount;
                oldFood.Tag = foodDTO.Tag.ToLower();
                oldFood.TagArEn = foodDTO.TagArEn == null ? null : foodDTO.TagArEn.ToLower();
                oldFood.IsIngredient = foodDTO.IsIngredient;
                oldFood.IsActive = foodDTO.IsActive;
                oldFood.IsDelete = false;
                oldFood.IsUpdate = true;
                oldFood.GI = foodDTO.GI;
                oldFood.UseInDiet = foodDTO.UseInDiet;
                oldFood.DefaultMeasureUnitId = foodDTO.DefaultMeasureUnitId;
                oldFood.FoodMeals = foodDTO.FoodMealIds;
                oldFood.IsRecipe = foodDTO.IsRecipe;

                await _foodRepository.UpdateAsync(oldFood, cancellationToken);



                await _mediator.Send(new UpdateFoodCategoryCommand
                {
                    FoodCategoryIds = foodDTO.FoodCategoryIds,
                    FoodId = foodDTO.Id,
                    PastFoodCategories = oldFood.FoodCategories.ToList()
                }, cancellationToken);



                await _mediator.Send(new UpdateFoodNationalityCommand
                {
                    FoodId = foodDTO.Id,
                    NationalityIds = foodDTO.NationalityIds,
                    FoodNationalities = oldFood.FoodNationalities.ToList()
                }, cancellationToken);

                await _mediator.Send(new UpdateFoodHabitCommand
                {
                    FoodId = foodDTO.Id,
                    FoodHabitIds = foodDTO.FoodHabitIds,
                    FoodFoodHabits = oldFood.FoodHabits.ToList()
                }, cancellationToken);

                await _mediator.Send(new UpdateFoodDietCategoryCommand
                {
                    FoodId = oldFood.Id,
                    DietCategoryIds = foodDTO.DietCategoryIds,
                    FoodDietCategories = oldFood.FoodDietCategories.ToList()
                }, cancellationToken);

                await _mediator.Send(new UpdateFoodSpecialDiseasesCommand
                {
                    FoodId = oldFood.Id,
                    OldSpecialDiseases = oldFood.FoodSpecialDiseases.ToList(),
                    SpecialDiseases = foodDTO.SpecialDiseases
                }, cancellationToken);

                //----------------------Add food MessureUnits---------------------------------------------------------------
                List<FoodMeasureUnit> foodMeasurelist = new List<FoodMeasureUnit>();
                if (oldFood.FoodMeasureUnits.Any())
                {
                    //var foodMeasureunitIds = await _foodMeasurUnitrepository.GetFoodMeasureUnits(oldFood.Id, cancellationToken);
                    var oldMeasureUnits = oldFood.FoodMeasureUnits.Select(m => m.MeasureUnitId);
                    var deleteMeasureUnits = new List<FoodMeasureUnit>();
                    foreach (var oldMeasureunit in oldFood.FoodMeasureUnits)
                    {
                        if (foodDTO.MeasureUnits.All(m => m != oldMeasureunit.MeasureUnitId))
                        {
                            deleteMeasureUnits.Add(oldMeasureunit);
                        }
                    }
                    await _foodMeasurUnitrepository.DeleteRangeAsync(deleteMeasureUnits, cancellationToken);
                    foreach (var item in foodDTO.MeasureUnits)
                    {
                        if (!oldMeasureUnits.Any(m => m == item) && item != 36 && item != 37 && item != 58)
                        {
                            FoodMeasureUnit foodMeasureUnit = new FoodMeasureUnit();
                            foodMeasureUnit.FoodId = oldFood.Id;
                            foodMeasureUnit.MeasureUnitId = item;
                            foodMeasurelist.Add(foodMeasureUnit);
                        }
                    }

                    foodMeasurelist = foodMeasurelist.Distinct().ToList();
                    await _foodMeasurUnitrepository.AddRangeAsync(foodMeasurelist, cancellationToken);
                }
                else
                {
                    //---------------------------------------------------------------------------------------
                    foreach (var item in foodDTO.MeasureUnits)
                    {
                        if (item != 36 && item != 37 && item != 58)
                        {
                            FoodMeasureUnit foodMeasureUnit = new FoodMeasureUnit();
                            foodMeasureUnit.FoodId = oldFood.Id;
                            foodMeasureUnit.MeasureUnitId = item;
                            foodMeasurelist.Add(foodMeasureUnit);
                        }
                    }
                    foodMeasurelist = foodMeasurelist.Distinct().ToList();
                    await _foodMeasurUnitrepository.AddRangeAsync(foodMeasurelist, cancellationToken);
                }
                //-----------------Add food Ingredient-------------------------------------------------------------------------
                var foodIngList = new List<FoodIngredient>();
                var deleteIngList = new List<FoodIngredient>();
                var updatedIngs = new List<int>();
                foreach (var oldIng in oldFood.FoodIngredients)
                {
                    var existIng = foodDTO.Ingredients
                        .Where(i => i.Id == oldIng.IngredientId);
                    if (!existIng.Any())
                    {
                        deleteIngList.Add(oldIng);
                    }
                    else if (existIng.Count() == 1)
                    {
                        var doubleIng = oldFood.FoodIngredients
                            .Where(i => i.IngredientId == oldIng.IngredientId);
                        if (doubleIng.Count() < 2)
                        {
                            var ExistfoodIng = existIng.FirstOrDefault();
                            oldIng.IngredientValue = ExistfoodIng.Value;
                            oldIng.MeasureUnitId = ExistfoodIng.MeasureUnitId;
                            await _foodIngredientRepository.UpdateAsync(oldIng, cancellationToken);
                            updatedIngs.Add(oldIng.IngredientId);
                        }
                        else
                        {
                            deleteIngList.Add(oldIng);
                        }
                    }
                }
                await _foodIngredientRepository.DeleteRangeAsync(deleteIngList, cancellationToken);
                foreach (var ing in foodDTO.Ingredients)
                {
                    if (updatedIngs.All(i => i != ing.Id))
                    {
                        var foodIng = new FoodIngredient
                        {
                            IngredientId = ing.Id,
                            IngredientValue = ing.Value,
                            MeasureUnitId = ing.MeasureUnitId,
                            FoodId = oldFood.Id
                        };
                        foodIngList.Add(foodIng);
                    }
                }
                await _foodIngredientRepository.AddRangeAsync(foodIngList, cancellationToken);


                if (await _redisCacheClient.Db2.ExistsAsync($"food_{oldFood.Id}"))
                {
                    await _redisCacheClient.Db2.RemoveAsync($"food_{oldFood.Id}");
                }
                if (await _redisCacheClient.Db7.ExistsAsync($"GetFood_V2_{oldFood.Id}"))
                {
                    await _redisCacheClient.Db7.RemoveAsync($"GetFood_V2_{oldFood.Id}");
                }

                return Ok();
            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError, e.Message);
            }
        }


        [HttpGet("FoodTypes")]
        public IActionResult FoodTypes()
        {
            return Ok(EnumExtensions.GetEnumNameValues<FoodType>());
        }

        [HttpPost("OfflineMode")]
        public IActionResult OfflineMode(FastOfflineInputCommand fastOfflineInputCommand)
        {
            return Ok(true);
        }

        [AllowAnonymous]
        [HttpGet("Search")]
        public async Task<PageResult<FoodResult>> Search(int? Page, int PageSize, [FromQuery] FoodInputParameters foodInputParameters, CancellationToken cancellationToken)
        {
            PageResult<FoodResult> result = new PageResult<FoodResult>();
            if (!string.IsNullOrEmpty(foodInputParameters.Name))
            {
                result = await _mediator.Send(new GetFoodQuery
                {
                    foodInputParameters = foodInputParameters,
                    LanguageName = LanguageName,
                    Page = Page,
                    PageSize = PageSize
                }, cancellationToken);

            }
            else
            {
                result = await _mediator.Send(new GetFoodByBarcodeQuery
                {
                    foodInputParameters = foodInputParameters,
                    LanguageName = LanguageName,
                    Page = Page,
                    PageSize = PageSize
                }, cancellationToken);
            }
            return result;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("SearchAdmin")]
        public async Task<PageResult<FoodResultAdmin>> SearchAdmin(int? Page, int PageSize, [FromQuery] FoodInputParameters foodInputParameters, CancellationToken cancellationToken)
        {
            PageResult<FoodResultAdmin> result = new PageResult<FoodResultAdmin>();
            if (!string.IsNullOrEmpty(foodInputParameters.Name))
            {
                result = await _mediator.Send(new GetFoodAdminQuery
                {
                    foodInputParameters = foodInputParameters,
                    LanguageName = LanguageName,
                    Page = Page,
                    PageSize = PageSize
                }, cancellationToken);

            }

            return result;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("SearchFoodCode")]
        public async Task<PageResult<FoodResultFoodCode>> SearchFoodCode(long foodCode, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(new GetFoodByCodeQuery
            {
                FoodCode = foodCode
            }, cancellationToken);

            return result;

        }

        [HttpGet]
        public async Task<ApiResult<FoodViewModel>> Get(int foodId, CancellationToken cancellationToken)
        {
            Food food;
            FoodViewModel foodViewModel;

            if (await _redisCacheClient.Db2.ExistsAsync($"food_{foodId}"))
            {
                foodViewModel = await _redisCacheClient.Db2.GetAsync<FoodViewModel>($"food_{foodId}");
            }
            else
            {
                food = await _foodRepository.GetByIdAsync(cancellationToken, foodId);

                //var foodCategory = _
                //--------------------------------------Food Ingredients-----------------------------------------
                var ingredients = new List<IngredientAdminModel>();
                foreach (var item in food.FoodIngredients)
                {
                    var ing = new IngredientAdminModel
                    {
                        Id = item.IngredientId,
                        Name = new Translation
                        {
                            Persian = item.Ingredient.Translation.Persian,
                            Arabic = item.Ingredient.Translation.Arabic,
                            English = item.Ingredient.Translation.English
                        },
                        MeasureUnitId = item.MeasureUnitId,
                        Value = item.IngredientValue,
                        MeasureUnitList = item.Ingredient.IngredientMeasureUnits.Select(m => m.MeasureUnitId).Distinct().ToList(),
                    };
                    ingredients.Add(ing);
                }

                List<int> measureUnits = new List<int> { 36, 37, 58 };
                if (food.FoodMeasureUnits.Any())
                {
                    List<int> extraMeasureUnits = food.FoodMeasureUnits
                        .Select(m => m.MeasureUnitId).Distinct().ToList();
                    measureUnits.AddRange(extraMeasureUnits);
                }


                var foodViewModelResult = new FoodViewModel
                {
                    FoodId = food.Id,
                    _id = food.Id.ToString(),
                    Name = new TranslationViewModel()
                    {
                        Persian = food.TranslationName.Persian,
                        English = food.TranslationName.English,
                        Arabic = food.TranslationName.Arabic
                    },
                    Recipe = new TranslationViewModel()
                    {
                        Persian = food.TranslationRecipe?.Persian,
                        English = food.TranslationRecipe?.English,
                        Arabic = food.TranslationRecipe?.Arabic
                    },
                    PersonCount = food.PersonCount > 0 ? food.PersonCount : 1,
                    BakingType = food.BakingType,
                    FoodType = food.FoodType,
                    ImageUri = food.ImageUri,
                    ImageThumb = food.ImageThumb,
                    NutrientValue = StringConvertor.ToNumber(food.NutrientValue),
                    Brand = (food.BrandId > 0) ? new TranslationViewModel()
                    {
                        Arabic = food.Brand.Translation.Arabic,
                        English = food.Brand.Translation.English,
                        Persian = food.Brand.Translation.Persian
                    } : null,
                    IsUpdate = food.IsUpdate,
                    Version = food.Version,
                    BakingTime = food.BakingTime,
                    BarcodeGs1 = food.BarcodeGs1,
                    BarcodeNational = food.BarcodeNational,
                    MeasureUnits = measureUnits,
                    Ingredients = ingredients,
                    FoodMeals = food.FoodMeals,
                    GI = food.GI,
                    FoodHabitIds = food.FoodHabits.Select(s => s.FoodHabit).ToList(),
                    DefaultMeasureUnitId = food.DefaultMeasureUnitId,
                };

                foodViewModel = foodViewModelResult;
                await _redisCacheClient.Db2.AddAsync($"food_{food.Id}", foodViewModel);
            }

            return new ApiResult<FoodViewModel>(true, ApiResultStatusCode.Success, foodViewModel);
        }

        //محاسبه مواد مغذی های غذا برای پک های رژیم
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public ApiResult<List<double>> CalculateFoodsNutrients()
        {
            var json = Request.Form;
            var _foodsDTO = Request.Form["JsonDetails"];
            List<IngMeasurModel> foods = JsonConvert.DeserializeObject<List<IngMeasurModel>>(_foodsDTO);

            List<double> Nutrients = new List<double>();
            foreach (var item in foods)
            {
                var food = _foodRepository.TableNoTracking.FirstOrDefault(f => f.Id == item.Id);
                var foodNutrients = StringConvertor.ToNumber(food.NutrientValue);
                var measurValue = _measureUnitRepository.TableNoTracking.FirstOrDefault(m => m.Id == item.MeasureUnitId);
                if (Nutrients.Count() > 0)
                {
                    for (int i = 0; i < foodNutrients.Count(); i++)
                    {
                        Nutrients[i] = Nutrients[i] + ((foodNutrients[i] * measurValue.Value * item.Value) / 100);
                    }
                }
                else
                {
                    for (int i = 0; i < foodNutrients.Count(); i++)
                    {
                        foodNutrients[i] = (foodNutrients[i] * measurValue.Value * item.Value) / 100;
                    }
                    Nutrients = foodNutrients;
                }
            }
            return Nutrients;
        }

        [HttpGet("GetMeasurunitsByFoodId")]
        public async Task<List<MeasureUnitModelDTO>> GetMeasurunitsByFoodIdAsync(int Id, CancellationToken cancellationToken)
        {
            //var listIng = repository.Table;

            var Ing = await _foodRepository.Table.Include(a => a.FoodMeasureUnits)
                                           .ThenInclude(a => a.MeasureUnit).ThenInclude(t => t.Translation)
                                           .Where(i => i.Id == Id).FirstAsync(cancellationToken);
            List<MeasureUnitModelDTO> MessureUnits = Ing.FoodMeasureUnits.Select(m => new MeasureUnitModelDTO()
            {
                Id = m.MeasureUnit.Id,
                Persian = m.MeasureUnit.Translation.Persian,
                Value = m.MeasureUnit.Value
            }).ToList();
            MessureUnits.Add(new MeasureUnitModelDTO { Id = 36, Value = 1, Persian = "گرم" });
            MessureUnits.Add(new MeasureUnitModelDTO { Id = 37, Value = 28.35, Persian = "اونس" });
            MessureUnits.Add(new MeasureUnitModelDTO { Id = 58, Value = 453.6, Persian = "پوند" });
            return MessureUnits;
        }


        [HttpGet("GetFullFood")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<FoodAdminViewModel>> GetById(int foodId, CancellationToken cancellationToken)
        {
            var food = await _foodRepository.GetByIdAsync(cancellationToken, foodId);
            //--------------------------------------Food Ingredients-----------------------------------------
            var ingredients = new List<IngredientAdminSelectDTO>();
            foreach (var item in food.FoodIngredients)
            {
                try
                {
                    var ing = new IngredientAdminSelectDTO
                    {
                        Id = item.IngredientId,
                        Name = new TranslationDto
                        {
                            Persian = item.Ingredient.Translation.Persian,
                            Arabic = item.Ingredient.Translation.Arabic,
                            English = item.Ingredient.Translation.English,
                        },
                        MessureUnit = new MeasureUnitModelDTO()
                        {
                            Id = item.MeasureUnitId,
                            Persian = item.MeasureUnit.Translation.Persian,
                            English = item.MeasureUnit.Translation.English,
                            Arabic = item.MeasureUnit.Translation.Arabic,
                            MeasureUnitCategory = item.MeasureUnit.MeasureUnitCategory,
                            Value = item.MeasureUnit.Value
                        },
                        Value = item.IngredientValue,
                        Code = item.Ingredient.Code,
                        NutrientValue = StringConvertor.ToNumber(item.Ingredient.NutrientValue),
                        ThumbUri = CommonStrings.CommonUrl + "Ingimg/" + item.Ingredient.ThumbUri,
                    };
                    ingredients.Add(ing);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            //-------------------------------------Food MeasureUnits------------------------------------------
            List<int> measureUnits = new List<int> { 36, 37, 58 };
            if (food.FoodMeasureUnits.Any())
            {
                List<int> extraMeasureUnits = food.FoodMeasureUnits.Select(m => m.MeasureUnitId).ToList();
                measureUnits.AddRange(extraMeasureUnits);
            }


            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            if (food.FoodCategories.Any())
            {
                foreach (var foodCategory in food.FoodCategories)
                {
                    categories.Add(new CategoryViewModel
                    {
                        Id = foodCategory.Category.Id,
                        NameTranslation = foodCategory.Category.NameTranslation,
                        ParentId = foodCategory.Category.ParentId,
                        Percent = foodCategory.Category.Percent
                    });
                }
            }

            List<NationalityViewModel> nationalities = new List<NationalityViewModel>();
            if (food.FoodNationalities.Any())
            {
                foreach (var foodNationality in food.FoodNationalities)
                {
                    nationalities.Add(new NationalityViewModel
                    {
                        Id = foodNationality.Nationality.Id,
                        NameTranslation = foodNationality.Nationality.NameTranslation,
                        ParentId = foodNationality.Nationality.ParentId
                    });
                }

            }



            List<DietCategoryViewModel> dietCategories = new List<DietCategoryViewModel>();

            if (food.FoodDietCategories.Any())
            {
                foreach (var foodDietCategories in food.FoodDietCategories)
                {
                    dietCategories.Add(new DietCategoryViewModel
                    {
                        Id = foodDietCategories.DietCategory.Id,
                        NameTranslation = foodDietCategories.DietCategory.NameTranslation,
                        DescriptionTranslation = foodDietCategories.DietCategory.DescriptionTranslation,
                        ParentId = foodDietCategories.DietCategory.ParentId,
                        Image = foodDietCategories.DietCategory.Image
                    });
                }
            }
            if (!string.IsNullOrEmpty(food.ImageUri))
            {
                food.ImageUri = await ConvertImage.GetImageAsBase64Url("FoodImage/" + food.ImageUri);
            }
            if (!string.IsNullOrEmpty(food.ImageThumb))
            {
                food.ImageThumb = await ConvertImage.GetImageAsBase64Url("FoodThumb/" + food.ImageThumb);
            }
            var Food = new FoodAdminViewModel()
            {
                Id = food.Id,
                _id = food.Id.ToString(),
                Tag = food.Tag,
                TagArEn = food.TagArEn,
                Name = new TranslationDto
                {
                    Id = food.NameId,
                    Persian = food.TranslationName.Persian,
                    English = food.TranslationName.English,
                    Arabic = food.TranslationName.Arabic,
                },
                Recipe = food.RecipeId != null ? new TranslationDto
                {
                    Id = food.RecipeId ?? 0,
                    Persian = food.TranslationRecipe.Persian,
                    English = food.TranslationRecipe.English,
                    Arabic = food.TranslationRecipe.Arabic,
                } : null,
                FoodCode = food.FoodCode,
                FoodHabitIds = food.FoodHabits.Select(x => x.FoodHabit).ToList(),
                BakingType = food.BakingType,
                FoodType = food.FoodType,
                ImageUri = food.ImageUri,
                ImageThumb = food.ImageThumb,
                PersonCount = food.PersonCount,
                NutrientValue = StringConvertor.ToNumber(food.NutrientValue),
                Brand = (food.BrandId > 0) ? new BrandSelectAdminDTO()
                {
                    Id = food.BrandId ?? 0,
                    Name = new Translation
                    {
                        Id = food.Brand.NameId,
                        Arabic = food.Brand.Translation.Arabic,
                        English = food.Brand.Translation.English,
                        Persian = food.Brand.Translation.Persian
                    }
                } : null,
                IsUpdate = food.IsUpdate,
                Version = food.Version,
                BakingTime = food.BakingTime,
                BarcodeGs1 = food.BarcodeGs1,
                BarcodeNational = food.BarcodeNational,
                MeasureUnits = measureUnits,
                Ingredients = ingredients,
                IsIngredient = food.IsIngredient,
                FoodMealIds = food.FoodMeals,
                Nationalities = nationalities,
                DietCategories = dietCategories,
                Categories = categories,
                IsActive = food.IsActive,
                GI = food.GI,
                UseInDiet = food.UseInDiet,
                SpecialDiseases = food.FoodSpecialDiseases.Select(x => x.SpecialDisease).ToList(),
                DefaultMeasureUnitId = food.DefaultMeasureUnitId,
                IsRecipe = food.IsRecipe,
            };
            return Food;
        }

        [HttpPut("ActiveFoods")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> ActiveFoods(CancellationToken cancellationToken)
        {
            var deActivedFoods = await _foodRepository.TableNoTracking.Where(f => f.IsActive == false).ToListAsync(cancellationToken);
            List<Food> foods = new List<Food>();
            foreach (var food in deActivedFoods)
            {
                food.IsActive = true;
                foods.Add(food);
            }
            await _foodRepository.UpdateRangeAsync(foods, cancellationToken);
            if (foods.Count() > 0)
            {
                double version = foods.Max(v => v.Version);
                if (version > 0)
                {
                    await _mediator.Send(new GetVersionQuery()
                    {
                        lastVersion = version - 0.01
                    });
                }
            }
            return Ok();
        }

        [HttpPut("UpdateRedisFoodVersions")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> UpdateRedisFoodVersionss(CancellationToken cancellationToken)
        {
            double version = _foodRepository.TableNoTracking.Max(v => v.Version);

            if (version > 0)
            {
                for (int i = 0; i <= version * 100; ++i)
                {
                    double v = i / 100;
                    await _mediator.Send(new GetVersionQuery()
                    {
                        lastVersion = v
                    });
                }
            }
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {

            await _mediator.Send(new DeleteFoodQuery
            {
                Id = id
            });

            return Ok();
        }

        [HttpPut("IsDelete")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> IsDelete(int Id, CancellationToken cancellationToken)
        {
            Food food = await _foodRepository.GetByIdAsync(cancellationToken, Id);
            food.IsDelete = true;
            await _foodRepository.UpdateAsync(food, cancellationToken);
            return Ok();
        }

        [HttpGet("GetVersion")]
        public async Task<ApiResult<FoodsVersionModel>> GetAll(double lastVersion, CancellationToken cancellationToken)
        {
            List<FoodViewModel> foodList = new List<FoodViewModel>();
            FoodsVersionModel foods = new FoodsVersionModel()
            {
                Foods = foodList,
                Version = lastVersion
            };
            var _foods = await _mediator.Send(new GetRedisFoodsVersionQuery()
            {
                lastVersion = lastVersion
            });
            foods = (_foods == null) ? foods : _foods;
            return foods;
        }

        [HttpGet("GetByPaging")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<FoodViewModel>> GetByPaging(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var _foodList = await _foodRepository.Table
                .Include(ft => ft.TranslationName)
                .Include(fr => fr.TranslationRecipe)
                .Include(fi => fi.FoodIngredients)
                .ThenInclude(i => i.Ingredient)
                .ThenInclude(t => t.Translation)
                .Include(f => f.FoodIngredients)
                .ThenInclude(fi => fi.Ingredient)
                .ThenInclude(im => im.IngredientMeasureUnits)
                .Include(fm => fm.FoodMeasureUnits)
                .ThenInclude(m => m.MeasureUnit).ThenInclude(mt => mt.Translation)
                .Include(fb => fb.Brand).ThenInclude(b => b.Translation)
                .OrderBy(o => o.Id).Where(f => (int)f.FoodType == 1)
                .Skip((Page - 1 ?? 0) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
            var countDetails = _foodList.Count();
            List<FoodViewModel> _paging = new List<FoodViewModel>();

            foreach (var food in _foodList)
            {
                //--------------------------------------Food Ingredients-----------------------------------------
                var ingredients = new List<IngredientAdminModel>();
                foreach (var item in food.FoodIngredients)
                {
                    var ing = new IngredientAdminModel()
                    {
                        Id = item.IngredientId,
                        Name = new Translation
                        {
                            Persian = item.Ingredient.Translation.Persian,
                            Arabic = item.Ingredient.Translation.Arabic,
                            English = item.Ingredient.Translation.English,
                        },
                        MeasureUnitId = item.MeasureUnitId,
                        Value = item.IngredientValue,
                        MeasureUnitList = item.Ingredient.IngredientMeasureUnits.Select(m => m.MeasureUnitId).Distinct().ToList(),
                    };
                    ingredients.Add(ing);
                }
                //-------------------------------------Food MeasureUnits------------------------------------------
                List<int> measureUnits = new List<int> { 36, 37, 58 };
                if (food.FoodMeasureUnits.Count() > 0)
                {
                    List<int> extraMeasureUnits = food.FoodMeasureUnits.Select(m => m.MeasureUnitId).ToList();
                    measureUnits.AddRange(extraMeasureUnits);
                }
                measureUnits = measureUnits.Distinct().ToList();
                //-------------------------------------------------------------------------------------------------
                var test = StringConvertor.ToNumber(food.NutrientValue);
                var Food = new FoodViewModel()
                {
                    FoodId = food.Id,
                    _id = food.Id.ToString(),
                    Name = new TranslationViewModel()
                    {
                        Persian = food.TranslationName.Persian,
                        English = food.TranslationName.English,
                        Arabic = food.TranslationName.Arabic,
                    },
                    BakingType = food.BakingType,
                    FoodType = food.FoodType,
                    ImageUri = food.ImageUri,       //(food.ImageUri!=null) ? CommonStrings.CommonUrl + "FoodImage/" + food.ImageUri:null,
                    //ImageThumb = food.ImageThumb,   //(food.ImageThumb!=null)? CommonStrings.CommonUrl + "FoodImage/" + food.ImageThumb:null,
                    NutrientValue = StringConvertor.ToNumber(food.NutrientValue),
                    Brand = (food.BrandId > 0) ? new TranslationViewModel()
                    {
                        Arabic = food.Brand.Translation.Arabic,
                        English = food.Brand.Translation.English,
                        Persian = food.Brand.Translation.Persian
                    } : null,
                    IsUpdate = food.IsUpdate,
                    Version = food.Version,
                    BakingTime = food.BakingTime,
                    BarcodeGs1 = food.BarcodeGs1,
                    BarcodeNational = food.BarcodeNational,
                    MeasureUnits = measureUnits,
                    Ingredients = ingredients,
                    PersonCount = food.PersonCount
                };
                _paging.Add(Food);
            }

            var result = new PageResult<FoodViewModel>()
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };
            return result;

        }

        [HttpGet("GetAllFoodsAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<GetAllFoodsAdminViewModel>> GetAllAdmin(int page, int pageSize, CancellationToken cancellationToken)
        {
            List<GetAllFoodsAdminViewModel> foodsList = await _foodRepository.TableNoTracking
                .Include(ft => ft.TranslationName)
                .OrderByDescending(o => o.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new GetAllFoodsAdminViewModel
                {
                    FoodId = s.Id,
                    Name = new TranslationDto
                    {
                        Persian = s.TranslationName.Persian,
                        Arabic = s.TranslationName.Arabic,
                        English = s.TranslationName.English
                    },
                    IsActive = s.IsActive,
                    Code = s.FoodCode
                })
                .ToListAsync(cancellationToken);

            int activeFoodCount = _foodRepository.TableNoTracking.Count(f => f.IsActive);
            int foodsCount = _foodRepository.TableNoTracking.Count();
            int inactiveFoodCount = _foodRepository.TableNoTracking.Count(f => f.IsActive == false);
            int deleteFoodCount = _foodRepository.TableNoTracking.Count(f => f.IsDelete);

            var result = new PageResultFood<GetAllFoodsAdminViewModel>()
            {
                Count = foodsCount,
                PageIndex = page,
                PageSize = pageSize,
                Items = foodsList,
                ActiveFoods = activeFoodCount,
                InactiveFoods = deleteFoodCount
            };
            return result;
        }

        [HttpDelete("ClearFoodSearchCache")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> ClearSearchCache()
        {
            await _mediator.Send(new DeleteAllFoodRedisCommand());
            return Ok();
        }

        [HttpGet("GetFoodsForDietPlan")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<PageResult<GetFoodsForDietPlanViewModel>>>
            GetAllFoodsOnDietCategories([FromQuery] List<int> dietCategoryIds, int categoryId
                , [FromQuery] List<int> nationalityIds)
        {
            var result = await _mediator.Send(new GetFoodsForDietPlanQuery
            {
                DietCategoryIds = dietCategoryIds,
                CategoryId = categoryId,
                NationalityIds = nationalityIds,
                LanguageName = LanguageName == null ? "Persian" : LanguageName
            });
            return result;
        }


        //=========================================================================================

        //[HttpGet("GetByFoodId")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ApiResult<FoodSelectAdminDTO>> GetAdmin(CancellationToken cancellationToken, int foodId)
        //{
        //    var food = await _foodRepository.GetByIdAsync(cancellationToken, foodId);
        //    var foodIngs = (((FoodType)food.FoodType) != FoodType.Supermarket) ? food.FoodIngredients : null;
        //    var foodMeasurUnit = food.FoodMeasureUnits;
        //    //--------------------------------------Food Ingredients-----------------------------------------
        //    var ingredients = new List<IngredientAdminModel>();
        //    foreach (var item in foodIngs)
        //    {
        //        var _ingMeasures = new List<SearchResultAdminDTO>();
        //        foreach (var ingMeas in item.Ingredient.IngredientMeasureUnits)
        //        {
        //            var _measureUnit = new SearchResultAdminDTO()
        //            {
        //                Id = ingMeas.MeasureUnitId,
        //                Name = new TranslationDto()
        //                {
        //                    Id = ingMeas.MeasureUnit.Translation.Id,
        //                    Persian = ingMeas.MeasureUnit.Translation.Persian,
        //                    English = ingMeas.MeasureUnit.Translation.English,
        //                    Arabic = ingMeas.MeasureUnit.Translation.Arabic
        //                }
        //            };
        //            _ingMeasures.Add(_measureUnit);
        //        }
        //        var ing = new IngredientAdminModel()
        //        {
        //            IngredientName = new TranslationDto()
        //            {
        //                Id = item.Ingredient.Id,
        //                Persian = item.Ingredient.Translation.Persian,
        //                English = item.Ingredient.Translation.English,
        //                Arabic = item.Ingredient.Translation.Arabic
        //            },
        //            IngredientId = item.IngredientId,
        //            MeasureUnitName = new TranslationDto()
        //            {
        //                Id = item.MeasureUnit.NameId,
        //                Persian = item.MeasureUnit.Translation.Persian,
        //                English = item.MeasureUnit.Translation.English,
        //                Arabic = item.MeasureUnit.Translation.Arabic
        //            },
        //            MeasureUnitId = item.MeasureUnitId,
        //            Value = item.IngredientValue,
        //            MeasureUnitList = _ingMeasures
        //        };
        //        ingredients.Add(ing);
        //    }
        //    //-------------------------------------Food MeasureUnits------------------------------------------
        //    var measureUnits = new List<SearchResultAdminDTO>();
        //    foreach (var item in foodMeasurUnit)
        //    {
        //        SearchResultAdminDTO _measur = new SearchResultAdminDTO()
        //        {
        //            Id = item.MeasureUnitId,
        //            Name = new TranslationDto()
        //            {
        //                Id = item.MeasureUnit.Translation.Id,
        //                Arabic = item.MeasureUnit.Translation.Arabic,
        //                English = item.MeasureUnit.Translation.English,
        //                Persian = item.MeasureUnit.Translation.Persian
        //            }
        //        };
        //        measureUnits.Add(_measur);
        //    }
        //    var Food = new FoodSelectAdminDTO()
        //    {
        //        FoodId = food.Id,
        //        Name = new TranslationDto()
        //        {
        //            Id = food.TranslationName.Id,
        //            Arabic = food.TranslationName.Arabic,
        //            English = food.TranslationName.English,
        //            Persian = food.TranslationName.Persian
        //        },
        //        Recipe = new TranslationDto()
        //        {
        //            Id = food.TranslationRecipe.Id,
        //            Arabic = food.TranslationRecipe.Arabic,
        //            English = food.TranslationRecipe.English,
        //            Persian = food.TranslationRecipe.Persian
        //        },
        //        BakingTime = food.BakingTime,
        //        BakingType = (BakingType)food.BakingType,
        //        ImageUri = CommonStrings.CommonUrl + "FoodImage/" + food.ImageUri,
        //        ImageThumb = CommonStrings.CommonUrl + "FoodImage/" + food.ImageThumb,
        //        Ingredients = ingredients,
        //        MeasureUnits = measureUnits,
        //        NutrientValue = StringConvertor.ToNumber(food.NutrientValue),
        //        Brand = (food.BrandId > 0) ? new BrandSelectAdminDTO()
        //        {
        //            Id = food.Brand.Id,
        //            Address = food.Brand.Address,
        //            Name = new Translation()
        //            {
        //                Id = food.Brand.NameId,
        //                Arabic = food.Brand.Translation.Arabic,
        //                English = food.Brand.Translation.English,
        //                Persian = food.Brand.Translation.Persian
        //            },
        //            LogoUri = food.Brand.LogoUri

        //        } : null,
        //        FoodCode = food.FoodCode,
        //        Version = food.Version
        //    };
        //    return Food;
        //}        //-------------------------------------------------------------------------------------------------

        //------------------------------------------------Post Food From Excel----------------------------------

        [HttpPost("AddSuperMarketFoodsFromExcel")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TestResult>> PostAsync(string password, int fromId, int toId, CancellationToken cancellationToken)
        {
            var result = new TestResult();
            var startTime = DateTime.Now;
            int n = 0;
            if (password.Contains("8520456"))
            {
                List<string> status = new List<string>();
                var foodList = await _ExcelTable.TableNoTracking.Where(e => e.Id >= fromId && e.Id <= toId).ToListAsync(cancellationToken); //new List<ExcelTable>(); //;
                foreach (var item in foodList)
                {
                    var foodIsExist = await _foodRepository.TableNoTracking.Where(f => f.BarcodeGs1 == item.GS1 || f.BarcodeNational == item.IRCode).FirstOrDefaultAsync(cancellationToken);
                    if (foodIsExist == null)
                    {
                        Food food = new Food()
                        {
                            BakingType = BakingType.NoTypeOfBaking,
                            BarcodeGs1 = item.GS1,
                            BarcodeNational = item.IRCode,
                            FoodType = FoodType.Supermarket,
                            WeightAfterBaking = 100,
                            WeightBeforBaking = 100,
                            EvaporatedWater = 0,
                            BakingTime = new TimeSpan(0),
                            IsActive = true,
                            IsUpdate = false,
                            Version = 0,
                            //food.ImageUri = item.Code.ToString() + ".JPG";
                            //food.ImageThumb = item.Code.ToString() + "_Thumb.JPG";
                        };
                        if (item.GS1.Length > 10)
                        {
                            food.FoodCode = (long)Convert.ToDouble(item.GS1);
                        }
                        else
                        {
                            food.FoodCode = long.Parse(item.IRCode);
                        }

                        double[] Nutrients = new double[34];

                        Nutrients[0] = item.V1;
                        Nutrients[1] = item.V2;
                        Nutrients[2] = item.V3;
                        Nutrients[3] = item.V4;
                        Nutrients[4] = item.V5;
                        Nutrients[5] = item.V6;
                        Nutrients[6] = item.V7;
                        Nutrients[7] = item.V8;
                        Nutrients[8] = item.V9;
                        Nutrients[9] = item.V10;
                        Nutrients[10] = item.V11;
                        Nutrients[11] = item.V12;
                        Nutrients[12] = item.V13;
                        Nutrients[13] = item.V14;
                        Nutrients[14] = item.V15;
                        Nutrients[15] = item.V16;
                        Nutrients[16] = item.V17;
                        Nutrients[17] = item.V18;
                        Nutrients[18] = item.V19;
                        Nutrients[19] = item.V20;
                        Nutrients[20] = item.V21;
                        Nutrients[21] = item.V22;
                        Nutrients[22] = item.V23;
                        Nutrients[23] = item.V24;
                        Nutrients[24] = item.V25;
                        Nutrients[25] = item.V26;
                        Nutrients[26] = item.V27;
                        Nutrients[27] = item.V28;
                        Nutrients[28] = item.V29;
                        Nutrients[29] = item.V30;
                        Nutrients[30] = item.V31;
                        Nutrients[31] = item.V32;
                        Nutrients[32] = item.V33;
                        Nutrients[33] = item.V34;
                        food.NutrientValue = StringConvertor.DoubleToString(Nutrients.ToList());
                        //--------------Translation-----------------------------------
                        var Name = new TranslationDto()
                        {
                            Arabic = item.ArabicName.Trim() + " " + item.EnglishName.Trim() + " " + item.PersianName.Trim(),
                            English = item.ArabicName.Trim() + " " + item.EnglishName.Trim() + " " + item.PersianName.Trim(),
                            Persian = item.ArabicName.Trim() + " " + item.EnglishName.Trim() + " " + item.PersianName.Trim(),
                        };
                        var _Name = await _mediator.Send(new CreateTranslationCommand
                        {
                            Translation = Name.ToEntity(_mapper)
                        });
                        food.NameId = _Name.Id;
                        //-------------------Brand---------------------------------------------
                        if (item.Brand != null)
                        {
                            Brand brand = _brandRepository.Table.Where(b => b.Translation.Persian.Trim() == item.Brand.Trim()).FirstOrDefault();
                            if (brand != null)
                            {
                                food.BrandId = brand.Id;
                            }
                            else
                            {
                                TranslationDto _brandName = new TranslationDto()
                                {
                                    Persian = item.Brand.Trim(),
                                    Arabic = item.Brand.Trim(),
                                    English = item.Brand.Trim()
                                };
                                var brandName = await _mediator.Send(new CreateTranslationCommand
                                {
                                    Translation = _brandName.ToEntity(_mapper)
                                });
                                Brand _brand = new Brand()
                                {
                                    NameId = brandName.Id,
                                };
                                _brand = await _brandRepository.AddAsync(_brand);
                                food.BrandId = _brand.Id;
                            }
                        }
                        food.Tag = (item.Brand == null) ? (item.ArabicName.Trim() + " " + item.EnglishName.Trim() + " " + item.PersianName.Trim()).ToLower() : (item.ArabicName.Trim() + " " + item.EnglishName.Trim() + " " + item.PersianName.Trim() + " " + item.Brand.Trim()).ToLower();
                        //food.TagArEn = (item.Brand == null) ? (item.EnglishName.Trim()).ToLower() : (item.EnglishName.Trim() + " " + item.Brand.Trim()).ToLower();
                        //--------------------------------------------------
                        food.Id = await _foodRepository.AddAsync(food, cancellationToken);
                        var foodmeas = new FoodMeasureUnit()
                        {
                            FoodId = food.Id,
                            MeasureUnitId = 571
                        };
                        await _foodMeasurUnitrepository.AddAsync(foodmeas, cancellationToken);

                        n++;

                    }
                    else
                    {
                        status.Add(item.IRCode + "_NotAdded");
                    }
                }
                result.CodeList = status;
            }
            result.count = n;
            result.duration = DateTime.Now - startTime;

            return result;
            //return Ok();
        }

        [HttpPost("AddResturantFoodsFromExcel")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TestResult>> PostResturantFoodsAsync(string password, int fromId, int toId, CancellationToken cancellationToken)
        {
            var result = new TestResult();
            var startTime = DateTime.Now;
            int n = 0;
            if (password.Contains("007#Alireza"))
            {
                List<string> status = new List<string>();
                var foodList = await _ExcelTable.TableNoTracking.Where(e => e.Id >= fromId && e.Id <= toId).ToListAsync(cancellationToken); //new List<ExcelTable>(); //;
                foreach (var item in foodList)
                {
                    Food food = new Food()
                    {
                        BakingType = BakingType.NoTypeOfBaking,
                        BarcodeGs1 = item.GS1,
                        BarcodeNational = item.IRCode,
                        FoodType = FoodType.Restaurant,
                        WeightAfterBaking = 100,
                        WeightBeforBaking = 100,
                        EvaporatedWater = 0,
                        DryIngredient = 100 - item.V2,
                        BakingTime = new TimeSpan(0),
                        //FoodHabit = FoodHabit.Normal,
                        IsActive = true,
                        IsUpdate = false,
                        Version = 0,
                        //food.ImageUri = item.Code.ToString() + ".JPG";
                        //food.ImageThumb = item.Code.ToString() + "_Thumb.JPG";
                    };
                    //if (item.GS1.Length >10)
                    //{
                    //    food.FoodCode = (long)Convert.ToDouble(item.GS1);
                    //}
                    //else
                    //{
                    //    food.FoodCode = long.Parse(item.IRCode);
                    //}                   
                    food.FoodCode = item.Code;
                    double[] Nutrients = new double[34];

                    Nutrients[0] = item.V1;
                    Nutrients[1] = item.V2;
                    Nutrients[2] = item.V3;
                    Nutrients[3] = item.V4;
                    Nutrients[4] = item.V5;
                    Nutrients[5] = item.V6;
                    Nutrients[6] = item.V7;
                    Nutrients[7] = item.V8;
                    Nutrients[8] = item.V9;
                    Nutrients[9] = item.V10;
                    Nutrients[10] = item.V11;
                    Nutrients[11] = item.V12;
                    Nutrients[12] = item.V13;
                    Nutrients[13] = item.V14;
                    Nutrients[14] = item.V15;
                    Nutrients[15] = item.V16;
                    Nutrients[16] = item.V17;
                    Nutrients[17] = item.V18;
                    Nutrients[18] = item.V19;
                    Nutrients[19] = item.V20;
                    Nutrients[20] = item.V21;
                    Nutrients[21] = item.V22;
                    Nutrients[22] = item.V23;
                    Nutrients[23] = item.V24;
                    Nutrients[24] = item.V25;
                    Nutrients[25] = item.V26;
                    Nutrients[26] = item.V27;
                    Nutrients[27] = item.V28;
                    Nutrients[28] = item.V29;
                    Nutrients[29] = item.V30;
                    Nutrients[30] = item.V31;
                    Nutrients[31] = item.V32;
                    Nutrients[32] = item.V33;
                    Nutrients[33] = item.V34;
                    food.NutrientValue = StringConvertor.DoubleToString(Nutrients.ToList());
                    //--------------Translation-----------------------------------
                    var Name = new TranslationDto()
                    {
                        Arabic = item.ArabicName.Trim(),
                        English = item.EnglishName.Trim(),
                        Persian = item.PersianName.Trim()
                    };
                    var _Name = await _mediator.Send(new CreateTranslationCommand
                    {
                        Translation = Name.ToEntity(_mapper)
                    });
                    food.NameId = _Name.Id;
                    //-------------------Brand---------------------------------------------
                    if (item.Brand != null)
                    {
                        food.Tag = Name.Persian.Trim() + " " + item.Brand.Trim();
                        Brand brand = _brandRepository.Table.Where(b => b.Translation.Persian.Trim() == item.Brand.Trim()).FirstOrDefault();
                        if (brand != null)
                        {
                            food.BrandId = brand.Id;
                        }
                        else
                        {
                            TranslationDto _brandName = new TranslationDto()
                            {
                                Persian = item.Brand.Trim(),
                                Arabic = item.Brand.Trim(),
                                English = item.Brand.Trim()
                            };
                            var brandName = await _mediator.Send(new CreateTranslationCommand
                            {
                                Translation = _brandName.ToEntity(_mapper)
                            });
                            Brand _brand = new Brand()
                            {
                                NameId = brandName.Id,
                            };
                            _brand = await _brandRepository.AddAsync(_brand);
                            food.BrandId = _brand.Id;
                        }
                    }
                    //--------------------------------------------------

                    food.Id = await _foodRepository.AddAsync(food, cancellationToken);
                    n++;
                    status.Add(food.FoodCode + "_Added");
                }
                result.CodeList = status;
            }
            result.count = n;
            result.duration = DateTime.Now - startTime;

            //return result;
            return Ok();
        }

        [HttpPost("OneIng")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TestResult>> PostOneIngAsync(string password, CancellationToken cancellationToken)
        {
            if (password.Contains("007#Alireza"))
            {
                var startTime = DateTime.Now;
                int n = 0;
                var foodList = await _ExcelTable.TableNoTracking.ToListAsync();
                foreach (var item in foodList)
                {
                    List<Food> _foodtest = await _foodRepository.TableNoTracking.Where(f => f.FoodCode == item.Code).ToListAsync();
                    if (_foodtest.Count() == 1)
                    {
                        foreach (var food in _foodtest)
                        {
                            _foodRepository.Detach(food);
                            food.ImageUri = item.ArabicName;
                            await _foodRepository.UpdateAsync(food, cancellationToken);
                        }

                    }
                }
                var result = new TestResult()
                {
                    count = n,
                    duration = DateTime.Now - startTime
                };
                return result;
            }
            return null;
        }

        [HttpPut("EditOneIng")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TestResult>> PutOneIngAsync(string password, CancellationToken cancellationToken)
        {
            if (password.Contains("007#Alireza"))
            {
                var startTime = DateTime.Now;
                int n = 0;
                var foodList = await _ExcelTable.TableNoTracking.ToListAsync();
                List<string> CodeList = new List<string>();
                foreach (var item in foodList)
                {
                    //double[] nutrients = new double[34];

                    //nutrients[0] = item.V1;
                    //nutrients[1] = item.V2;
                    //nutrients[2] = item.V3;
                    //nutrients[3] = item.V4;
                    //nutrients[4] = item.V5;
                    //nutrients[5] = item.V6;
                    //nutrients[6] = item.V7;
                    //nutrients[7] = item.V8;
                    //nutrients[8] = item.V9;
                    //nutrients[9] = item.V10;
                    //nutrients[10] = item.V11;
                    //nutrients[11] = item.V12;
                    //nutrients[12] = item.V13;
                    //nutrients[13] = item.V14;
                    //nutrients[14] = item.V15;
                    //nutrients[15] = item.V16;
                    //nutrients[16] = item.V17;
                    //nutrients[17] = item.V18;
                    //nutrients[18] = item.V19;
                    //nutrients[19] = item.V20;
                    //nutrients[20] = item.V21;
                    //nutrients[21] = item.V22;
                    //nutrients[22] = item.V23;
                    //nutrients[23] = item.V24;
                    //nutrients[24] = item.V25;
                    //nutrients[25] = item.V26;
                    //nutrients[26] = item.V27;
                    //nutrients[27] = item.V28;
                    //nutrients[28] = item.V29;
                    //nutrients[29] = item.V30;
                    //nutrients[30] = item.V31;
                    //nutrients[31] = item.V32;
                    //nutrients[32] = item.V33;
                    //nutrients[33] = item.V34;
                    // var NutrientValue = StringConvertor.DoubleToString(nutrients.ToList());

                    List<Food> _foodtest = await _foodRepository.TableNoTracking.Where(f => f.FoodCode == item.Code).ToListAsync();
                    if (_foodtest.Count() == 1)
                    {

                        foreach (var food in _foodtest)
                        {
                            var Name = new TranslationDto()
                            {
                                Arabic = item.ArabicName,
                                Persian = item.PersianName,
                                English = item.EnglishName,
                                Id = food.NameId
                            };

                            await _mediator.Send(new UpdateTranslationCommand
                            {
                                Translation = Name.ToEntity(_mapper)
                            });
                            //_foodRepository.Detach(food);
                            //food.NutrientValue = NutrientValue;
                            // await _foodRepository.UpdateAsync(food, cancellationToken);
                            n++;
                            CodeList.Add(food.FoodCode.ToString());
                        }

                    }

                }
                var result = new TestResult()
                {
                    count = n,
                    duration = DateTime.Now - startTime,
                    CodeList = CodeList
                };
                return result;
            }
            return null;
        }

        [HttpGet("GetFaultyfood")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<FoodViewModel>> GetFaultyfood(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var _foodList = await _foodRepository.Table
                .Include(ft => ft.TranslationName)
                .Include(fr => fr.TranslationRecipe)
                .Include(fi => fi.FoodIngredients).ThenInclude(i => i.Ingredient).ThenInclude(t => t.Translation)
                .Include(f => f.FoodIngredients).ThenInclude(fi => fi.Ingredient).ThenInclude(im => im.IngredientMeasureUnits)
                .Include(fm => fm.FoodMeasureUnits).ThenInclude(m => m.MeasureUnit).ThenInclude(mt => mt.Translation)
                .Include(fb => fb.Brand).ThenInclude(b => b.Translation)
                .OrderBy(o => o.Id).Where(f => (int)f.FoodType == 1)
                .Skip((Page - 1 ?? 0) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
            var countDetails = _foodList.Count();
            List<FoodViewModel> _paging = new List<FoodViewModel>();

            foreach (var food in _foodList)
            {
                var Nu = StringConvertor.ToNumber(food.NutrientValue);
                if (Nu.Count() > 34 || Nu.Count() < 34)
                {
                    var Food = new FoodViewModel()
                    {
                        FoodId = food.Id,
                        _id = food.Id.ToString(),
                        Name = new TranslationViewModel()
                        {
                            Persian = food.TranslationName.Persian,
                            English = food.TranslationName.English,
                            Arabic = food.TranslationName.Arabic,
                        },
                        BakingType = food.BakingType,
                        FoodType = food.FoodType,
                        ImageUri = food.ImageUri,       //(food.ImageUri!=null) ? CommonStrings.CommonUrl + "FoodImage/" + food.ImageUri:null,
                                                        //ImageThumb = food.ImageThumb,   //(food.ImageThumb!=null)? CommonStrings.CommonUrl + "FoodImage/" + food.ImageThumb:null,
                        NutrientValue = StringConvertor.ToNumber(food.NutrientValue),
                        Brand = (food.BrandId > 0) ? new TranslationViewModel()
                        {
                            Arabic = food.Brand.Translation.Arabic,
                            English = food.Brand.Translation.English,
                            Persian = food.Brand.Translation.Persian
                        } : null,
                        IsUpdate = food.IsUpdate,
                        Version = food.Version,
                        BakingTime = food.BakingTime,
                        BarcodeGs1 = food.BarcodeGs1,
                        BarcodeNational = food.BarcodeNational,
                    };
                    _paging.Add(Food);
                }
            }

            var result = new PageResult<FoodViewModel>()
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };
            return result;

        }

        [HttpPut("EditSuperMarketyFoodCode")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TestResult>> PutSuperMarketyFoodCodeAsync(int fromId, int ToId, CancellationToken cancellationToken)
        {
            var startTime = DateTime.Now;
            int n = 0;
            var foodList = await _foodRepository.Table
                .Include(ft => ft.TranslationName)
                .Include(fb => fb.Brand).ThenInclude(b => b.Translation)
                .OrderBy(o => o.Id).Where(f => (int)f.FoodType == 3 && f.Id > fromId && f.Id < ToId)
                .ToListAsync(cancellationToken);
            List<string> CodeList = new List<string>();
            foreach (var food in foodList)
            {
                var item = await _ExcelTable.TableNoTracking.Where(e => e.IRCode == food.BarcodeNational || e.GS1 == food.BarcodeGs1 || e.Code == food.FoodCode).FirstOrDefaultAsync(cancellationToken);
                if (item != null)
                {
                    //if (food.BrandId > 0)
                    //{
                    //    food.Tag = food.TranslationName.Persian.Trim() + " " + food.Brand.Translation.Persian.Trim();
                    //}

                    double[] nutrients = new double[34];

                    nutrients[0] = item.V1;
                    nutrients[1] = item.V2;
                    nutrients[2] = item.V3;
                    nutrients[3] = item.V4;
                    nutrients[4] = item.V5;
                    nutrients[5] = item.V6;
                    nutrients[6] = item.V7;
                    nutrients[7] = item.V8;
                    nutrients[8] = item.V9;
                    nutrients[9] = item.V10;
                    nutrients[10] = item.V11;
                    nutrients[11] = item.V12;
                    nutrients[12] = item.V13;
                    nutrients[13] = item.V14;
                    nutrients[14] = item.V15;
                    nutrients[15] = item.V16;
                    nutrients[16] = item.V17;
                    nutrients[17] = item.V18;
                    nutrients[18] = item.V19;
                    nutrients[19] = item.V20;
                    nutrients[20] = item.V21;
                    nutrients[21] = item.V22;
                    nutrients[22] = item.V23;
                    nutrients[23] = item.V24;
                    nutrients[24] = item.V25;
                    nutrients[25] = item.V26;
                    nutrients[26] = item.V27;
                    nutrients[27] = item.V28;
                    nutrients[28] = item.V29;
                    nutrients[29] = item.V30;
                    nutrients[30] = item.V31;
                    nutrients[31] = item.V32;
                    nutrients[32] = item.V33;
                    nutrients[33] = item.V34;
                    var NutrientValue = StringConvertor.DoubleToString(nutrients.ToList());
                    if (item.GS1.Length > 4)
                    {
                        food.FoodCode = Convert.ToInt64(item.GS1);
                    }
                    else if (item.IRCode.Length > 4)
                    {
                        food.FoodCode = Convert.ToInt64(item.IRCode);
                    }

                    _foodRepository.Detach(food);
                    await _foodRepository.UpdateAsync(food, cancellationToken);
                    n++;
                }
                else
                {
                    CodeList.Add(food.FoodCode.ToString());
                }

            }
            var result = new TestResult()
            {
                count = n,
                duration = DateTime.Now - startTime,
                CodeList = CodeList
            };
            return result;
        }

        [HttpDelete("DeleteSuperMarkety")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TestResult>> DeleteSuperMarketi(CancellationToken cancellationToken)
        {
            var startTime = DateTime.Now;
            int n = 0;
            try
            {
                var foodlist = await _foodRepository.Table
                             .Include(fm => fm.FoodMeasureUnits).ThenInclude(m => m.MeasureUnit)
                             .Include(fi => fi.FoodIngredients).ThenInclude(i => i.Ingredient)
                             .Where(f => (int)f.FoodType == 3).ToListAsync(cancellationToken);

                List<string> CodeList = new List<string>();
                foreach (var food in foodlist)
                {
                    await _foodMeasurUnitrepository.DeleteRangeAsync(food.FoodMeasureUnits, cancellationToken);
                    await _foodRepository.DeleteAsync(food, cancellationToken);
                    List<int> nameIds = new List<int>();
                    nameIds.Add(food.NameId);
                    await _mediator.Send(new DeleteTranslationCommand
                    {
                        Ids = nameIds
                    });
                    n++;
                    CodeList.Add(food.Id.ToString());
                }
                var result = new TestResult()
                {
                    count = n,
                    duration = DateTime.Now - startTime,
                    CodeList = CodeList
                };
                return result;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        [HttpPut("AllHomeFoodsEditFromBack")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> AllHomeFoodsEditFromBack(CancellationToken cancellationToken)
        {
            List<Food> Foods = await _foodRepository.Table
            .Include(fi => fi.FoodIngredients).ThenInclude(i => i.Ingredient)
            .Include(f => f.FoodIngredients).ThenInclude(fi => fi.Ingredient).ThenInclude(im => im.IngredientMeasureUnits).ThenInclude(m => m.MeasureUnit)
            .Include(fm => fm.FoodMeasureUnits).ThenInclude(m => m.MeasureUnit).ThenInclude(mt => mt.Translation)
            .Include(fb => fb.Brand).ThenInclude(b => b.Translation)
            .Where(f => (int)f.FoodType == 1).ToListAsync(cancellationToken);
            foreach (var oldFood in Foods)
            {
                if (oldFood.FoodIngredients.Count() > 1)
                {
                    List<IngredientAdminSelectDTO> ingList = new List<IngredientAdminSelectDTO>();
                    foreach (var foodIng in oldFood.FoodIngredients)
                    {
                        ingList.Add(new IngredientAdminSelectDTO()
                        {
                            Id = foodIng.IngredientId,
                            MessureUnit = new MeasureUnitModelDTO
                            {
                                Id = foodIng.MeasureUnitId,
                            },
                            Value = foodIng.IngredientValue,
                        });
                    }


                    var _cal = new SelectIngredient();
                    List<IngMeasurModel> ingMeasurModels = ingList.Select(i => new IngMeasurModel() { Id = i.Id, Value = i.Value, MeasureUnitId = i.MessureUnit.Id }).ToList();
                    _cal = await _mediator.Send(new GetIngredientCalculateQuery { IngredientModels = ingMeasurModels });

                    var foodFactors = Formula.FoodWeightsCalculation(new FoodParameters()
                    {
                        foodWeight = _cal.SumWeight,
                        BakingRatio = ((BakingType)oldFood.BakingType).ToDescription().ToDouble(),
                        BakingTimeInMinute = oldFood.BakingTime.TotalMinutes,
                        foodNutrients = _cal.IngredientCalculate
                    });

                    oldFood.WeightBeforBaking = _cal.SumWeight;
                    oldFood.EvaporatedWater = foodFactors.EvaporatedWater;
                    oldFood.WeightAfterBaking = foodFactors.AfterBaking;
                    oldFood.BakingTime = oldFood.BakingTime;
                    oldFood.DryIngredient = foodFactors.DryIngredient;
                    List<double> _foodnutrients = new List<double>();
                    if (foodFactors.AfterBaking > 0)
                    {
                        foreach (var nut in _cal.IngredientCalculate)
                        {
                            _foodnutrients.Add(nut * 100 / foodFactors.AfterBaking);
                        }
                        _foodnutrients[1] = (foodFactors.AfterBaking - foodFactors.DryIngredient) * 100 / foodFactors.AfterBaking;
                    }
                    else
                    {
                        _foodnutrients = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    }
                    oldFood.NutrientValue = StringConvertor.DoubleToString(_foodnutrients);
                    oldFood.IsActive = false;
                    oldFood.Version = 0.01;
                    oldFood.IsUpdate = true;
                    await _foodRepository.UpdateAsync(oldFood, cancellationToken);
                }
            }

            return Ok();
        }

        [HttpPut("RoundHomeFoodsNutrients")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> RoundHomeFoodsNutrients(CancellationToken cancellationToken)
        {
            List<Food> Foods = await _foodRepository.TableNoTracking
            .Where(f => (int)f.FoodType == 1).ToListAsync(cancellationToken);
            foreach (var oldFood in Foods)
            {
                List<double> NutrientValue = StringConvertor.ToNumber(oldFood.NutrientValue);
                oldFood.NutrientValue = StringConvertor.DoubleToString(NutrientValue);

                await _foodRepository.UpdateAsync(oldFood, cancellationToken);
            }

            return Ok();
        }

        [HttpPut("EditResturanFoods")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TestResult>> EditResturanFoodsAsync(string password, CancellationToken cancellationToken)
        {
            if (password.Contains("007#Alireza"))
            {
                var startTime = DateTime.Now;
                int n = 0;
                var foodList = await _ExcelTable.TableNoTracking.ToListAsync();
                List<string> CodeList = new List<string>();
                foreach (var item in foodList)
                {

                    List<Food> _foodtest = await _foodRepository.TableNoTracking.Where(f => f.FoodCode == item.Code).ToListAsync();
                    if (_foodtest.Count() == 1)
                    {

                        foreach (var food in _foodtest)
                        {
                            if (item.Brand != null)
                            {
                                food.Tag = item.Brand;
                            }
                            if (item.IRCode != null)
                            {
                                food.TagArEn = item.IRCode;
                            }
                            _foodRepository.Detach(food);
                            //food.NutrientValue = NutrientValue;
                            await _foodRepository.UpdateAsync(food, cancellationToken);
                            var Name = new TranslationDto()
                            {
                                Arabic = item.ArabicName,
                                Persian = item.PersianName,
                                English = item.EnglishName,
                                Id = food.NameId
                            };
                            _foodRepository.Detach(food);
                            await _mediator.Send(new UpdateTranslationCommand
                            {
                                Translation = Name.ToEntity(_mapper)
                            });
                            n++;
                            CodeList.Add(food.FoodCode.ToString());
                        }

                    }

                }
                var result = new TestResult()
                {
                    count = n,
                    duration = DateTime.Now - startTime,
                    CodeList = CodeList
                };
                return result;
            }
            return null;
        }

        [HttpPut("UpdateFoodRedis")]
        public async Task<ApiResult> UpdateFoodRedis()
        {
            await _mediator.Send(new UpdateFoodRedisCommand());
            return Ok();
        }

        [HttpPut("EditSuperMarketFoods")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TestResult>> EditSuperMarketFoodsAsync(string password, int fromId, int ToId, CancellationToken cancellationToken)
        {
            if (password == "alireza8520456+")
            {
                var startTime = DateTime.Now;
                int n = 0;
                var foodList = await _foodRepository.Table
                    //.Include(t=>t.TranslationName)
                    .OrderBy(o => o.Id).Where(f => (int)f.FoodType == 3 && f.Id >= fromId && f.Id <= ToId)
                    .ToListAsync(cancellationToken);
                List<string> CodeList = new List<string>();
                foreach (var food in foodList)
                {
                    var item = await _ExcelTable.TableNoTracking.Where(e => e.IRCode == food.BarcodeNational || e.GS1 == food.BarcodeGs1).FirstOrDefaultAsync(cancellationToken);
                    if (item != null)
                    {
                        Translation Name = new Translation();
                        Name = await _translationRepository.GetByIdAsync(cancellationToken, food.NameId);
                        if (item.EnglishName != null)
                        {
                            Name = new Translation()
                            {
                                Id = Name.Id,
                                Persian = Name.Persian + " " + item.EnglishName + " " + item.PersianName,
                                Arabic = Name.Arabic + " " + item.EnglishName + " " + item.PersianName,
                                English = Name.English + " " + item.EnglishName + " " + item.PersianName,
                            };
                            await _translationRepository.BasicUpdateAsync(Name.Id, Name, cancellationToken);
                        }
                        //await _mediator.Send(new UpdateTranslationCommand
                        //{
                        //    Translation = Name,
                        //});

                        //if (food.BrandId > 0)
                        //{
                        //    food.Tag = food.TranslationName.Persian.Trim() + " " + food.Brand.Translation.Persian.Trim();
                        //}

                        //double[] nutrients = new double[34];

                        //nutrients[0] = item.V1;
                        //nutrients[1] = item.V2;
                        //nutrients[2] = item.V3;
                        //nutrients[3] = item.V4;
                        //nutrients[4] = item.V5;
                        //nutrients[5] = item.V6;
                        //nutrients[6] = item.V7;
                        //nutrients[7] = item.V8;
                        //nutrients[8] = item.V9;
                        //nutrients[9] = item.V10;
                        //nutrients[10] = item.V11;
                        //nutrients[11] = item.V12;
                        //nutrients[12] = item.V13;
                        //nutrients[13] = item.V14;
                        //nutrients[14] = item.V15;
                        //nutrients[15] = item.V16;
                        //nutrients[16] = item.V17;
                        //nutrients[17] = item.V18;
                        //nutrients[18] = item.V19;
                        //nutrients[19] = item.V20;
                        //nutrients[20] = item.V21;
                        //nutrients[21] = item.V22;
                        //nutrients[22] = item.V23;
                        //nutrients[23] = item.V24;
                        //nutrients[24] = item.V25;
                        //nutrients[25] = item.V26;
                        //nutrients[26] = item.V27;
                        //nutrients[27] = item.V28;
                        //nutrients[28] = item.V29;
                        //nutrients[29] = item.V30;
                        //nutrients[30] = item.V31;
                        //nutrients[31] = item.V32;
                        //nutrients[32] = item.V33;
                        //nutrients[33] = item.V34;
                        //var NutrientValue = StringConvertor.DoubleToString(nutrients.ToList());

                        //if (item.GS1.Length > 4)
                        //{
                        //    food.FoodCode = Convert.ToInt64(item.GS1);
                        //}
                        //else if (item.IRCode.Length > 4)
                        //{
                        //    food.FoodCode = Convert.ToInt64(item.IRCode);
                        //}

                        //_foodRepository.Detach(food);
                        //await _foodRepository.UpdateAsync(food, cancellationToken);



                        n++;
                    }
                    else
                    {
                        CodeList.Add(food.FoodCode.ToString());
                    }

                }
                var result = new TestResult()
                {
                    count = n,
                    duration = DateTime.Now - startTime,
                    CodeList = CodeList
                };
                return result;
            }
            return null;
        }

        [HttpGet("GetFaultyFoods")]
        public async Task<PageResult<FoodSelectAdminDTO>> GetFaultyFoods(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var _foodList = await _foodRepository.Table
                .Include(ft => ft.TranslationName)
                .Include(fr => fr.TranslationRecipe)
                .Include(fi => fi.FoodIngredients).ThenInclude(i => i.Ingredient).ThenInclude(t => t.Translation)
                .Include(f => f.FoodIngredients).ThenInclude(fi => fi.Ingredient).ThenInclude(im => im.IngredientMeasureUnits)
                .Include(fm => fm.FoodMeasureUnits).ThenInclude(m => m.MeasureUnit).ThenInclude(mt => mt.Translation)
                .Include(fb => fb.Brand).ThenInclude(b => b.Translation)
                .Where(f => (int)f.FoodType == 1)
                .OrderByDescending(o => o.Id)
                .Skip((Page - 1 ?? 0) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
            var countDetails = _foodList.Count();
            List<FoodSelectAdminDTO> _paging = new List<FoodSelectAdminDTO>();

            foreach (var food in _foodList)
            {
                if (food.FoodIngredients == null)
                {
                    //-------------------------------------Food MeasureUnits------------------------------------------
                    var measureUnits = new List<SearchResultAdminDTO>();
                    foreach (var item in food.FoodMeasureUnits)
                    {
                        SearchResultAdminDTO _measur = new SearchResultAdminDTO()
                        {
                            Id = item.MeasureUnitId,
                            Name = new TranslationDto()
                            {
                                Id = item.MeasureUnit.Translation.Id,
                                Arabic = item.MeasureUnit.Translation.Arabic,
                                English = item.MeasureUnit.Translation.English,
                                Persian = item.MeasureUnit.Translation.Persian
                            }
                        };
                        measureUnits.Add(_measur);
                    }
                    measureUnits = measureUnits.Distinct().ToList();
                    //-------------------------------------------------------------------------------------------------
                    var Food = new FoodSelectAdminDTO();
                    Food.FoodId = food.Id;
                    Food.Name = new TranslationDto()
                    {
                        Id = food.TranslationName.Id,
                        Persian = food.TranslationName.Persian,
                        English = food.TranslationName.English,
                        Arabic = food.TranslationName.Arabic
                    };
                    Food.Recipe = (food.TranslationRecipe != null) ? new TranslationDto()
                    {
                        Id = food.TranslationRecipe.Id,
                        Persian = food.TranslationRecipe.Persian,
                        English = food.TranslationRecipe.English,
                        Arabic = food.TranslationRecipe.Arabic
                    } : null;
                    Food.BakingType = food.BakingType;
                    Food.FoodType = food.FoodType;
                    Food.ImageUri = CommonStrings.CommonUrl + "FoodImage/" + food.ImageUri;
                    Food.ImageThumb = CommonStrings.CommonUrl + "FoodImage/" + food.ImageThumb;
                    Food.NutrientValue = StringConvertor.ToNumber(food.NutrientValue);
                    Food.Brand = (food.BrandId > 0) ? new BrandSelectAdminDTO()
                    {
                        Id = food.Brand.Id,
                        Address = food.Brand.Address,
                        Name = new Translation()
                        {
                            Id = food.Brand.NameId,
                            Arabic = food.Brand.Translation.Arabic,
                            English = food.Brand.Translation.English,
                            Persian = food.Brand.Translation.Persian
                        },
                        LogoUri = food.Brand.LogoUri

                    } : null;
                    Food.FoodCode = food.FoodCode;
                    Food.Version = food.Version;
                    Food.BakingTime = food.BakingTime;
                    Food.BarcodeGs1 = food.BarcodeGs1;
                    Food.BarcodeNational = food.BarcodeNational;
                    //Food.FoodHabit = food.FoodHabit;
                    Food.MeasureUnits = measureUnits;
                    Food.Tag = food.Tag;
                    _paging.Add(Food);
                }
            }
            var result = new PageResult<FoodSelectAdminDTO>()
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };
            return result;
        }

        [HttpGet("TestAddFoodNameInTag")]
        public async Task<PageResult<FoodSelectAdminDTO>> AddFoodNameInTag(string password, int FromId, int ToId, CancellationToken cancellationToken)
        {
            if (password == "alireza007")
            {
                var _foodList = await _foodRepository.Table
              .Include(ft => ft.TranslationName)
              .Include(fr => fr.TranslationRecipe)
              .Include(fi => fi.FoodIngredients).ThenInclude(i => i.Ingredient).ThenInclude(t => t.Translation)
              .Include(f => f.FoodIngredients).ThenInclude(fi => fi.Ingredient).ThenInclude(im => im.IngredientMeasureUnits)
              .Include(fm => fm.FoodMeasureUnits).ThenInclude(m => m.MeasureUnit).ThenInclude(mt => mt.Translation)
              .Include(fb => fb.Brand).ThenInclude(b => b.Translation)
              .OrderByDescending(o => o.Id)
              .Where(f => (int)f.FoodType == 2 && f.Id >= FromId && f.Id <= ToId)
              .ToListAsync(cancellationToken);
                int countDetails = 0;
                List<FoodSelectAdminDTO> _paging = new List<FoodSelectAdminDTO>();

                foreach (var food in _foodList)
                {
                    food.Tag = (food.Tag + " - " + food.TranslationName.Persian).ToLower();

                    if (food.TranslationName.Arabic != null)
                    {
                        food.TagArEn = (food.TagArEn + " - " + food.TranslationName.Arabic).ToLower();
                    }
                    if (food.TranslationName.English != null)
                    {
                        food.TagArEn = (food.TagArEn + " - " + food.TranslationName.English).ToLower();
                    }
                    _foodRepository.Detach(food);
                    await _foodRepository.UpdateAsync(food, cancellationToken);
                    countDetails++;
                }
                var result = new PageResult<FoodSelectAdminDTO>()
                {
                    Count = countDetails,

                    Items = _paging
                };
                return result;
            }
            return null;
        }

        [HttpPut("TestMesureUnitSuperMarketFoodsFromExcel")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> TestMesureUnitSuperMarketFoodsFromExcelAsync(string password, int fromId, int toId, CancellationToken cancellationToken)
        {
            var result = new TestResult();
            var startTime = DateTime.Now;
            int n = 0;
            if (password.Contains("8520456"))
            {
                List<string> status = new List<string>();
                var foodList = await _ExcelTable.TableNoTracking.Where(f => f.Id >= fromId && f.Id <= toId).ToListAsync(cancellationToken); //new List<ExcelTable>(); //;
                foreach (var item in foodList)
                {
                    var foodIsExist = await _foodRepository.Table
                        .Include(f => f.FoodMeasureUnits)
                        .Where(f => f.BarcodeGs1 == item.GS1 || f.BarcodeNational == item.IRCode).FirstOrDefaultAsync(cancellationToken);
                    if (foodIsExist != null)
                    {
                        var m = foodIsExist.FoodMeasureUnits.Where(m => m.MeasureUnitId == 32).Count();
                        if (m == 0)
                        {
                            var foodmeas = new FoodMeasureUnit()
                            {
                                FoodId = foodIsExist.Id,
                                MeasureUnitId = 32
                            };
                            await _foodMeasurUnitrepository.AddAsync(foodmeas, cancellationToken);
                            n++;
                        }

                        //--------------------------------------------------

                        // status.Add(foodIsExist.FoodCode + "_Added");
                    }
                }
                // result.CodeList = status;
            }
            result.count = n;
            result.duration = DateTime.Now - startTime;

            //return result;
            return n;
        }

        [HttpPut("TestDeleteMesureUnitSuperMarketFoodsFromExcel")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> DeleteMesureUnitSuperMarketFoodsFromExcelAsync(string password, int fromId, int toId, CancellationToken cancellationToken)
        {
            var result = new TestResult();
            var startTime = DateTime.Now;
            int n = 0;
            if (password.Contains("8520456"))
            {
                List<string> status = new List<string>();
                var foodList = await _ExcelTable.TableNoTracking.Where(f => f.Id >= fromId && f.Id <= toId).ToListAsync(cancellationToken); //new List<ExcelTable>(); //;
                foreach (var item in foodList)
                {
                    var foodIsExist = await _foodRepository.Table
                        .Include(f => f.FoodMeasureUnits)
                        .Where(f => f.BarcodeGs1 == item.GS1 || f.BarcodeNational == item.IRCode).FirstOrDefaultAsync(cancellationToken);
                    try
                    {
                        if (foodIsExist != null)
                        {
                            var m = foodIsExist.FoodMeasureUnits.Where(m => m.MeasureUnitId == 32).FirstOrDefault();
                            if (m != null)
                            {
                                await _foodMeasurUnitrepository.DeleteAsync(m, cancellationToken);
                                n++;
                            }

                            //--------------------------------------------------

                            // status.Add(foodIsExist.FoodCode + "_Added");
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
                // result.CodeList = status;
            }
            result.count = n;
            result.duration = DateTime.Now - startTime;

            //return result;
            return n;
        }

        [HttpPut("TesEditTagArEn")]
        public async Task<PageResult<FoodSelectAdminDTO>> TesEditTagArEn(string password, int FromId, int ToId, CancellationToken cancellationToken)
        {
            int countDetails = 0;
            var foodList = await _ExcelTable.TableNoTracking.Where(f => f.Id >= FromId && f.Id <= ToId).ToListAsync(cancellationToken); //new List<ExcelTable>(); //;
            if (password == "alireza007")
            {
                List<Food> foods = new List<Food>();
                foreach (var item in foodList)
                {
                    var food = new Food();
                    food = _foodRepository.Table.Where(f => f.Id == item.Id).FirstOrDefault();
                    food.TagArEn = item.ArabicName.ToLower();
                    food.Tag = item.EnglishName.ToLower();
                    _foodRepository.Detach(food);
                    foods.Add(food);
                    countDetails++;
                }
                await _foodRepository.UpdateRangeAsync(foods, cancellationToken);
                PageResult<FoodSelectAdminDTO> result = new PageResult<FoodSelectAdminDTO>()
                {
                    Count = countDetails,

                    Items = null,
                };
                return result;
            }
            return null;
        }

        [HttpPut("TesEditTranslation")]
        public async Task<PageResult<FoodSelectAdminDTO>> TesEditTranslation(string password, int FromId, int ToId, CancellationToken cancellationToken)
        {
            int countDetails = 0;
            var foodList = await _ExcelTable.TableNoTracking.Where(f => f.Id >= FromId && f.Id <= ToId).ToListAsync(cancellationToken); //new List<ExcelTable>(); //;
            if (password == "alireza007")
            {
                List<Food> foods = new List<Food>();
                foreach (var item in foodList)
                {
                    await _mediator.Send(new UpdateTranslationCommand
                    {
                        Translation = new Translation
                        {
                            Id = item.Code,
                            Arabic = item.ArabicName,
                            English = item.EnglishName,
                            Persian = item.PersianName
                        }
                    });
                    countDetails++;
                }
                PageResult<FoodSelectAdminDTO> result = new PageResult<FoodSelectAdminDTO>()
                {
                    Count = countDetails,

                    Items = null,
                };
                return result;
            }
            return null;
        }


        [HttpGet("GetDuplicateIngInFoods")]
        [Authorize(Roles = "Admin")]
        public async Task<List<IdValue>> GetDuplicateIngInFoods(CancellationToken cancellationToken)
        {
            List<IdValue> result = new List<IdValue>();
            try
            {
                var _foodList = await _foodRepository.Table
          .Include(ft => ft.TranslationName)
          .Include(fi => fi.FoodIngredients)
          .OrderBy(o => o.Id).Where(f => (int)f.FoodType == 1)
          .ToListAsync(cancellationToken);

                foreach (var food in _foodList)
                {
                    foreach (var item in food.FoodIngredients)
                    {
                        var ings = food.FoodIngredients.Where(i => i.IngredientId == item.IngredientId);
                        int count = ings.Count();
                        if (count > 1)
                        {
                            result.Add(new IdValue() { Id = food.FoodCode, Value = food.TranslationName.Persian });
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new AppException();
            }
            return result;

        }

        [HttpPut("TesEditTag")]
        public async Task<List<Food>> TesEditTag(string password, int FromId, int ToId, CancellationToken cancellationToken)
        {
            int countDetails = 0;
            var foodList = await _foodRepository.Table.Include(t => t.TranslationName).Where(f => f.TranslationName.Persian.Contains("آب پز")).ToListAsync(cancellationToken);
            if (password == "alireza007")
            {
                List<Food> foods = new List<Food>();
                foreach (var item in foodList)
                {
                    var food = new Food();
                    food = _foodRepository.Table.Where(f => f.Id == item.Id).FirstOrDefault();
                    food.Tag = food.Tag + " - " + "آبپز";
                    _foodRepository.Detach(food);
                    foods.Add(food);
                    countDetails++;
                }
                await _foodRepository.UpdateRangeAsync(foods, cancellationToken);
                foods = foodList;
                return foods;
            }
            return null;
        }

        [HttpGet("GetFoodsWithNoMeasureunit")]
        [Authorize(Roles = "Admin")]
        public async Task<List<IdValue>> GetFoodsWithNoMeasureunit(CancellationToken cancellationToken)
        {
            List<IdValue> result = new List<IdValue>();
            try
            {
                var _foodList = await _foodRepository.Table
          .Include(ft => ft.TranslationName)
          .Include(fi => fi.FoodMeasureUnits)
          .OrderBy(o => o.Id).Where(f => (int)f.FoodType == 1)
          .ToListAsync(cancellationToken);

                foreach (var food in _foodList)
                {
                    if (food.FoodMeasureUnits.Count() == 0)
                    {
                        result.Add(new IdValue() { Id = food.FoodCode, Value = food.TranslationName.Persian });
                    }
                }
            }
            catch (Exception ex)
            {

                throw new AppException(ex.Message);
            }
            return result;

        }

        [Route("[action]")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<List<int>> GetFoodsByBodyType(int bodyType, CancellationToken cancellationToken)
        {
            var _foodList = await _foodRepository.Table
                .Include(ft => ft.TranslationName)
                .Where(f => (int)f.FoodType == 1)
                .OrderByDescending(o => o.Id)
                .ToListAsync(cancellationToken);
            List<int> result = new List<int>();
            foreach (var item in _foodList)
            {
                var nutrients = StringConvertor.ToNumber(item.NutrientValue);
                double calorie = nutrients[24];
                MacroNutrientsModel macroNutrients = new MacroNutrientsModel();
                switch (bodyType)
                {
                    case 0: //Ectomorph
                        macroNutrients = new MacroNutrientsModel()
                        {
                            Calorie = calorie,
                            Fat = calorie * 0.3 / 9,
                            Protein = calorie * 0.5 / 4,
                            Carbohydrate = calorie * 0.2 / 4,
                        };
                        break;
                    case 1:   //Mesomorph
                        macroNutrients = new MacroNutrientsModel()
                        {
                            Calorie = calorie,
                            Fat = calorie * 0.3 / 9,
                            Protein = calorie * 0.3 / 4,
                            Carbohydrate = calorie * 0.4 / 4,
                        };
                        break;
                    case 2:  //Endomorph
                        macroNutrients = new MacroNutrientsModel()
                        {
                            Calorie = calorie,
                            Fat = calorie * 0.4 / 9,
                            Protein = calorie * 0.35 / 4,
                            Carbohydrate = calorie * 0.25 / 4,
                        };
                        break;
                    default:
                        macroNutrients = new MacroNutrientsModel()
                        {
                            Calorie = calorie,
                            Carbohydrate = calorie / (3 * 4),
                            Protein = calorie / (3 * 4),
                            Fat = calorie / (3 * 9)
                        };
                        break;
                };

                if (calorie > 0)
                {
                    if (nutrients[0] <= (macroNutrients.Fat + 1) && (macroNutrients.Fat - 1) <= nutrients[0])
                    {
                        //if (nutrients[9] <= (macroNutrients.Protein + 3) && (macroNutrients.Protein - 3) <= nutrients[9])
                        //{
                        if (nutrients[31] <= (macroNutrients.Carbohydrate + 3) && (macroNutrients.Carbohydrate - 3) <= nutrients[31])
                        {
                            result.Add(item.Id);
                        }
                        //}
                    }

                }

            }
            return result;
        }

        [HttpPut("ChangeStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<FoodChangeStatusViewModel>> ChangeStatus(int foodId, bool isActive, CancellationToken cancellationToken)
        {
            var food = await _foodRepository.Entities.FindAsync(foodId);

            food.IsActive = isActive;
            await _foodRepository.UpdateAsync(food, cancellationToken);

            return new ApiResult<FoodChangeStatusViewModel>(
                true, ApiResultStatusCode.Success, new FoodChangeStatusViewModel
                {
                    IsActive = food.IsActive
                });
        }

        [HttpGet("GetFoodsRecipe")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<List<GetFoodsRecipeViewModel>>> GetFoodsRecipe(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetFoodsRecipeQuery()));
        }

        [HttpGet("GetRecipeCategory")]
        public async Task<ApiResult<List<FoodDTO>>> GetRecipeCategory(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var _foodList = await _foodRepository.Table
          .Where(x => x.IsRecipe)
          .Include(f => f.FoodIngredients).ThenInclude(fi => fi.Ingredient).ThenInclude(im => im.Translation)
          .Include(fb => fb.Brand).ThenInclude(b => b.Translation)
          .OrderBy(o => o.Id).Where(f => (int)f.FoodType == 1)
          .Skip((Page - 1 ?? 0) * PageSize)
          .Take(PageSize)
          .Select(x => new Food
          {
              BakingType = x.BakingType,
              BakingTime = x.BakingTime,
              PersonCount = x.PersonCount,
              IsRecipeCategoreId = x.IsRecipeCategoreId,
              IsRecipe = x.IsRecipe,
              Tag = x.Tag,
              FoodIngredients = x.FoodIngredients,
              TranslationName = x.TranslationName,
              TranslationRecipe = x.TranslationRecipe,
          })
          .ToListAsync(cancellationToken);

            List<FoodDTO> foodDtos = new List<FoodDTO>();
            foreach (var i in _foodList)
            {
                var ingredients = new List<IngMeasurModel>();
                foreach (var x in i.FoodIngredients)
                {
                    var ing = new IngMeasurModel
                    {
                        Id = x.Id,
                        MeasureUnitId = x.MeasureUnitId,
                        Value = x.IngredientValue,
                        Translation = new Translation
                        {
                            Arabic = x.Ingredient.Translation.Arabic,
                            English = x.Ingredient.Translation.English,
                            Persian = x.Ingredient.Translation.Persian
                        }
                    };
                    ingredients.Add(ing);
                }
                foodDtos.Add(new FoodDTO
                {
                    BakingTime = i.BakingTime,
                    BakingType = (int)i.BakingType,
                    Id = i.Id,
                    Ingredients = ingredients,
                    Name = new TranslationDto
                    {
                        Id = i.NameId,
                        Arabic = i.TranslationName.Arabic,
                        English = i.TranslationName.English,
                        Persian = i.TranslationName.Persian
                    },
                    Recipe = new TranslationDto
                    {
                        Id = i.NameId,
                        Arabic = i.TranslationRecipe.Arabic,
                        English = i.TranslationRecipe.English,
                        Persian = i.TranslationRecipe.Persian
                    },
                    IsRecipe = i.IsRecipe,
                    Tag = i.Tag,
                    PersonCount = i.PersonCount
                });
            }
            return foodDtos;
        }


    }
}
