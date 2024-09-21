using Data.Repositories;
using FoodStuff.Data.Repositories;
using FoodStuff.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStuff.API.Models
{
    public class IngredientSelectModel : MeasureValuBase<double>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SearchResultSelectDTO> MeasureUnitList { get; set; }

    }
}
