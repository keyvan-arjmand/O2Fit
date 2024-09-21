using System.Collections.Generic;

namespace FoodStuff.Domain.Entities.ViewModels
{
    public class DietPackSearchResultViewModel
    {

        public int PackId { get; set; }
        public string PackName { get; set; }
        public double CalorieValue { get; set; }

    }

    public class DietPackFoodViewModel
    {
        public double MeasureUnitValue { get; set; }
        public int CategoryChildId { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int MeasureUnitId { get; set; }
        public string MeasureUnitName { get; set; }
        public int Calorie { get; set; }
        public List<double> NutrientValue { get; set; }

    }

}
