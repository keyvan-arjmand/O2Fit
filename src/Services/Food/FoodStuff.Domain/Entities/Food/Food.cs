using Domain;
using FoodStuff.Domain.Entities.Diet;
using FoodStuff.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStuff.Domain.Entities.Food
{
    public class Food : BaseEntity
    {
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation TranslationName { get; set; }

        public int? RecipeId { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public Translation.Translation TranslationRecipe { get; set; }
        public long FoodCode { get; set; }
        public BakingType BakingType { get; set; }
        public TimeSpan BakingTime { get; set; }
        public string BarcodeGs1 { get; set; }
        public string BarcodeNational { get; set; }
        public string ImageUri { get; set; }
        public string ImageThumb { get; set; }
        public double WeightBeforBaking { get; set; }
        public double WeightAfterBaking { get; set; }
        public double EvaporatedWater { get; set; }
        public double DryIngredient { get; set; }
        public FoodType FoodType { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public bool IsUpdate { get; set; }
        public string Tag { get; set; }
        public string TagArEn { get; set; }
        public string NutrientValue { get; set; }
        public int? BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }
        public double Version { get; set; }
        public bool IsIngredient { get; set; }
        public int PersonCount { get; set; }
        public FoodMeal[] FoodMeals { get; set; }

        public double GI { get; set; }

        public bool UseInDiet { get; set; }

        public int DefaultMeasureUnitId { get; set; }

        public bool IsRecipe { get; set; }
        public int IsRecipeCategoreId { get; set; }
        public int? RecipeCategoryId { get; set; }
        //[ForeignKey(nameof(RecipeCategoryId))]
        //public RecipeCategore RecipeCategory { get; set; }
        public Recipe Recipe { get; set; }


        public ICollection<UserTrackFood> UserTrackFoods { get; set; }
        public ICollection<FoodMeasureUnit> FoodMeasureUnits { get; set; }
        public ICollection<FoodIngredient> FoodIngredients { get; set; }
        public ICollection<DietPackFood> DietPackFoods { get; set; }
        public ICollection<FoodCommentAndLike> foodCommentAndLikes { get; set; }

        public ICollection<FoodFoodHabit> FoodHabits { get; set; }

        public ICollection<FoodNationality> FoodNationalities { get; set; }

        public ICollection<FoodCategory> FoodCategories { get; set; }

        public ICollection<FoodDietCategory> FoodDietCategories { get; set; }
        public ICollection<FoodSpecialDisease> FoodSpecialDiseases { get; set; }
    }
}
