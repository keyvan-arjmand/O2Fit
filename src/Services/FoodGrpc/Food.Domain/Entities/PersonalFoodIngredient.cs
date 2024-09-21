using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class PersonalFoodIngredient
{
    public int Id { get; set; }

    public int PersonalFoodId { get; set; }

    public int IngredientId { get; set; }

    public int MeasureUnitId { get; set; }

    public double IngredientValue { get; set; }

    public string? Id1 { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual MeasureUnit MeasureUnit { get; set; } = null!;

    public virtual PersonalFood PersonalFood { get; set; } = null!;
}
