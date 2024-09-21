namespace FoodStuff.API.Models.DTOs
{
    public class GetDietPacksForNutritionistDto
    {
        public int FoodMeal { get; set; }
        public double CalorieValue { get; set; }
        public string Name { get; set; }
    }
}
