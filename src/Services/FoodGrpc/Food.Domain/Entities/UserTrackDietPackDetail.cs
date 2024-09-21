using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class UserTrackDietPackDetail
{
    public int Id { get; set; }

    public int Repeat { get; set; }

    public int Meal { get; set; }

    public int DietPackId { get; set; }

    public int UserTrackDietPackId { get; set; }

    public virtual DietPack DietPack { get; set; } = null!;

    public virtual UserTrackDietPack UserTrackDietPack { get; set; } = null!;
}
