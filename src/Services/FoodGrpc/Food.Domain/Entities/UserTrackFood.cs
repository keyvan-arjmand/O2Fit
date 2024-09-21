using System;
using System.Collections.Generic;
using Food.Domain.Enum;

namespace Food.Domain.Entities;

public partial class UserTrackFood
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? FoodId { get; set; }

    public float Value { get; set; }

    public int MeasureUnitId { get; set; }

    public FoodMeal FoodMeal { get; set; }

    public DateTime InsertDate { get; set; }

    public string? FoodNutrientValue { get; set; }

    public int? PersonalFoodId { get; set; }

    public string? Id1 { get; set; }

    public virtual Food? Food { get; set; }

    public virtual MeasureUnit MeasureUnit { get; set; } = null!;

    public virtual PersonalFood? PersonalFood { get; set; }
}
