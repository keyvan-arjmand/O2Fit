using FoodStuff.Domain.Enum;

namespace FoodStuff.Service.Models
{
    public class ChangeRecipeStatusDto
    {
        public int Id { get; set; }
        public RecipeStatus Status { get; set; }
    }
}