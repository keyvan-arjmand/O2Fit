using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class UserFoodFavorite : BaseEntity
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
        [ForeignKey(nameof(FoodId))]
        public Food Food { get; set; }
        public string _id { get; set; }
    }
}
