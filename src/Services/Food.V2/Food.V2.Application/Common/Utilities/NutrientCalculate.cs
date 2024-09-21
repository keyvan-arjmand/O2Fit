namespace Food.V2.Application.Common.Utilities;

public static class NutrientCalculate
{
    public static decimal[] SumNutrient(decimal[] currentValue,
        List<decimal> sourceIngredient,
        List<decimal> sourceMeasureUnit,
        decimal value,// int ingredientId, int measureUnitId,
        ref decimal sumWeight)
    {
        decimal[] response = currentValue;

        decimal measureValue = sourceMeasureUnit.First();
        var ingredientValueNut = sourceIngredient;


        for (int i = 0; i < ingredientValueNut.Count; i++)
        {
            var nutrientValue = ingredientValueNut[i];
            var calculate = (value * measureValue * nutrientValue) / 100;
            response[i] += calculate;
        }
            
        sumWeight += (value * measureValue);

        return response;
    }
}