namespace FoodStuff.Service.Models
{
    public class FoodRecipeDto
    {
        public int Id { get; set; }
        public int? RecipeCategoryId { get; set; }
        public TranslationResultDto TranslationName { get; set; }
    }
}