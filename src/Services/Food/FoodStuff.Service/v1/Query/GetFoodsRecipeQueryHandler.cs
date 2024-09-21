using Common;
using Data.Contracts;
using FoodStuff.API.Models.ViewModels;
using FoodStuff.Common.Utilities;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Service.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query
{
    public class GetFoodsRecipeQueryHandler : IRequestHandler<GetFoodsRecipeQuery, List<GetFoodsRecipeViewModel>>, IScopedDependency
    {
        private readonly IRepository<Food> _repository;

        public GetFoodsRecipeQueryHandler(IRepository<Food> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetFoodsRecipeViewModel>> Handle(GetFoodsRecipeQuery request, CancellationToken cancellationToken)
        {
            return await _repository.TableNoTracking
                  .Include(fi => fi.FoodIngredients)
                  .ThenInclude(i => i.Ingredient)
                  .ThenInclude(itr => itr.Translation)
                  .Include(fi => fi.FoodIngredients)
                  .ThenInclude(i => i.Ingredient)
                  .ThenInclude(imu => imu.IngredientMeasureUnits)
                  .ThenInclude(mu => mu.MeasureUnit)
                  .ThenInclude(mutr => mutr.Translation)
                  .Where(f => f.IsRecipe).Select(s => new GetFoodsRecipeViewModel
                  {
                      BakingTime = s.BakingTime,
                      BakingType = s.BakingType,
                      Id = s.Id,
                      ImageThumb = s.ImageThumb,
                      ImageUri = s.ImageUri,
                      Name = new TranslationViewModel
                      {
                          Arabic = s.TranslationName.Arabic,
                          English = s.TranslationName.English,
                          Persian = s.TranslationName.Persian,
                      },
                      _id = s.Id.ToString(),
                      PersonCount = s.PersonCount,
                      NutrientValue = StringConvertor.ToNumber(s.NutrientValue),
                      Ingredients = s.FoodIngredients
                      .Select(ins => new IngredientViewModel
                      {
                          Id = ins.Ingredient.Id,
                          Name = new TranslationViewModel
                          {
                              Arabic = ins.Ingredient.Translation.Arabic,
                              English = ins.Ingredient.Translation.English,
                              Persian = ins.Ingredient.Translation.Persian,
                          },
                          MeasureUnitId = ins.MeasureUnitId,
                          Value = ins.IngredientValue,
                          MeasureUnitList = ins.Ingredient.IngredientMeasureUnits
                          .Select(fis => new Service.Models.MeasureUnitViewModel
                          {
                              Id = fis.MeasureUnit.Id,
                              MeasureUnitName = new TranslationViewModel
                              {
                                  Arabic = fis.MeasureUnit.Translation.Arabic,
                                  English = fis.MeasureUnit.Translation.English,
                                  Persian = fis.MeasureUnit.Translation.Persian,
                              }
                              ,
                              Value = fis.MeasureUnit.Value,
                          }).Distinct().ToList(),
                      }).ToList(),
                      Recipe = s.RecipeId != null ? new TranslationViewModel
                      {

                          Persian = s.TranslationRecipe.Persian,
                          English = s.TranslationRecipe.English,
                          Arabic = s.TranslationRecipe.Arabic,
                      } : null,
                  }).ToListAsync(cancellationToken);
        }
    }
}
