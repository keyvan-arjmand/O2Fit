using Common;
using Data.Contracts;
using FoodStuff.Common;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.GetVersion
{
    public class GetVersionQueryHandller : IRequestHandler<GetVersionQuery, GetVersionQueryResult>, ITransientDependency
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IRepositoryRedis<FoodsVersionModel> _repositoryRedis;
        public GetVersionQueryHandller(IFoodRepository foodRepository, IRepositoryRedis<FoodsVersionModel> repositoryRedis)
        {
            _foodRepository = foodRepository;
            _repositoryRedis = repositoryRedis;
        }
        public async Task<GetVersionQueryResult> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        {
            var Version = request.lastVersion + 0.01;
            List<Food> _foodList = new List<Food>();
            for (int i = 0; i < 2; i++)
            {
                _foodList = await _foodRepository.Table
                      .Include(ft => ft.TranslationName)
                      .Include(fr => fr.TranslationRecipe)
                      .Include(fi => fi.FoodIngredients).ThenInclude(i => i.Ingredient).ThenInclude(t => t.Translation)
                      .Include(f => f.FoodIngredients).ThenInclude(fi => fi.Ingredient).ThenInclude(im => im.IngredientMeasureUnits)
                      .Include(fm => fm.FoodMeasureUnits).ThenInclude(m => m.MeasureUnit).ThenInclude(mt => mt.Translation)
                      .Include(fb => fb.Brand).ThenInclude(b => b.Translation)
                      .Where(f => f.Version == Version && f.IsActive == true).ToListAsync(cancellationToken);
                if (_foodList.Count() == 0)
                {
                    var nextFoods = await _foodRepository.TableNoTracking.Where(f => f.Version > request.lastVersion).ToListAsync(cancellationToken);
                    if (nextFoods.Count() > 0)
                    {
                        Version = nextFoods.Select(v => v.Version).Min();
                    }
                    else
                    {
                        Version = request.lastVersion;
                        break;
                    }
                }
            }

            List<FoodViewModel> foodList = new List<FoodViewModel>();
            foreach (var food in _foodList)
            {
                //--------------------------------------Food Ingredients-----------------------------------------
                var ingredients = new List<IngredientAdminModel>();
                foreach (var item in food.FoodIngredients)
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
                        MeasureUnitList = item.Ingredient.IngredientMeasureUnits.Select(m => m.MeasureUnitId).Distinct().ToList(),
                    };
                    ingredients.Add(ing);
                }
                //-------------------------------------Food MeasureUnits------------------------------------------
                List<int> measureUnits = new List<int>();
                measureUnits = (new int[] { 36, 37, 58, }).ToList();
                if (food.FoodMeasureUnits != null)
                {
                    List<int> extraMeasureunits = food.FoodMeasureUnits.Select(m => m.MeasureUnitId).ToList();
                    measureUnits.AddRange(extraMeasureunits);
                }
                measureUnits = measureUnits.Distinct().ToList();
                //-------------------------------------------------------------------------------------------------
                var Food = new FoodViewModel()
                {
                    FoodId = food.Id,
                    _id = food.Id.ToString(),
                    Name = new TranslationViewModel()
                    {
                        Persian = food.TranslationName.Persian,
                        English = food.TranslationName.English,
                        Arabic = food.TranslationName.Arabic
                    },
                    BakingType = food.BakingType,
                    FoodType = food.FoodType,
                    ImageUri = (food.ImageUri != null) ? CommonStrings.CommonUrl + "FoodImage/" + food.ImageUri : null,
                    NutrientValue = StringConvertor.ToNumber(food.NutrientValue),
                    Brand = (food.BrandId > 0) ? new TranslationViewModel()
                    {
                        Arabic = food.Brand.Translation.Arabic,
                        English = food.Brand.Translation.English,
                        Persian = food.Brand.Translation.Persian

                    } : null,
                    Version = food.Version,
                    IsUpdate = food.IsUpdate,
                    BakingTime = food.BakingTime,
                    BarcodeGs1 = food.BarcodeGs1,
                    BarcodeNational = food.BarcodeNational,
                    MeasureUnits = measureUnits,
                    Ingredients = ingredients
                };
                foodList.Add(Food);
            }
            FoodsVersionModel foodsVersion = new FoodsVersionModel()
            {
                Foods = foodList,
                Version = Version,
            };

            await _repositoryRedis.UpdateAsync($"FoodsVersion_{Version}", foodsVersion);
            return new GetVersionQueryResult()
            {
                foodsVersionModel = foodsVersion
            };
        }
    }
}
