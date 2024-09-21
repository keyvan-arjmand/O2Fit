using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class PersonalFood
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Name { get; set; }

    public int BakingType { get; set; }

    public TimeSpan BakingTime { get; set; }

    public string? ImageUri { get; set; }

    public string? ThumbUri { get; set; }

    public double WeightBeforBaking { get; set; }

    public double WeightAfterBaking { get; set; }

    public double EvaporatedWater { get; set; }

    public double DryIngredient { get; set; }

    public double Water { get; set; }

    public int? ParentFoodId { get; set; }

    public bool IsCopyable { get; set; }

    public string? NutrientValue { get; set; }

    public bool Isdelete { get; set; }

    public DateTime Insertdate { get; set; }

    public string? Id1 { get; set; }

    public string? Recipe { get; set; }

    public virtual ICollection<PersonalFoodIngredient> PersonalFoodIngredients { get; set; } = new List<PersonalFoodIngredient>();

    public virtual ICollection<UserTrackFood> UserTrackFoods { get; set; } = new List<UserTrackFood>();
}
