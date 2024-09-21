using System;
using System.Collections.Generic;

namespace Food.Domain.Entities;

public partial class UserTrackDietPack
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsDelete { get; set; }

    public int NutritionistId { get; set; }

    public double DailyCalorie { get; set; }

    public DateTime EndDate { get; set; }

    public string? PackageName { get; set; }

    public DateTime StartDate { get; set; }

    public virtual ICollection<UserTrackDietPackDetail> UserTrackDietPackDetails { get; set; } = new List<UserTrackDietPackDetail>();
}
