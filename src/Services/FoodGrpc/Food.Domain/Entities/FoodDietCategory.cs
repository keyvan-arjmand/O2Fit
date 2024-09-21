using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class FoodDietCategory
{
    public int FoodId { get; set; }

    public int DietCategoryId { get; set; }

    public int Id { get; set; }

    public virtual DietCategory DietCategory { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;
}
