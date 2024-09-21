using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class RecipeCategore
{
    public int Id { get; set; }

    public int NameId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public virtual Translation Name { get; set; } = null!;
}
