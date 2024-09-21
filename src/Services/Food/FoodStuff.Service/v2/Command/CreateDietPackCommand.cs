using System;
using System.Collections.Generic;
using System.Text;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using MediatR;

namespace FoodStuff.Service.v2.Command
{
    public class CreateDietPackCommand : IRequest<Unit>
    {
        public int NameId { get; set; }
        public FoodMeal FoodMeal { get; set; }
        public double CaloriesValue { get; set; }
        public int DietCategory { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<int> PackCountries { get; set; }
        public List<DietPackFoodDto> DietPackFoods { get; set; }
        public bool IsActive { get; set; }
        public int[] NationalityIds { get; set; }

    }
}
