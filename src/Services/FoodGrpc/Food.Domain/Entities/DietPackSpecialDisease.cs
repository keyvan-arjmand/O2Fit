using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class DietPackSpecialDisease
{
    public int Id { get; set; }

    public int DietPackId { get; set; }

    public int SpecialDisease { get; set; }

    public virtual DietPack DietPack { get; set; } = null!;
}
