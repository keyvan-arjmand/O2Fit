using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodStuff.API.Models.DTOs
{
    public class SearchDietPackDTO
    {
        public string Name { get; set; }
        [Required]
        public double CalorieValue { get; set; }
        [Required]
        public double DailyCalorie { get; set; }
        [Required]
        public int FoodMeal { get; set; }
        [Required]
        public List<int> DietCategoryIds { get; set; }
        public List<int> NationalityIds { get; set; }
        public List<int> SpecialDiseases { get; set; }
        public List<int> Allergies { get; set; }
        public int? Page { get; set; }
        public int PageSize { get; set; }

    }
}
