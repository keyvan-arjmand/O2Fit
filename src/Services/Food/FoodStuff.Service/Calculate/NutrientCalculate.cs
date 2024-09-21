using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utilities;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;

namespace FoodStuff.Service.Calculate
{
    public static class NutrientCalculate
    {
        public static double[] SumNutrient(double[] currentValue,
                                               List<Ingredient> SourceIngredient,
                                               List<MeasureUnit> SourceMeasureUnit,
                                               double value, int IngredientId, int MeasureUnitId,
                                               ref double SumWeight)
        {
            double[] _Response = currentValue;

            double _measureValue = SourceMeasureUnit.Where(a => a.Id == MeasureUnitId).Select(a => a.Value).First();
            string _IngredientValueNut = SourceIngredient.Where(b => b.Id == IngredientId).Select(a => a.NutrientValue).First();

            string[] _nutrient = _IngredientValueNut.Split(",");

            for (int i = 0; i < _nutrient.Length; i++)
            {
                double _nutrientValue = _nutrient[i].ToDouble();
                double _calculate = (value * _measureValue * _nutrientValue) / 100;
                _Response[i] = _Response[i] + _calculate;
            }
            
            SumWeight = SumWeight + (value * _measureValue);

            return _Response;
        }
    }
}