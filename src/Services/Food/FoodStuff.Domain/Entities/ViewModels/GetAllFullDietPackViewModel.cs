using FoodStuff.Domain.Enum;
using System.Collections.Generic;

namespace FoodStuff.Domain.Entities.ViewModels
{
    public class GetAllFullDietPackViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public FoodMeal FoodMeal { get; set; }
        public double DailyCalorie { get; set; }
        public double CaloriValue { get; set; }

        public string NutrientValue { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public List<int> NationalityIds { get; set; }
        public List<int> IngredientAllergyIds { get; set; }
        public List<SpecialDisease> SpecialDiseases { get; set; }

        public List<GetAllFullDietPackFood> DietPackFoods { get; set; }
    }

    public class GetAllFullDietPackFood
    {
        public double Value { get; set; }
        public int Calorie { get; set; }


        public int FoodId { get; set; }
        public string FoodName { get; set; }

        public int MeasureUnitId { get; set; }
        public string MeasureUnitName { get; set; }
    }
}
