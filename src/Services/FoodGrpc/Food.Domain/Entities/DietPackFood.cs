using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class DietPackFood
{
    public int Id { get; set; }

    public double Value { get; set; }

    public int FoodId { get; set; }

    public int MeasureUnitId { get; set; }

    public int DietPackId { get; set; }

    public int Calorie { get; set; }

    public int CategoryChildId { get; set; }

    public string? NutrientValue { get; set; }

    public virtual DietPack DietPack { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;

    public virtual MeasureUnit MeasureUnit { get; set; } = null!;
}
