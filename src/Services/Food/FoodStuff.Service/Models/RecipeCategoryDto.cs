namespace FoodStuff.Service.Models
{
    public class RecipeCategoryDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }

        public TranslationResultDto Translation { get; set; }
    }
}