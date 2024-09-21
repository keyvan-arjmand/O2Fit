using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class FoodCommentAndLike
{
    public int Id { get; set; }

    public int FoodId { get; set; }

    public string? Comment { get; set; }

    public string? Language { get; set; }

    public bool Like { get; set; }

    public bool AdminConfirmed { get; set; }

    public virtual Food Food { get; set; } = null!;
}
