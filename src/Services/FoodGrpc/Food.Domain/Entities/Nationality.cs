using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class Nationality
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<DietPackNationality> DietPackNationalities { get; set; } = new List<DietPackNationality>();

    public virtual ICollection<FoodNationality> FoodNationalities { get; set; } = new List<FoodNationality>();

    public virtual Translation Name { get; set; } = null!;
}
