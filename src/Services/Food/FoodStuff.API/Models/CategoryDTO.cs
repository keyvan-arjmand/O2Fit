using FoodStuff.Domain.Entities.Food;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class CategoryDTO : BaseDto<CategoryDTO, Category>
    {
        public TranslationDto NameTranslation { get; set; }

        public int? ParentId { get; set; }

        public float? Percent { get; set; }
    }

    public class DeleteCategoryDTO
    {
        public int Id { get; set; }

    }
}
