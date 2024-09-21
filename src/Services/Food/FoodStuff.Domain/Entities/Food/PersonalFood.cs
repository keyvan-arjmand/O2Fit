using Domain;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Enum;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class PersonalFood : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Recipe { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string ImageUri { get; set; }
        public string ThumbUri { get; set; }
        public double WeightBeforBaking { get; set; }
        public double WeightAfterBaking { get; set; }
        public double EvaporatedWater { get; set; }
        public double DryIngredient { get; set; }
        public double Water { get; set; }
        public int? ParentFoodId { get; set; }
        public bool IsCopyable { get; set; }
        public string NutrientValue { get; set; }
        public bool Isdelete { get; set; }
        public DateTime Insertdate{ get; set; }
        public ICollection<UserTrackFood> UserTrackFoods { get; set; }
        public ICollection<PersonalFoodIngredient> PersonalFoodIngredients { get; set; }
        public string _id { get; set; }
    }
}
