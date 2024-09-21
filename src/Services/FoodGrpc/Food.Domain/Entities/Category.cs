using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class Category
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public int? ParentId { get; set; }

    public float? Percent { get; set; }

    public virtual ICollection<FoodCategory> FoodCategories { get; set; } = new List<FoodCategory>();

    public virtual Translation Name { get; set; } = null!;
}
