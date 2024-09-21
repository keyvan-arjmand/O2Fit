using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.ViewModels
{
  public  class PersonalFoodSelectDTO
    {
        public int? PersonalFoodId { get; set; }
        public int? FoodId { get { return null; } }
        public string FoodName { get; set; }
        public string Recipe { get; set; }
        public int BakingType { get; set; }
        public string BakingTypeName { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string ImageUri { get; set; }
        // public string ImageThumb { get; set; }
        public int? ParentFoodId { get; set; }
        public bool IsDelete { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<int> MeasureUnits { get; set; }
        public List<IngredientAdminModel> Ingredients { get; set; }
        public string _id { get; set; }
    }
}
