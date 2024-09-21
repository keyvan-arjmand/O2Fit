using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class UserTrackWater
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime InsertDate { get; set; }

    public double Value { get; set; }

    public string? Id1 { get; set; }
}
