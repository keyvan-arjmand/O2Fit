using FoodStuff.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.ViewModels
{
   public class MeasureUnitsAndVersionSelectDTO
    {
        public List<MeasureUnitModelDTO> measureUnits { get; set; }
        public double Version { get; set; }
    }
}
