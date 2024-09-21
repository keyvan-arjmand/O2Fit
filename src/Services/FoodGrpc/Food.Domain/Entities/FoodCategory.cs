using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class FoodCategory
{
    public int FoodId { get; set; }

    public int CategoryId { get; set; }

    public int Id { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Food Food { get; set; } = null!;
}
