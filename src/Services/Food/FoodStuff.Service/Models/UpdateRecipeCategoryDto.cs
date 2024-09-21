namespace FoodStuff.Service.Models
{
    public class UpdateRecipeCategoryDto
    {
        public int Id { get; set; }
        public CreateTranslationDto Translation { get; set; }
        public bool IsActive { get; set; }
    }
}