using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Query.DietPackQuery
{
    public class GetDietPackViewModel
    {
        public int Id { get; set; }
        public FoodMeal FoodMeal { get; set; }
        public double CalorieValue { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public bool IsActive { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<int> NationalityIds { get; set; }
        public string Name { get; set; }
        public List<DietPackFoodViewModel> DietPackFoods { get; set; }

        public int CategoryId { get; set; }
        public double DailyCalorie { get; set; }
    }


    public class GetAllDietPackViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FoodMeal { get; set; }
        public double CalorieValue { get; set; }
        public List<DietCategoryViewModel> DietCategories { get; set; }
        public bool IsActive { get; set; }
        public List<NationalityViewModel> Nationalities { get; set; }
        public List<int> SpecialDisease { get; set; }
        public double DailyCalorie { get; set; }
    }

    public class GetAllDietPackDTO
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public int FoodMeal { get; set; }
        public double CaloriValue { get; set; }
        public bool IsActive { get; set; }
        public double DailyCalorie { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public List<int> NationalityIds { get; set; }
    }

    public class DietPackViewModel
    {
        public int Id { get; set; }
        public string DietPackName { get; set; }
        public int FoodMeal { get; set; }
        public double CalorieValue { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public bool IsActive { get; set; }
        public string NutrientValue { get; set; }
        public int CategoryId { get; set; }
        public SpecialDisease[] SpecialDisease { get; set; }
        public double DailyCalorie { get; set; }
    }




    public class DietCategoryViewModel
    {
        public int Id { get; set; }
        public string DietCategoryName { get; set; }
    }

    public class NationalityViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string NationalityName { get; set; }
    }
    public class AllergyIngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
