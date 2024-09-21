using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class UserFoodAlergy
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int IngredientId { get; set; }

    public string? Id1 { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;
}
