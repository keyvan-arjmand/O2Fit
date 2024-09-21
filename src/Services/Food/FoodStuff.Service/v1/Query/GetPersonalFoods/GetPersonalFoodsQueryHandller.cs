using Common;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query.GetPersonalFoods
{
    class GetPersonalFoodsQueryHandller : IRequestHandler<GetPersonalFoodsQuery, List<PersonalFoodSelectDTO>>,IScopedDependency
    {
        private readonly IPersonalFoodRepository _personalFoodRepository;
        private readonly IRepositoryRedis<List<PersonalFoodSelectDTO>> _repositoryRedis;
        private readonly IFoodMeasureUnitRepository _foodMeasurUnitrepository;
        public GetPersonalFoodsQueryHandller(IPersonalFoodRepository personalFoodRepository,
            IFoodMeasureUnitRepository foodMeasurUnitrepository,
            IRepositoryRedis<List<PersonalFoodSelectDTO>> repositoryRedis)
        {
            _personalFoodRepository = personalFoodRepository;
            _repositoryRedis = repositoryRedis;
            _foodMeasurUnitrepository = foodMeasurUnitrepository;
        }
        public async Task<List<PersonalFoodSelectDTO>> Handle(GetPersonalFoodsQuery request, CancellationToken cancellationToken)
        {            
            List<PersonalFoodSelectDTO> personalFoods = await _repositoryRedis.GetAsync($"UserPersonalFoods_{request.userId}");
            if (personalFoods==null)
            {
                personalFoods = new List<PersonalFoodSelectDTO>();
                var foods = await _personalFoodRepository.GetByUserIdAsync(request.userId, cancellationToken);
                foreach (var item in foods)
                {
                    var MeasureUnits = new List<int> { 36, 37, 58 };
                    if (item.ParentFoodId > 0)
                    {
                        var foodMeasureUnits = await _foodMeasurUnitrepository.GetFoodMeasureUnits(item.ParentFoodId ?? 0, cancellationToken);
                        if (foodMeasureUnits.Count() > 0)
                        {
                            MeasureUnits.AddRange(foodMeasureUnits.Select(m => m.MeasureUnitId).ToList());
                        }
                    }
                    //--------------------------------------Food Ingredients-----------------------------------------
                    var ingredients = new List<IngredientAdminModel>();
                    foreach (var foodIng in item.PersonalFoodIngredients)
                    {
                        var ingredient = new IngredientAdminModel()
                        {
                            Id = foodIng.IngredientId,
                            Name = new Domain.Entities.Translation.Translation
                            {
                                Persian = foodIng.Ingredient.Translation.Persian,
                                Arabic = foodIng.Ingredient.Translation.Arabic,
                                English = foodIng.Ingredient.Translation.English,
                            },
                            MeasureUnitId = foodIng.MeasureUnitId,
                            Value = foodIng.IngredientValue,
                            MeasureUnitList = foodIng.Ingredient.IngredientMeasureUnits.Select(m => m.MeasureUnitId).ToList(),
                        };
                        ingredients.Add(ingredient);
                    }
                    var personalFood = new PersonalFoodSelectDTO()
                    {
                        FoodName = item.Name,
                        PersonalFoodId = item.Id,
                        BakingTime = item.BakingTime,
                        BakingType = (int)item.BakingType,
                        ImageUri = (item.ImageUri == null) ? null : Common.CommonStrings.CommonUrl + "PersonalFoodImage/" + item.ImageUri,
                        Recipe = item.Recipe,
                        IsDelete=item.Isdelete,
                        MeasureUnits = MeasureUnits,
                        NutrientValue = StringConvertor.ToNumber(item.NutrientValue),
                        Ingredients = ingredients,
                        _id = item._id
                    };
                    personalFoods.Add(personalFood);
                    await _repositoryRedis.UpdateAsync($"UserPersonalFoods_{request.userId}", personalFoods);
                };
            }
            return personalFoods;
        }
    }
}
