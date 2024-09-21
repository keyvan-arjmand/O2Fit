using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class IngredientAllergy
{
    public int Id { get; set; }

    public int IngredientId { get; set; }

    public bool IsDelete { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;
}
