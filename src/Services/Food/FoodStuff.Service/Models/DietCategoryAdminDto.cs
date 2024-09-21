namespace FoodStuff.Service.Models
{
    public class DietCategoryAdminDto
    {
        public int Id { get; set; }
        public TranslationResultDto NameTranslation { get; set; }
        public TranslationResultDto DescriptionTranslation { get; set; }
        public string Image { get; set; }
        public int? ParentId { get; set; }
    }
}