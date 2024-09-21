using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common;
using Data.Contracts;
using FoodStuff.Common;
using FoodStuff.Common.Utilities;
using FoodStuff.Service.Models;
using FoodStuff.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Query.Food
{
    public class GetFoodWithDetailsByIdForAdminQueryHandler : IRequestHandler<GetFoodWithDetailsByIdForAdminQuery , FoodWithDetailForAdminDto>, IScopedDependency
    {
        private readonly IRepository<Domain.Entities.Food.Food> _repository;
        private readonly IMapper _mapper;

        public GetFoodWithDetailsByIdForAdminQueryHandler(IRepository<Domain.Entities.Food.Food> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FoodWithDetailForAdminDto> Handle(GetFoodWithDetailsByIdForAdminQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var food = await _repository.TableNoTracking.Include(x => x.FoodIngredients).ThenInclude(x => x.Ingredient)
                       .ThenInclude(x => x.Translation).Include(x => x.FoodIngredients).ThenInclude(x => x.MeasureUnit).ThenInclude(x=>x.Translation)
                       .Include(x => x.FoodDietCategories).ThenInclude(x => x.DietCategory).ThenInclude(x => x.NameTranslation)
                       .Include(x => x.FoodDietCategories).ThenInclude(x => x.DietCategory).ThenInclude(x => x.DescriptionTranslation)
                       .Include(x => x.FoodCategories).ThenInclude(x => x.Category).ThenInclude(x => x.NameTranslation)
                       .Include(x => x.TranslationName).Include(x => x.FoodNationalities).ThenInclude(x => x.Nationality).ThenInclude(x => x.NameTranslation)
                       .Include(x => x.TranslationRecipe).Include(x => x.Brand).ThenInclude(x => x.Translation)
                       //.Include(x => x.RecipeCategory)
                       .Include(x => x.Recipe).ThenInclude(x => x.RecipeSteps).ThenInclude(x => x.Translation)
                       .Include(x => x.Recipe).ThenInclude(x => x.Tips).ThenInclude(x => x.Translation).Include(x => x.FoodMeasureUnits)
                       .Include(x => x.FoodIngredients).ThenInclude(x => x.Ingredient).ThenInclude(x => x.IngredientMeasureUnits)
                       .Include(x => x.FoodHabits).Include(x => x.FoodSpecialDiseases)
                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (food == null)
                {
                    return null;
                }

                var result = _mapper.Map<Domain.Entities.Food.Food, FoodWithDetailForAdminDto>(food);


                foreach (var item in food.FoodIngredients)
                {
                    var ing = new IngredientAdminDto()
                    {
                        Id = item.IngredientId,
                        Name = new TranslationResultDto
                        {
                            Id = item.Ingredient.Translation.Id,
                            Persian = item.Ingredient.Translation.Persian,
                            Arabic = item.Ingredient.Translation.Arabic,
                            English = item.Ingredient.Translation.English
                        },
                        Value = item.IngredientValue,
                        Code = item.Ingredient.Code,
                        NutrientValue = StringConvertor.ToNumber(item.Ingredient.NutrientValue),
                        ThumbUri = CommonStrings.CommonUrl + "Ingimg/" + item.Ingredient.ThumbUri,
                        MeasureUnit = new MeasureUnitAdminDto
                        {
                            Id = item.MeasureUnitId,
                            Persian = item.MeasureUnit.Translation.Persian,
                            English = item.MeasureUnit.Translation.English,
                            Arabic = item.MeasureUnit.Translation.Arabic,
                            MeasureUnitCategory = item.MeasureUnit.MeasureUnitCategory,
                            Value = item.MeasureUnit.Value
                        }
                    };
                    if (result.Ingredients == null)
                        result.Ingredients = new List<IngredientAdminDto>();

                    result.Ingredients.Add(ing);
                }

                List<int> measureUnits = new List<int> { 36, 37, 58 };
                if (food.FoodMeasureUnits.Any())
                {
                    List<int> extraMeasureUnits = food.FoodMeasureUnits
                        .Select(m => m.MeasureUnitId).Distinct().ToList();
                    measureUnits.AddRange(extraMeasureUnits);
                }
                result.MeasureUnits = measureUnits;


                if (!string.IsNullOrEmpty(food.ImageUri))
                {
                    result.ImageUri = await ConvertImage.GetImageAsBase64Url("FoodImage/" + food.ImageUri);
                }
                if (!string.IsNullOrEmpty(food.ImageThumb))
                {
                    result.ImageThumb = await ConvertImage.GetImageAsBase64Url("FoodThumb/" + food.ImageThumb);
                }

                result.FoodHabitIds = food.FoodHabits.Select(x => x.FoodHabit).ToList();
                //result.NutrientValue = StringConvertor.ToNumber(food.NutrientValue);
                result.SpecialDiseases = food.FoodSpecialDiseases.Select(x => x.SpecialDisease).ToList();

                return result;
            }
            catch (System.Exception e)
            {

                throw e;
            }
        }
    }
}