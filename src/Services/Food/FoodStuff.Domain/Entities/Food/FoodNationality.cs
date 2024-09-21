using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace FoodStuff.Domain.Entities.Food
{
    public class FoodNationality : BaseEntity
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int NationalityId { get; set; }
        public Nationality Nationality { get; set; }
    }
}
