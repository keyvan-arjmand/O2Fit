using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class Tip
{
    public int Id { get; set; }

    public DateTime DateInsert { get; set; }

    public int? RecipeId { get; set; }

    public bool IsDelete { get; set; }

    public int DescriptionId { get; set; }

    public virtual Translation Description { get; set; } = null!;

    public virtual Recipe? Recipe { get; set; }
}
