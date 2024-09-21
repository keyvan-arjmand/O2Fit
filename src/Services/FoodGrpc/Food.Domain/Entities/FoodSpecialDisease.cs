using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class FoodSpecialDisease
{
    public int Id { get; set; }

    public int FoodId { get; set; }

    public int SpecialDisease { get; set; }

    public virtual Food Food { get; set; } = null!;
}
