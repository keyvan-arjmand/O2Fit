using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class FastOfflineInputCommand
    {
        public string UserId { get; set; }
        public string LocalId { get; set; }
    }
}
