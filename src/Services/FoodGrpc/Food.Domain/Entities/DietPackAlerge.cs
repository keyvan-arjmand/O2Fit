using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class DietPackAlerge
{
    public int Id { get; set; }

    public int IngredientId { get; set; }

    public int DietPackId { get; set; }

    public virtual DietPack DietPack { get; set; } = null!;

    public virtual Ingredient Ingredient { get; set; } = null!;
}
