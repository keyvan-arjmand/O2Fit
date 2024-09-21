using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class DietDTO
    {
        public int BodyType { get; set; }
        public int UserId { get; set; }
        public double Calori { get; set; }
        public double ZCalori { get; set; }
        public int country { get; set; }
        public int Category { get; set; }
        public DateTime StartDate { get; set; }
        public List<int> Alergies { get; set; }
    }
}
