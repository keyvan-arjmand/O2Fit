namespace FoodStuff.Service.Models
{
    public class ParentDietCategoryDto
    {
        public int Id { get; set; }
        public TranslationResultDto Name { get; set; }
        public TranslationResultDto Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public bool IsPromote { get; set; }
        public string BannerImage { get; set; }
    }
}