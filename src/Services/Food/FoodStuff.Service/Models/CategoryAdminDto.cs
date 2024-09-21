namespace FoodStuff.Service.Models
{
    public class CategoryAdminDto
    {
        public int Id { get; set; }
        public TranslationResultDto NameTranslation { get; set; }

        public int? ParentId { get; set; }

        public float? Percent { get; set; }
    }
}