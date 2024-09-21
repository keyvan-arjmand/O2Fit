using Data.Repositories;
using FoodStuff.Data.Repositories;
using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.MeasureUnit;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class FoodSelectDTO
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string Recipe { get; set; }
        public long FoodCode { get; set; }
        public int BakingType { get; set; }
        public string BakingTypeName { get; set; }
        public TimeSpan BakingTime { get; set; }
        // public string FoodHabit { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public int FoodType { get; set; }
        public string FoodTypeName { get; set; }
        public string Brand { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<SearchResultSelectDTO> MeasureUnits { get; set; }
        public List<IngredientSelectModel> Ingredients { get; set; }
    }
   
}
