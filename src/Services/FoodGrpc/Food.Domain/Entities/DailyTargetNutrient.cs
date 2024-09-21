using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class DailyTargetNutrient
{
    public int Id { get; set; }

    public int Nutrient { get; set; }

    public int Gender { get; set; }

    public int FromAge { get; set; }

    public int ToAge { get; set; }

    public string? NutrientValue { get; set; }
}
