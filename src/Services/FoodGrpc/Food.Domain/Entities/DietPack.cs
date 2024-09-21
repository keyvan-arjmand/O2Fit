using System;
using System.Collections.Generic;
using Food.Domain.Enum;

namespace Food.Domain.Entities;

public partial class DietPack
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public FoodMeal FoodMeal { get; set; }

    public double CaloriValue { get; set; }

    public string? NutrientValue { get; set; }

    public bool IsActive { get; set; }

    public int CategoryId { get; set; }

    public double DailyCalorie { get; set; }

    public virtual ICollection<DietPackAlerge> DietPackAlerges { get; set; } = new List<DietPackAlerge>();

    public virtual ICollection<DietPackDietCategory> DietPackDietCategories { get; set; } = new List<DietPackDietCategory>();

    public virtual ICollection<DietPackFood> DietPackFoods { get; set; } = new List<DietPackFood>();

    public virtual ICollection<DietPackNationality> DietPackNationalities { get; set; } = new List<DietPackNationality>();

    public virtual ICollection<DietPackSpecialDisease> DietPackSpecialDiseases { get; set; } = new List<DietPackSpecialDisease>();

    public virtual Translation Name { get; set; } = null!;

    public virtual ICollection<UserTrackDietPackDetail> UserTrackDietPackDetails { get; set; } = new List<UserTrackDietPackDetail>();
}
