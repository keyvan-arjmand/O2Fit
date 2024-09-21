using System;
using System.Linq;
using System.Threading;
using FoodStuff.Service.Models;
using FoodStuff.Service.v2.Query.Food;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Common.Exceptions;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.v1.Query;
using WebFramework.Api;
using FoodStuff.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using FoodStuff.API.Models;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Service.v1.Command;
using Service.v1.Command;
using CreateFoodCommand = FoodStuff.Service.v2.Command.Foods.CreateFoodCommand;
using FoodStuff.Service.v2.Command.Recipes;
using FoodStuff.Service.v2.Command.RecipeSteps;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.v2.Command.Tips;
using FoodStuff.Service.v2.Command.FoodMeasureUnits;
using FoodStuff.Data.Repositories;
using System.Collections.Generic;
using Common.Utilities;
using FoodStuff.Service.v2.Command.FoodIngredients;
using FoodStuff.Service.Contracts;
using FoodStuff.Service.Formula;
using Microsoft.JSInterop.Infrastructure;
using FoodStuff.Service.v2.Command.Foods;
using FoodStuff.Service.v2.Command.Translations;
using GetFoodsForDietPlanQuery = FoodStuff.Service.v2.Query.Food.GetFoodsForDietPlanQuery;
using FoodStuff.API.Models.DTOs;
using FoodStuff.Service.v2.Query.Recipe;

namespace FoodStuff.API.Controllers.v2
{
    [ApiVersion("2")]
    public class FoodController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IFoodMeasureUnitRepository _foodMeasureUnitRepository;
        private readonly IFoodIngredientRepository _foodIngredientRepository;
        public FoodController(IMediator mediator, IRedisCacheClient redisCacheClient, IFoodRepository foodRepository, IMapper mapper, IFileService fileService,
            IFoodMeasureUnitRepository foodMeasureUnitRepository, IFoodIngredientRepository foodIngredientRepository)
        {
            _mediator = mediator;
            _redisCacheClient = redisCacheClient;
            _foodRepository = foodRepository;
            _mapper = mapper;
            _fileService = fileService;
            _foodMeasureUnitRepository = foodMeasureUnitRepository;
            _foodIngredientRepository = foodIngredientRepository;
        }

        //=================== Start Old ==================================

        //[HttpGet("GetById")]
        //[Authorize]
        //public async Task<GetFoodByIdViewModel> GetById(int id)
        //{
        //    var result = new GetFoodByIdViewModel();
        //
        //    if (await _redisCacheClient.Db7.ExistsAsync($"GetFood_V2_{id}"))
        //    {
        //        result = await _redisCacheClient.Db7.GetAsync<GetFoodByIdViewModel>($"GetFood_V2_{id}");
        //    }
        //    else
        //    {
        //        result = await _mediator.Send(new GetFoodByIdQuery { Id = id });
        //        await _redisCacheClient.Db7.AddAsync($"GetFood_V2_{id}", result);
        //    }
        //    return result;
        //
        //}

        //==================== End Old ==============================

        [HttpGet("{id:int}", Name = "GetById")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ApiResult<FoodWithDetailsDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var food = await _mediator.Send(new GetFoodWithDetailsByIdQuery
            {
                Id = id
            }, cancellationToken);

            if (food == null)
            {
                return new ApiResult<FoodWithDetailsDto>(true, ApiResultStatusCode.BadRequest, null);
            }
            return new ApiResult<FoodWithDetailsDto>(true, ApiResultStatusCode.Success, food);
        }

