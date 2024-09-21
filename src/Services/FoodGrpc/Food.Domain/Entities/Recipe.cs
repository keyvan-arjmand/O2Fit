using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class Recipe
{
    public int Id { get; set; }

    public int Status { get; set; }

    public int FoodId { get; set; }

    public DateTime DateInsert { get; set; }

    public bool IsDelete { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();

    public virtual ICollection<Tip> Tips { get; set; } = new List<Tip>();
}
