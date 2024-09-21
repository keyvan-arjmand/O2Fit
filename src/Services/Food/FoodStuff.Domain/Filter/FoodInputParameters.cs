using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;

namespace FoodStuff.Domain.Filter
{
    public class FoodInputParameters
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        //public string BarcodeNational { get; set; }
        public FoodType? FoodType { get; set; }

    }
}
