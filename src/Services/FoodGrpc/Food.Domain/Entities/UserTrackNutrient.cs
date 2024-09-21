using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class UserTrackNutrient
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime InsertDate { get; set; }

    public string? Value { get; set; }

    public string? Id1 { get; set; }
}
