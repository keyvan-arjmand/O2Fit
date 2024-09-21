using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Service.Models;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class DietCategoryDTO : BaseDto<DietCategoryDTO, DietCategory>
    {
        public TranslationDto Name { get; set; }
        public TranslationDto Description { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public bool IsPromote { get; set; }
        public string BannerImage { get; set; }
        public string BannerImageUri { get; set; }
        public string ImageUri { get; set; }

    }
}
