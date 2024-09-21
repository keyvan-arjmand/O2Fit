using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.Models;
using System;
using System.Collections.Generic;

namespace FoodStuff.API.Models.ViewModels
{
    public class GetFoodsRecipeViewModel
    {
        public int Id { get; set; }
        public string _id { get; set; }


        public TranslationViewModel Name { get; set; }
        public TranslationViewModel? Recipe { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public int PersonCount { get; set; }
        public List<double> NutrientValue { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }
    }

}
