using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
   public class FoodCommentAndLike : BaseEntity
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public string Comment { get; set; }
        public string Language { get; set; }
        public bool Like { get; set; }
        public bool AdminConfirmed { get; set; }
    }
}
