using FoodStuff.Domain.Enum;
using System.Runtime.Intrinsics.X86;

namespace FoodStuff.API.Models.DTOs
{
    public class SearchFoodByFilterDto
    {
        public int Id { get; set; }
        public long FoodCode { get; set; }
        public string PersianName { get; set; }
        public RecipeStatus? RecipeStatus { get; set; }

    }
}