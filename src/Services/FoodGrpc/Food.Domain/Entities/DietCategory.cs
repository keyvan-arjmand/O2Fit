using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class DietCategory
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public int? DescriptionId { get; set; }

    public string? Image { get; set; }

    public int? ParentId { get; set; }

    public bool IsActive { get; set; }

    public bool IsPromote { get; set; }

    public string? BannerImage { get; set; }

    public virtual Translation? Description { get; set; }

    public virtual ICollection<DietPackDietCategory> DietPackDietCategories { get; set; } = new List<DietPackDietCategory>();

    public virtual ICollection<FoodDietCategory> FoodDietCategories { get; set; } = new List<FoodDietCategory>();

    public virtual Translation Name { get; set; } = null!;
}
