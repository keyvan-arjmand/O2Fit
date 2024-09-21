using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class FastFoods
    {
        public string Id { get; set; }
        public string NameId { get; set; }
        public string Persian { get; set; }
        public string Arabic { get; set; }
        public string English { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public string FoodType { get; set; }

        #region Cache Attributes
        public double Score { get; set; } = 0;
        public string Tag { get; set; }
        #endregion
    }
}
