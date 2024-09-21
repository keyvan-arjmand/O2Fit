using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class DietPackNationality
{
    public int Id { get; set; }

    public int NationalityId { get; set; }

    public int DietPackId { get; set; }

    public virtual DietPack DietPack { get; set; } = null!;

    public virtual Nationality Nationality { get; set; } = null!;
}
