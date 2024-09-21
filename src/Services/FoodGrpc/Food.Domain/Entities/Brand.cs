using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class Brand
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public string? LogoUri { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Food> Foods { get; set; } = new List<Food>();

    public virtual Translation Name { get; set; } = null!;
}
