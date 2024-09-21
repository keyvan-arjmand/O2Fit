namespace Food.V2.Application.Common.Utilities;

public static class Formula
{
    public static FoodWeightsDto FoodWeightsCalculation(this FoodParametersDto model)
    {
        var result = new FoodWeightsDto();
        result.Water = model.FoodNutrients[1];
        result.DryIngredient = model.FoodWeight - result.Water;
        if (model.BakingRatio > 0 && model.BakingTimeInMinute > 0)
        {
            result.EvaporatedWater = result.Water * model.BakingRatio * model.BakingTimeInMinute;
            result.BeforeBaking = result.DryIngredient + result.Water;
            result.AfterBaking = result.DryIngredient + result.Water - result.EvaporatedWater;
        }
        else
        {
            result.EvaporatedWater = 0;
            result.BeforeBaking = model.FoodWeight;
            result.AfterBaking = model.FoodWeight;
        }

        return result;
    }
}