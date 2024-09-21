﻿using FoodStuff.Domain.Common;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class FoodDTO : BaseDto<FoodDTO, Food>
    {
        [Required]
        public TranslationDto Name { get; set; }
        public TranslationDto Recipe { get; set; }
        [Required]
        public long FoodCode { get; set; }
        public int BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public List<int> FoodHabitIds { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        [Required]
        public int FoodTypeId { get; set; }
        public int? BrandId { get; set; }
        public List<int> MeasureUnits { get; set; }
        public List<IngMeasurModel> Ingredients { get; set; }
        [Required]
        public List<double> Nutrients { get; set; }
        public double Version { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public bool IsIngredient { get; set; }
        public int PersonCount { get; set; }
        public bool IsActive { get; set; }
        public double GI { get; set; }
        public bool UseInDiet { get; set; }
        public List<int> NationalityIds { get; set; }

        public FoodMeal[] FoodMealIds { get; set; }

        public List<int> FoodCategoryIds { get; set; }

        public List<int> DietCategoryIds { get; set; }
        public List<int> SpecialDiseases { get; set; }
        public int DefaultMeasureUnitId { get; set; }
        public bool IsRecipe { get; set; }
        public int RecipeCategoreId { get; set; }
    }
}
