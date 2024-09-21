using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class FoodMeasureUnit
{
    public int FoodId { get; set; }

    public int MeasureUnitId { get; set; }

    public int Id { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual MeasureUnit MeasureUnit { get; set; } = null!;
}
