using FoodStuff.Domain.Entities.Food;


namespace FoodStuff.Service.Formula
{
    public static class Formula
    {

        public static FoodWeights FoodWeightsCalculation(FoodParameters model)
        {
            var result =new FoodWeights();
            result.Water = model.foodNutrients[1];
            result.DryIngredient = model.foodWeight - result.Water;
            if (model.BakingRatio > 0 && model.BakingTimeInMinute > 0)
            {
                result.EvaporatedWater = result.Water * model.BakingRatio * model.BakingTimeInMinute;
                result.BeforeBaking = result.DryIngredient + result.Water;
                result.AfterBaking = result.DryIngredient + result.Water - result.EvaporatedWater;
            }
            else
            {
                result.EvaporatedWater = 0;
                result.BeforeBaking = model.foodWeight;
                result.AfterBaking = model.foodWeight;
            }
            return result;
        }
    }
}
