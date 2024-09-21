using System.Collections.Generic;
using FoodStuff.Domain.Common;

namespace Service.DataInitializer
{
    public static class IngredientDataInitializer
    {
        public static List<IngMeasurModel> MockData()
        {
            var _mock = new List<IngMeasurModel>(){
               new IngMeasurModel() { Id = 1 ,Value = 2, MeasureUnitId = 1  },
               new IngMeasurModel() { Id = 4 ,Value = 5, MeasureUnitId = 3  },
               new IngMeasurModel() { Id = 5 ,Value = 3, MeasureUnitId = 4  },
            };

            return _mock;
        }

    }
}