using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using MediatR;
using System.Collections.Generic;


namespace FoodStuff.Service.v1.Command.DietPackCommand
{
    public class CreateDietPackCommand : IRequest<Unit>
    {
        public int NameId { get; set; }
        public FoodMeal FoodMeal { get; set; }
        public double DailyCalorie { get; set; }
        public double CalorieValue { get; set; }

        public List<double> NutrientValue { get; set; }
        public List<DietPackFoodDto> DietPackFoods { get; set; }
        public bool IsActive { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public List<int> NationalityIds { get; set; }

        public int CategoryId { get; set; }


    }
}
