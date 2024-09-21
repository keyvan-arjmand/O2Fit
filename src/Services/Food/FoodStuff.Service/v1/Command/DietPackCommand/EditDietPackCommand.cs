using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using MediatR;
using System.Collections.Generic;

namespace FoodStuff.Service.v1.Command.DietPackCommand
{
    public class EditDietPackCommand : IRequest<Domain.Entities.Diet.DietPack>
    {
        public int Id { get; set; }
        public int NameId { get; set; }
        public FoodMeal FoodMeal { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<int> DietCategoryIds { get; set; }
        public List<FoodStuff.Domain.Entities.ViewModels.DietPackFoodDto> DietPackFoods { get; set; }
        public double CalorieValue { get; set; }
        public bool IsActive { get; set; }
        public List<int> NationalityIds { get; set; }
        public double DailyCalorie { get; set; }

    }
}
