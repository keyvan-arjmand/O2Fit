using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class DietAlergy
{
    public int Id { get; set; }

    public int MainAlergyId { get; set; }

    public int IngredientAlergyId { get; set; }

    public virtual Ingredient MainAlergy { get; set; } = null!;
}
