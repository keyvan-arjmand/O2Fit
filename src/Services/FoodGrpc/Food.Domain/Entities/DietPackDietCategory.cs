using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class DietPackDietCategory
{
    public int Id { get; set; }

    public int DietCategoryId { get; set; }

    public int DietPackId { get; set; }

    public virtual DietCategory DietCategory { get; set; } = null!;

    public virtual DietPack DietPack { get; set; } = null!;
}