        [HttpGet("GetAllActiveRecipes")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<List<FoodWithDetailsDto>>> GetAllActiveRecipes(CancellationToken cancellationToken)
        {
            var foods = await _mediator.Send(new GetAllActiveFoodsWithDetailsQuery(), cancellationToken);

            return new ApiResult<List<FoodWithDetailsDto>>(true, ApiResultStatusCode.Success, foods);
        }

        [HttpGet("GetFullFoodById/{id:int}", Name = "GetFullFoodById")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult<FoodWithDetailForAdminDto>> GetFullFoodById(int id, CancellationToken cancellationToken)
        {
            var food = await _mediator.Send(new GetFoodWithDetailsByIdForAdminQuery
            {
                Id = id
            }, cancellationToken);

            if (food == null)
            {
                return new ApiResult<FoodWithDetailForAdminDto>(true, ApiResultStatusCode.BadRequest, null);
            }
            return new ApiResult<FoodWithDetailForAdminDto>(true, ApiResultStatusCode.Success, food);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post(CreateFoodDto dto, CancellationToken cancellationToken)
        {
            var isExistCode = _foodRepository.TableNoTracking
                .Any(f => f.FoodCode == dto.FoodCode);
            if (isExistCode)
                throw new AppException(ApiResultStatusCode.BadRequest, "FoodCode is duplicated");

            //if ((dto.Recipe.RecipeSteps.Any() || dto.Recipe.Tips.Any()) && dto.RecipeCategoryId == null)
            //{
            //    throw new AppException(ApiResultStatusCode.BadRequest, "RecipeCategoryId is required");
            //}

            var cal = new SelectIngredient();
            if ((FoodType)dto.FoodType == FoodType.Homemade ||
                (FoodType)dto.FoodType == FoodType.Restaurant)
            {
                if (!dto.Ingredients.Any() && (FoodType)dto.FoodType != FoodType.Supermarket)
                    throw new AppException(ApiResultStatusCode.BadRequest, "Ingredients Is Required");

                cal = await _mediator.Send(new GetIngredientCalculateQuery { IngredientModels = dto.Ingredients }, cancellationToken);
            }
            else
            {
                var isExistBarcode = await _foodRepository.TableNoTracking
                    .Where(f => f.BarcodeGs1 == dto.BarcodeGs1 ||
                                f.BarcodeNational == dto.BarcodeNational)
                    .ToListAsync(cancellationToken);
                if (isExistBarcode.Any())
                {
                    return new ApiResult(false, ApiResultStatusCode.BadRequest, "The barcode is duplicated");
                }

                cal.SumWeight = 100;
                cal.IngredientCalculate = dto.Nutrients;
                dto.BakingTime = new TimeSpan(0);

            }

            var foodName = await _mediator.Send(new CreateTranslationCommand
            {
                Translation = _mapper.Map<CreateTranslationDto, Translation>(dto.Name)
            }, cancellationToken);


            var foodId = await _mediator.Send(new CreateFoodCommand
            {
                NameId = foodName.Id,
                RecipeId = null,

                IngredientCalculate = cal.IngredientCalculate,
                SumWeight = cal.SumWeight,

                NutrientValue = StringConvertor.DoubleToString(dto.Nutrients),
                BakingType = (BakingType)dto.BakingType,

                FoodType = dto.FoodType,
                Tag = dto.Tag == null ? null : dto.Tag.ToLower(),
                TagArEn = dto.TagArEn == null ? null : dto.TagArEn.ToLower(),
                IsIngredient = dto.IsIngredient,
                PersonCount = dto.PersonCount,

                ImageUri = dto.ImageUri,
                ImageThumb = dto.ImageThumb,
                BakingTime = dto.BakingTime,
                BarcodeGs1 = dto.BarcodeGs1,
                BarcodeNational = dto.BarcodeNational,
                BrandId = dto.BrandId,
                FoodCode = dto.FoodCode,
                FoodMeals = dto.FoodMealIds,
                IsActive = dto.IsActive,
                Version = dto.Version,
                IsUpdate = false,
                GI = dto.GI,
                UseInDiet = dto.UseInDiet,
                DefaultMeasureUnitId = dto.DefaultMeasureUnitId,
                IsRecipe = dto.IsRecipe,
                RecipeCategoryId = dto.RecipeCategoryId

            }, cancellationToken);

            if (dto.Recipe != null)
            {


                var recipeId = await _mediator.Send(new CreateRecipeCommand
                {
                    FoodId = foodId,
                    Status = RecipeStatus.AwaitingConfirmation
                }, cancellationToken);


                foreach (var recipeStep in dto.Recipe.RecipeSteps)
                {
                    var recipeStepName = await _mediator.Send(new CreateTranslationCommand
                    {
                        Translation = _mapper.Map<CreateTranslationDto, Translation>(recipeStep.Translation)
                    }, cancellationToken);

                    await _mediator.Send(new CreateRecipeStepCommand
                    {
                        RecipeId = recipeId,
                        DescriptionId = recipeStepName.Id
                    }, cancellationToken);
                }

                foreach (var tip in dto.Recipe.Tips)
                {
                    var tipName = await _mediator.Send(new CreateTranslationCommand
                    {
                        Translation = _mapper.Map<CreateTranslationDto, Translation>(tip.Translation)
                    }, cancellationToken);

                    await _mediator.Send(new CreateTipCommand
                    {
                        DescriptionId = tipName.Id,
                        RecipeId = recipeId
                    }, cancellationToken);
                }
            }

            //=====================Add Food MeasureUnit=========================
            if (dto.MeasureUnits.Any())
            {
                foreach (var measureUnitId in dto.MeasureUnits)
                {
                    await _mediator.Send(new CreateFoodMeasureUnitWithConditionCommand
                    {
                        FoodId = foodId,
                        MeasureUnitId = measureUnitId
                    }, cancellationToken);
                }
            }
            //=====================End Food MeasureUnit=========================

            //========================Add food Ingredient===========================
            if (dto.Ingredients.Any())
            {
                await _mediator.Send(new CreateFoodIngredientCommand
                {
                    FoodId = foodId,
                    Ingredients = dto.Ingredients
                }, cancellationToken);
            }

            //========================End food Ingredient===========================

            if (dto.NationalityIds.Any())
            {
                await _mediator.Send(new CreateFoodNationalityCommand
                {
                    FoodId = foodId,
                    NationalityIds = dto.NationalityIds
                }, cancellationToken);
            }

            if (dto.FoodCategoryIds.Any())
            {
                await _mediator.Send(new CreateFoodCategoryCommand
                {
                    FoodId = foodId,
                    CategoryIds = dto.FoodCategoryIds
                }, cancellationToken);
            }

            if (dto.DietCategoryIds.Any())
            {
                await _mediator.Send(new CreateFoodDietCategoryCommand
                {
                    FoodId = foodId,
                    FoodDietCategoryIds = dto.DietCategoryIds
                }, cancellationToken);
            }

            if (dto.FoodHabitIds.Any())
            {
                await _mediator.Send(new CreateFoodHabitCommand
                {
                    FoodId = foodId,
                    FoodHabit = dto.FoodHabitIds
                }, cancellationToken);
            }

            if (dto.SpecialDiseases.Any())
            {
                await _mediator.Send(new CreateFoodSpecialDiseasesCommand
                {
                    FoodId = foodId,
                    FoodSpecialDiseases = dto.SpecialDiseases
                }, cancellationToken);
            }

            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<ApiResult> Put(UpdateFoodDto dto, CancellationToken cancellationToken)
        {
            //var oldFood = await _mediator.Send(new GetFoodForUpdateQuery
            //{
            //    Id = dto.Id
            //},cancellationToken);

            var oldFood = await _foodRepository.GetByIdAsync(cancellationToken, dto.Id);

            if (oldFood == null)
                throw new AppException(ApiResultStatusCode.BadRequest, "food Id Not Valid");
            //if ((dto.Recipe == null || dto.Recipe.RecipeSteps == null || dto.Recipe.Tips == null) && dto.RecipeCategoryId == null)
            //{
            //    throw new AppException(ApiResultStatusCode.BadRequest, "RecipeCategoryId is required");
            //}

            //---------------------------------------Add file-----------------------------------------------------------
            if (!string.IsNullOrEmpty(oldFood.ImageUri))
            {
                _fileService.RemoveImage(oldFood.ImageUri, "FoodImage");
            }


            if (!string.IsNullOrEmpty(dto.ImageUri))
            {
                var imageName = _fileService.AddImage(dto.ImageUri, "FoodImage", dto.FoodCode.ToString());
                oldFood.ImageUri = imageName;
            }


            if (!string.IsNullOrEmpty(oldFood.ImageThumb))
            {
                _fileService.RemoveImage(oldFood.ImageThumb, "FoodThumb");
            }

            if (!string.IsNullOrEmpty(dto.ImageThumb))
            {
                var imageThumb = _fileService.AddImage(dto.ImageThumb, "FoodThumb", dto.FoodCode.ToString() + "-2");
                oldFood.ImageThumb = imageThumb;
            }

            await _mediator.Send(new UpdateNewTranslationCommand
            {

                Id = oldFood.NameId,
                Arabic = dto.Name.Arabic,
                English = dto.Name.English,
                Persian = dto.Name.Persian
            }, cancellationToken);

            var cal = new SelectIngredient();
            if ((FoodType)dto.FoodType == FoodType.Homemade ||
                (FoodType)dto.FoodType == FoodType.Restaurant)
            {
                if (dto.Ingredients == null)
                {
                    return null;
                }

                List<IngMeasurModel> ingMeasurModels = dto.Ingredients
                    .Select(i => new IngMeasurModel()
                    {
                        Id = i.Id,
                        Value = i.Value,
                        MeasureUnitId = i.MeasureUnitId
                    }).ToList();
                cal = await _mediator.Send(new GetIngredientCalculateQuery
                {
                    IngredientModels = ingMeasurModels
                }, cancellationToken);
            }
            else
            {
                cal.SumWeight = 100;
                cal.IngredientCalculate = dto.NutrientValue;
                dto.BakingTime = new TimeSpan(0);
            }


            List<double> foodNutrients = new List<double>();

            var foodFactors = Formula.FoodWeightsCalculation(new FoodParameters()
            {
                foodWeight = cal.SumWeight,
                BakingRatio = ((BakingType)dto.BakingType).ToDescription().ToDouble(),
                BakingTimeInMinute = dto.BakingTime.TotalMinutes,
                foodNutrients = cal.IngredientCalculate
            });

            if (foodFactors.AfterBaking - foodFactors.DryIngredient <= 0
                && (FoodType)dto.FoodType != FoodType.Supermarket &&
                (BakingType)dto.BakingType != BakingType.NoTypeOfBaking)
                throw new AppException(ApiResultStatusCode.BadRequest, "Your food is burnt");


            foreach (var nut in cal.IngredientCalculate)
            {
                foodNutrients.Add(nut * 100 / foodFactors.AfterBaking);
            }

            foodNutrients[1] = (foodFactors.AfterBaking - foodFactors.DryIngredient) * 100 /
                               foodFactors.AfterBaking;

            if (foodNutrients[1] <= 0 &&
                (FoodType)dto.FoodType != FoodType.Supermarket &&
                (BakingType)dto.BakingType != BakingType.NoTypeOfBaking)
            {
                throw new AppException(ApiResultStatusCode.BadRequest, "Your food is burnt");
            }

            if ((FoodType)dto.FoodType == FoodType.Supermarket)
            {
                foodNutrients = dto.NutrientValue;
            }


            oldFood.WeightBeforBaking = cal.SumWeight;
            oldFood.EvaporatedWater = foodFactors.EvaporatedWater;
            oldFood.WeightAfterBaking = foodFactors.AfterBaking;
            oldFood.BakingTime = dto.BakingTime;
            oldFood.DryIngredient = foodFactors.DryIngredient;

            oldFood.NutrientValue = StringConvertor.DoubleToString(foodNutrients);
            oldFood.Version = dto.Version;
            oldFood.BakingType = (BakingType)dto.BakingType;

            oldFood.FoodType = (FoodType)dto.FoodType;
            if (dto.BrandId > 0)
            {
                oldFood.BrandId = dto.BrandId;
            }

            oldFood.BarcodeGs1 = dto.BarcodeGs1;
            oldFood.BarcodeNational = dto.BarcodeNational;
            oldFood.IsActive = dto.IsActive;
            oldFood.FoodCode = dto.FoodCode;
            oldFood.PersonCount = dto.PersonCount;
            oldFood.Tag = dto.Tag.ToLower();
            oldFood.TagArEn = dto.TagArEn == null ? null : dto.TagArEn.ToLower();
            oldFood.IsIngredient = dto.IsIngredient;
            oldFood.IsActive = dto.IsActive;
            oldFood.IsDelete = false;
            oldFood.IsUpdate = true;
            oldFood.GI = dto.GI;
            oldFood.UseInDiet = dto.UseInDiet;
            oldFood.DefaultMeasureUnitId = dto.DefaultMeasureUnitId;
            oldFood.FoodMeals = dto.FoodMealIds;
            oldFood.IsRecipe = dto.IsRecipe;
            oldFood.RecipeCategoryId = dto.RecipeCategoryId;
            await _foodRepository.UpdateAsync(oldFood, cancellationToken);


            await _mediator.Send(new UpdateFoodCategoryCommand
            {
                FoodCategoryIds = dto.FoodCategoryIds,
                FoodId = dto.Id,
                PastFoodCategories = oldFood.FoodCategories.ToList()
            }, cancellationToken);



            await _mediator.Send(new UpdateFoodNationalityCommand
            {
                FoodId = dto.Id,
                NationalityIds = dto.NationalityIds,
                FoodNationalities = oldFood.FoodNationalities.ToList()
            }, cancellationToken);

            await _mediator.Send(new UpdateFoodHabitCommand
            {
                FoodId = dto.Id,
                FoodHabitIds = dto.FoodHabitIds,
                FoodFoodHabits = oldFood.FoodHabits.ToList()
            }, cancellationToken);

            await _mediator.Send(new UpdateFoodDietCategoryCommand
            {
                FoodId = oldFood.Id,
                DietCategoryIds = dto.DietCategoryIds,
                FoodDietCategories = oldFood.FoodDietCategories.ToList()
            }, cancellationToken);

            await _mediator.Send(new UpdateFoodSpecialDiseasesCommand
            {
                FoodId = oldFood.Id,
                OldSpecialDiseases = oldFood.FoodSpecialDiseases.ToList(),
                SpecialDiseases = dto.SpecialDiseases
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
                    if (dto.MeasureUnits.All(m => m != oldMeasureunit.MeasureUnitId))
                    {
                        deleteMeasureUnits.Add(oldMeasureunit);
                    }
                }
                await _foodMeasureUnitRepository.DeleteRangeAsync(deleteMeasureUnits, cancellationToken);
                foreach (var item in dto.MeasureUnits)
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
                await _foodMeasureUnitRepository.AddRangeAsync(foodMeasurelist, cancellationToken);
            }
            else
            {
                //---------------------------------------------------------------------------------------
                foreach (var item in dto.MeasureUnits)
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
                await _foodMeasureUnitRepository.AddRangeAsync(foodMeasurelist, cancellationToken);
            }
            //-----------------Add food Ingredient-------------------------------------------------------------------------
            var foodIngList = new List<FoodIngredient>();
            var deleteIngList = new List<FoodIngredient>();
            var updatedIngs = new List<int>();
            foreach (var oldIng in oldFood.FoodIngredients)
            {
                var existIng = dto.Ingredients
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
            if (dto.FoodType != FoodType.Supermarket)
            {
                foreach (var ing in dto.Ingredients)
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
            }

            if (dto.Recipe != null)
            {
                #region RecipeSteps
                if (dto.Recipe.RecipeSteps.Count > 0)
                {
                    foreach (var recipeStep in dto.Recipe.RecipeSteps)
                    {
                        if (recipeStep.Id == 0)
                        {
                            var recipeStepName = await _mediator.Send(new CreateTranslationCommand
                            {
                                Translation = _mapper.Map<CreateTranslationDto, Translation>(recipeStep.Translation)
                            }, cancellationToken);

                            await _mediator.Send(new CreateRecipeStepCommand
                            {
                                DescriptionId = recipeStepName.Id,
                                RecipeId = dto.Recipe.Id
                            }, cancellationToken);
                        }
                        else
                        {
                            var updatedRecipeStepName = await _mediator.Send(new UpdateTranslationCommand
                            {
                                Translation = _mapper.Map<CreateTranslationDto, Translation>(recipeStep.Translation)
                            }, cancellationToken);

                            await _mediator.Send(new UpdateRecipeStepCommand
                            {
                                Id = recipeStep.Id,
                                RecipeId = recipeStep.RecipeId,
                                DescriptionId = updatedRecipeStepName.Id
                            }, cancellationToken);
                        }
                    }
                }
                #endregion

                #region Recipe Tips
                if (dto.Recipe.Tips.Count > 0)
                {
                    foreach (var tip in dto.Recipe.Tips)
                    {
                        if (tip.Id == 0)
                        {
                            var recipeStepName = await _mediator.Send(new CreateTranslationCommand
                            {
                                Translation = _mapper.Map<CreateTranslationDto, Translation>(tip.Translation)
                            }, cancellationToken);

                            await _mediator.Send(new CreateTipCommand
                            {
                                DescriptionId = recipeStepName.Id,
                                RecipeId = dto.Recipe.Id
                            }, cancellationToken);
                        }
                        else
                        {
                            var updatedRecipeStepName = await _mediator.Send(new UpdateTranslationCommand
                            {
                                Translation = _mapper.Map<CreateTranslationDto, Translation>(tip.Translation)
                            }, cancellationToken);

                            await _mediator.Send(new UpdateTipCommand
                            {
                                Id = tip.Id,
                                RecipeId = tip.RecipeId,
                                DescriptionId = updatedRecipeStepName.Id
                            }, cancellationToken);
                        }
                    }
                }
                #endregion
            }

            if (await _redisCacheClient.Db1.ExistsAsync($"FoodV2:{oldFood.Id}"))
            {
                await _redisCacheClient.Db1.RemoveAsync($"FoodV2:{oldFood.Id}");
            }

            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}", Name = "SoftDeleteGetById")]
        public async Task<ApiResult> SoftDeleteGetByIdTask(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SoftDeleteFoodByIdCommand
            {
                Id = id
            }, cancellationToken);
            return new ApiResult(true, ApiResultStatusCode.Success);
        }


        //[HttpGet("GetFoodsForDietPlan")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ApiResult<PageResult<GetFoodsForDietPlanViewModel>>>
        //    GetAllFoodsOnDietCategories([FromQuery] List<int> dietCategoryIds, int categoryId
        //        , [FromQuery] List<int> nationalityIds)
        //{
        //    return await _mediator.Send(new GetFoodsForDietPlanQuery
        //    {
        //        DietCategoryIds = dietCategoryIds,
        //        CategoryId = categoryId,
        //        NationalityIds = nationalityIds,
        //        LanguageName = LanguageName == null ? "Persian" : LanguageName
        //    });
        //}
        [HttpPut("RecipeCategory")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> RecipeCategory(int id, int recipeCategoryId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateRecipeCategoryIdCommand
            {
                Id = id,
                RecipeCategoryIdId = recipeCategoryId
            }, cancellationToken);
            return Ok();
        }

        [HttpGet("SearchFoodNoRecipeByFilter")]
        [Authorize(Roles = "Admin")]
        public async Task<List<GetFoodNotRecipeDto>> SearchFoodNoRecipeByFilter(int page, int pageSize, [FromQuery] SearchFoodByFilterDto request, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetNotRecipeFoodByFilterQuery
            {
                FoodCode = request.FoodCode,
                Id = request.Id,
                PersianName = request.PersianName,
                Page = page,
                PageSize = pageSize,
            }, cancellationToken);

        }

    }
}
