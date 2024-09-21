using System;
using System.Collections.Generic;
using Food.Domain.Enum;

namespace Food.Domain.Entities;

public partial class Food
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public int? RecipeId { get; set; }

    public long FoodCode { get; set; }

    public int BakingType { get; set; }

    public TimeSpan BakingTime { get; set; }

    public string? BarcodeGs1 { get; set; }

    public string? BarcodeNational { get; set; }

    public string? ImageUri { get; set; }

    public string? ImageThumb { get; set; }

    public double WeightBeforBaking { get; set; }

    public double WeightAfterBaking { get; set; }

    public double EvaporatedWater { get; set; }

    public double DryIngredient { get; set; }

    public int FoodType { get; set; }

    public int? BrandId { get; set; }

    public string? NutrientValue { get; set; }

    public bool IsDelete { get; set; }

    public int? TranslationRecipeId { get; set; }

    public double Version { get; set; }

    public bool IsActive { get; set; }

    public bool IsUpdate { get; set; }

    public string? Tag { get; set; }

    public string? TagArEn { get; set; }

    public bool IsIngredient { get; set; }

    public int PersonCount { get; set; }

    public FoodMeal[]? FoodMeals { get; set; }

    public double? Gi { get; set; }

    public bool UseInDiet { get; set; }

    public int DefaultMeasureUnitId { get; set; }

    public bool IsRecipe { get; set; }

    public int IsRecipeCategoreId { get; set; }

    public int? RecipeCategoryId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<DietPackFood> DietPackFoods { get; set; } = new List<DietPackFood>();

    public virtual ICollection<FoodCategory> FoodCategories { get; set; } = new List<FoodCategory>();

    public virtual ICollection<FoodCommentAndLike> FoodCommentAndLikes { get; set; } = new List<FoodCommentAndLike>();

    public virtual ICollection<FoodDietCategory> FoodDietCategories { get; set; } = new List<FoodDietCategory>();

    public virtual ICollection<FoodFoodHabit> FoodFoodHabits { get; set; } = new List<FoodFoodHabit>();

    public virtual ICollection<FoodIngredient> FoodIngredients { get; set; } = new List<FoodIngredient>();

    public virtual ICollection<FoodMeasureUnit> FoodMeasureUnits { get; set; } = new List<FoodMeasureUnit>();

    public virtual ICollection<FoodNationality> FoodNationalities { get; set; } = new List<FoodNationality>();

    public virtual ICollection<FoodSpecialDisease> FoodSpecialDiseases { get; set; } = new List<FoodSpecialDisease>();

    public virtual Translation Name { get; set; } = null!;

    public virtual Translation? Recipe { get; set; }

    public virtual Recipe? RecipeNavigation { get; set; }

    public virtual ICollection<UserFoodFavorite> UserFoodFavorites { get; set; } = new List<UserFoodFavorite>();

    public virtual ICollection<UserTrackFood> UserTrackFoods { get; set; } = new List<UserTrackFood>();
}
