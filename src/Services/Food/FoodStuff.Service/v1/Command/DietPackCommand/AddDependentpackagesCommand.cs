using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Service.v1.Command.DietPackCommand
{
   public class AddDependentpackagesCommand:IRequest<Domain.Entities.Diet.DietPack>
    {
        public int Id { get; set; }
        public int NameId { get; set; }
        public FoodMeal FoodMeal { get; set; }
        public BodyType BodyType { get; set; }
        public double CaloriValue { get; set; }
        public int DietPackState { get; set; }
        public List<double> NutrientValue { get; set; }
        public int DietCategory { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public List<int> PackCountries { get; set; }
        public List<DietPackFoodDto> DietPackFoods { get; set; }
        //public List<int> DietPackAlerges { get; set; }
        public List<int> DietPackSpecialDiseases { get; set; }
    }
}
