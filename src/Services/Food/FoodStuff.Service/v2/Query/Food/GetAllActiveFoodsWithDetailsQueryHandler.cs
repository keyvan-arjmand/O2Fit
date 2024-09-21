using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.Models;
using FoodStuff.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetAllActiveFoodsWithDetailsQueryHandler : IRequestHandler<GetAllActiveFoodsWithDetailsQuery, List<FoodWithDetailsDto>>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Food> _repository;
        private readonly IMapper _mapper;
        public GetAllActiveFoodsWithDetailsQueryHandler(IRepository<Domain.Entities.Food.Food> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<FoodWithDetailsDto>> Handle(GetAllActiveFoodsWithDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var foods = await _repository.TableNoTracking.Include(x => x.FoodIngredients).ThenInclude(x => x.Ingredient)
              .ThenInclude(x => x.Translation).Include(x => x.FoodIngredients).ThenInclude(x => x.MeasureUnit)
              .Include(x => x.FoodDietCategories).ThenInclude(x => x.DietCategory).ThenInclude(x => x.NameTranslation)
              .Include(x => x.FoodDietCategories).ThenInclude(x => x.DietCategory).ThenInclude(x => x.DescriptionTranslation)
              .Include(x => x.FoodCategories).ThenInclude(x => x.Category).ThenInclude(x => x.NameTranslation)
              .Include(x => x.TranslationName).Include(x => x.FoodNationalities).ThenInclude(x => x.Nationality).ThenInclude(x => x.NameTranslation)
              .Include(x => x.TranslationRecipe).Include(x => x.Brand).ThenInclude(x => x.Translation)
              //.Include(x => x.RecipeCategory)
              .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x => x.Translation)
              .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x => x.Translation).Include(x => x.FoodMeasureUnits)
              .Include(x => x.FoodIngredients).ThenInclude(x => x.Ingredient).ThenInclude(x => x.IngredientMeasureUnits)
              .Include(x=>x.FoodHabits).Where(x=>x.Recipe.Status== RecipeStatus.Approved)
              .ToListAsync(cancellationToken);

                var result = _mapper.Map<List<Domain.Entities.Food.Food>, List<FoodWithDetailsDto>>(foods);

                for (var index = 0; index < foods.Count; index++)
                {
                    var food = foods[index];
                    foreach (var item in food.FoodIngredients)
                    {
                        var ing = new IngredientDto
                        {
                            Id = item.IngredientId,
                            Name = new TranslationResultDto
                            {
                                Id = item.Ingredient.Translation.Id,
                                Persian = item.Ingredient.Translation.Persian,
                                Arabic = item.Ingredient.Translation.Arabic,
                                English = item.Ingredient.Translation.English
                            },
                            MeasureUnitId = item.MeasureUnitId,
                            Value = item.IngredientValue,
                            MeasureUnitList = item.Ingredient.IngredientMeasureUnits.Select(m => m.MeasureUnitId)
                                .Distinct().ToList(),
                        };
                        if (result[index].Ingredients == null)
                        {
                            result[index].Ingredients = new List<IngredientDto>();
                        }
                        result[index].Ingredients.Add(ing);
                    }
                    result[index].BakingTimeInMinutes = food.BakingTime.Minutes;

                    var originalCalories = double.Parse(food.NutrientValue.Split(',')[23]);
                    result[index].Calories = (int)originalCalories;

                    List<int> measureUnits = new List<int> { 36, 37, 58 };
                    if (food.FoodMeasureUnits.Any())
                    {
                        List<int> extraMeasureUnits = food.FoodMeasureUnits
                            .Select(m => m.MeasureUnitId).Distinct().ToList();
                        measureUnits.AddRange(extraMeasureUnits);
                    }
                    result[index].MeasureUnits = measureUnits;
                    result[index].FoodHabitIds = food.FoodHabits.Select(s => s.FoodHabit).ToList();
                    result[index].ImageUri = "FoodImage/" + food.ImageUri;
                    result[index].ImageThumb= "FoodThumb/" + food.ImageThumb;

                }

                return result;

            }
            catch (System.Exception exception)
            {

                throw exception;
            }
        }
    }
}