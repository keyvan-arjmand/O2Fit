namespace FoodStuff.Service.Models
{
    public class UpdateRecipeStepsDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public CreateTranslationDto Translation { get; set; }
    }
}