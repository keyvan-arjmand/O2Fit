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
    public class PersonalFoodIngredientDTO : BaseDto<PersonalFoodIngredientDTO, PersonalFoodIngredient>
    {
        public int PersonalFoodId { get; set; }
        public int IngredientId { get; set; }
        public int MeasureUnitId { get; set; }
        public double IngredientValue { get; set; }
    }
}
