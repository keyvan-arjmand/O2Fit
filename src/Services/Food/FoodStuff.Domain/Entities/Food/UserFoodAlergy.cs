using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class UserFoodAlergy : BaseEntity
    {
        public int UserId { get; set; }
        public int IngredientId { get; set; }
        [ForeignKey(nameof(IngredientId))]
        public Ingredient Ingredient { get; set; }
        public string _id { get; set; }
    }
}
