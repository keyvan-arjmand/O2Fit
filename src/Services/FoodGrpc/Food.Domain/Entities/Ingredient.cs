using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class Ingredient
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public string? ThumbUri { get; set; }

    public string? Code { get; set; }

    public string? NutrientValue { get; set; }

    public bool IsFood { get; set; }

    public string? Tag { get; set; }

    public string? TagArEn { get; set; }

    public string? TagAr { get; set; }

    public string? TagEn { get; set; }

    public int DefaultMeasureUnitId { get; set; }

    public virtual ICollection<DietAlergy> DietAlergies { get; set; } = new List<DietAlergy>();

    public virtual ICollection<DietPackAlerge> DietPackAlerges { get; set; } = new List<DietPackAlerge>();

    public virtual ICollection<FoodIngredient> FoodIngredients { get; set; } = new List<FoodIngredient>();

    public virtual ICollection<IngredientAllergy> IngredientAllergies { get; set; } = new List<IngredientAllergy>();

    public virtual ICollection<IngredientMeasureUnit> IngredientMeasureUnits { get; set; } = new List<IngredientMeasureUnit>();

    public virtual Translation Name { get; set; } = null!;

    public virtual ICollection<PersonalFoodIngredient> PersonalFoodIngredients { get; set; } = new List<PersonalFoodIngredient>();

    public virtual ICollection<UserFoodAlergy> UserFoodAlergies { get; set; } = new List<UserFoodAlergy>();
}
