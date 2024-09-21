using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class FoodNationality
{
    public int FoodId { get; set; }

    public int NationalityId { get; set; }

    public int Id { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual Nationality Nationality { get; set; } = null!;
}
