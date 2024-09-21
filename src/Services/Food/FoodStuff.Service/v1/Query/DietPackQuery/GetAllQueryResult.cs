using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
   public class GetAllQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoodMeal { get; set; }
        public int BodyType { get; set; }
        public double CaloriValue { get; set; }
        public int DietCategory { get; set; }
        public string Tag { get; set; }
        public int DietPackState { get; set; }
        public string TagArEn { get; set; }
        public string NutrientValue { get; set; }
        public List<int> PackCountries { get; set; }
        public List<DietPackFoodViewModel> DietPackFoods { get; set; }
        public List<int> DietPackAlerges { get; set; }
        public List<int> DietPackSpecialDiseases { get; set; }
        public Translation Translation { get; set; }
        public int? ParentId { get; set; }
    }
}
