using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class FoodTranslation
    {
        public int FoodId { get; set; }
        public string NamePersian { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string BrandNamePersian { get; set; }
        public string BrandNameEnglish { get; set; }
        public string BrandNameArabic { get; set; }
        public int FoodType { get; set; }
        public long FoodCode { get; set; }
    }
}
