using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class NutrientMeasureUnit
{
    public int Id { get; set; }

    public int Nutrient { get; set; }

    public int MeasureUnitId { get; set; }

    public virtual MeasureUnit MeasureUnit { get; set; } = null!;
}
