using Data.Repositories;
using FoodStuff.Data.Repositories;
using FoodStuff.Domain.Entities.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class IngredientSelectDTO:BaseDto<IngredientSelectDTO,Ingredient,int>
    {
        public string Name { get; set; }
        public string ThumbUri { get; set; }
        public string Code { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public IEnumerable<MeasureUnitSelectDTO> MessureUnits { get; set; }
        public string NutrientValue { get; set; }
    }
    


}
