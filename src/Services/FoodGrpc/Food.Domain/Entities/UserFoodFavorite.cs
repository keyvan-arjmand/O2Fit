using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class UserFoodFavorite
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FoodId { get; set; }

    public string? Id1 { get; set; }

    public virtual Food Food { get; set; } = null!;
}
