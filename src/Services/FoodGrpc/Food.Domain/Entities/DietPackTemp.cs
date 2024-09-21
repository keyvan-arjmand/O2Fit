using System;
using System.Collections.Generic;
using Food.Domain.Enum;

namespace Food.Domain.Entities;

public partial class DietPackTemp
{
    public int? Id { get; set; }

    public int? NameId { get; set; }

    public FoodMeal? FoodMeal { get; set; }

    public double? CaloriValue { get; set; }

    public string? NutrientValue { get; set; }

    public bool? IsActive { get; set; }

    public int? CategoryId { get; set; }

    public double? DailyCalorie { get; set; }

    public int? AllergyId { get; set; }
}
