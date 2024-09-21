using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class IngredientMeasureUnit
{
    public int IngredientId { get; set; }

    public int MeasureUnitId { get; set; }

    public int Id { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual MeasureUnit MeasureUnit { get; set; } = null!;
}
