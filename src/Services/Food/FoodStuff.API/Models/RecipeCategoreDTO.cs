using FoodStuff.Domain.Entities.Food;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class RecipeCategoreDTO : BaseDto<RecipeCategoreDTO, RecipeCategore>
    {
        public TranslationDto Name { get; set; }
    }
}
