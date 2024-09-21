namespace FoodStuff.Service.Models
{
    public class CreateFoodIngredientDto
    {
        public int FoodId { get; set; }
        public int IngredientId { get; set; }
        public int MeasureUnitId { get; set; }
        public double IngredientValue { get; set; }

    }
}