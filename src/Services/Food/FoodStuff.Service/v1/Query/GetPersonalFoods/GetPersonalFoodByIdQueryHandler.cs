using AutoMapper;
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
    public class GetPersonalFoodByIdQueryHandler : IRequestHandler<GetPersonalFoodByIdQuery, PersonalFoodSelectDTO>, IScopedDependency
    {
        private readonly IPersonalFoodIngredientRepository _personalFoodIngredientRepository;
        private readonly IRepositoryRedis<PersonalFoodSelectDTO> _personalFoodRepositoryRedis;
        private readonly IFoodMeasureUnitRepository _foodMeasurUnitrepository;
        private readonly IPersonalFoodRepository _personalFoodRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public GetPersonalFoodByIdQueryHandler(IRepositoryRedis<PersonalFoodSelectDTO> personalFoodRepositoryRedis,
            IPersonalFoodIngredientRepository personalFoodIngredientRepository,
            IFoodMeasureUnitRepository foodMeasurUnitrepository,
            IPersonalFoodRepository personalFoodRepository,
            IMediator mediator, IMapper mapper)
        {
            _personalFoodIngredientRepository = personalFoodIngredientRepository;
            _personalFoodRepositoryRedis = personalFoodRepositoryRedis;
            _foodMeasurUnitrepository = foodMeasurUnitrepository;
            _personalFoodRepository = personalFoodRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<PersonalFoodSelectDTO> Handle(GetPersonalFoodByIdQuery request, CancellationToken cancellationToken)
        {
            PersonalFoodSelectDTO personalFood = await _personalFoodRepositoryRedis.GetAsync($"PersonalFood_{request.Id}");
            if (personalFood == null)
            {
                var MeasureUnits = new List<int> { 36, 37, 58 };
                var food = await _personalFoodRepository.GetByIdAsync(request.Id, cancellationToken);
                if (food!=null)
                {
                    if (food.ParentFoodId > 0)
                    {
                        var foodMeasureUnits = await _foodMeasurUnitrepository.GetFoodMeasureUnits(food.ParentFoodId ?? 0, cancellationToken);
                        if (foodMeasureUnits.Count() > 0)
                        {
                            MeasureUnits.AddRange(foodMeasureUnits.Select(m => m.MeasureUnitId).ToList());
                        }
                    }
                    var ingredients = new List<IngredientAdminModel>();
                    //--------------------------------------Food Ingredients-----------------------------------------
                    foreach (var item in food.PersonalFoodIngredients)
                    {
                        var ing = new IngredientAdminModel();

                        ing.Id = item.IngredientId;
                        ing.Name = new Domain.Entities.Translation.Translation
                        {
                            Persian = item.Ingredient.Translation.Persian,
                            Arabic = item.Ingredient.Translation.Arabic,
                            English = item.Ingredient.Translation.English,
                        };
                        ing.MeasureUnitId = item.MeasureUnitId;
                        ing.Value = item.IngredientValue;
                        var c = item.Ingredient.IngredientMeasureUnits;
                        ing.MeasureUnitList = c.Select(m => m.MeasureUnitId).ToList();
                        ingredients.Add(ing);
                    }
                    personalFood = new PersonalFoodSelectDTO()
                    {
                        PersonalFoodId = food.Id,
                        FoodName = food.Name,
                        BakingTime = food.BakingTime,
                        BakingType = (int)food.BakingType,
                        BakingTypeName = food.BakingType.ToString(),
                        ImageUri = (food.ImageUri != null) ? Common.CommonStrings.CommonUrl + "PersonalFoodImage/" + food.ImageUri : null,
                        IsDelete = food.Isdelete,
                        Recipe = food.Recipe,
                        MeasureUnits = MeasureUnits,
                        NutrientValue = StringConvertor.ToNumber(food.NutrientValue),
                        Ingredients = ingredients,
                        ParentFoodId = food.ParentFoodId,
                        _id = food._id,
                    };
                    await _personalFoodRepositoryRedis.UpdateAsync($"PersonalFood_{request.Id}", personalFood);
                }
             }
            return personalFood;
        }
    }
}
