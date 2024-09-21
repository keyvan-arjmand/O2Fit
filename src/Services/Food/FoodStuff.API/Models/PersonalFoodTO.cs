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
    public class PersonalFoodDTO : BaseDto<PersonalFoodDTO, PersonalFood>
    {
        public int UserId { get; set; }
        public string FoodName { get; set; }
        //public string Recipe { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string ImageUri { get; set; }
        //public string ThumbUri { get; set; }
        public int? ParentFoodId { get; set; }
       // public List<MeasureUnit> MeasureUnits { get; set; }
        public List<IngMeasurModel> Ingredients { get; set; }
        public string _id { get; set; }
    }
}
