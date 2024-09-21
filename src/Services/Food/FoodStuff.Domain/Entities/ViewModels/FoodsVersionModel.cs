using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.ViewModels
{
  public class FoodsVersionModel
    {
        public List<FoodViewModel> Foods { get; set; }
        public double Version { get; set; }
    }
}
