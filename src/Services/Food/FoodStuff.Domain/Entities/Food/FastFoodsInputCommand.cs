using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class FastFoodsInputCommand
    {
        public string Name { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public string FoodType { get; set; }
    }
}
