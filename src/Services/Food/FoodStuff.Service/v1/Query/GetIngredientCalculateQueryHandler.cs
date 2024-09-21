using Common;
using Data.Contracts;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Service.Calculate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Query
{
    public class GetIngredientCalculateQueryHandler : IRequestHandler<GetIngredientCalculateQuery, SelectIngredient>, ITransientDependency
    {
        private readonly IRepository<Ingredient> _ingredientRepository;
        private readonly IRepository<MeasureUnit> _measureUnitRepository;

        public GetIngredientCalculateQueryHandler(IRepository<Ingredient> ingredientRepository, 
            IRepository<MeasureUnit> measureUnitRepository)
        {
            _ingredientRepository = ingredientRepository;
            _measureUnitRepository = measureUnitRepository;
        }

        public async Task<SelectIngredient> Handle(GetIngredientCalculateQuery request, CancellationToken cancellationToken)
        {
            SelectIngredient CalculateValue = new SelectIngredient();

            double[] _result = new double[34];

            List<int> IngredientIds = new List<int>();
            List<int> MeasureUnitsIds = new List<int>();
            double _SumWeight = 0;

            foreach (var item in request.IngredientModels)
            {
                IngredientIds.Add(item.Id);
                MeasureUnitsIds.Add(item.MeasureUnitId);
            }

            List<Ingredient> _Ingredients = _ingredientRepository.TableNoTracking.Where(x => IngredientIds.Contains(x.Id)).ToList();
            List<MeasureUnit> _MeasureUnits = _measureUnitRepository.TableNoTracking.Where(a => MeasureUnitsIds.Contains(a.Id)).ToList();

            foreach (var item in request.IngredientModels)
            {
                _result = NutrientCalculate.SumNutrient(_result, _Ingredients, _MeasureUnits, item.Value, item.Id, item.MeasureUnitId, ref _SumWeight);
            }
            CalculateValue.IngredientCalculate = _result.ToList();
            CalculateValue.SumWeight = _SumWeight;
            return CalculateValue;
        }
    }
}
