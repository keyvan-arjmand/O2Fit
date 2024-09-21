using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStuff.Service.Models
{
    public class DietCategoryResultDto
    {
        public int Id { get; set; }
        
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public string BannerImage { get; set; }
        public bool IsPromote { get; set; }
        public TranslationResultDto Name { get; set; }
        public TranslationResultDto Description { get; set; }
        public DietCategoryResultDto ParentCategory { get; set; }
    }
}