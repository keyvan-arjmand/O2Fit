using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class MeasureUnit
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public double Value { get; set; }

    public int MeasureUnitCategory { get; set; }

    public double Version { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<DietPackFood> DietPackFoods { get; set; } = new List<DietPackFood>();

    public virtual ICollection<FoodIngredient> FoodIngredients { get; set; } = new List<FoodIngredient>();

    public virtual ICollection<FoodMeasureUnit> FoodMeasureUnits { get; set; } = new List<FoodMeasureUnit>();

    public virtual ICollection<IngredientMeasureUnit> IngredientMeasureUnits { get; set; } = new List<IngredientMeasureUnit>();

    public virtual Translation Name { get; set; } = null!;

    public virtual ICollection<NutrientMeasureUnit> NutrientMeasureUnits { get; set; } = new List<NutrientMeasureUnit>();

    public virtual ICollection<PersonalFoodIngredient> PersonalFoodIngredients { get; set; } = new List<PersonalFoodIngredient>();

    public virtual ICollection<UserTrackFood> UserTrackFoods { get; set; } = new List<UserTrackFood>();
}
