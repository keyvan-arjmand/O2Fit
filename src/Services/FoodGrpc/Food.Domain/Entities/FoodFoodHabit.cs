using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class FoodFoodHabit
{
    public int Id { get; set; }

    public int FoodId { get; set; }

    public int FoodHabit { get; set; }

    public virtual Food Food { get; set; } = null!;
}
