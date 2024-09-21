using Identity.V2.Application.Dtos.UserTrackSpecification;

namespace Identity.V2.Application.Dtos.Users;

public class ProfileDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public FoodHabit FoodHabit { get; set; }
    public double? WeightChangeRate { get; set; }
    public int? WeightTimeRange { get; set; }
    public HeightSize HeightSize { get; set; } = null!;
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string? ImageUri { get; set; }
    public DateTime? PkExpireDate { get; set; }
    public DateTime? DietPkExpireDate { get; set; }
    public DailyActivityRate DailyActivityRate { get; set; }
    public TargetDto Target { get; set; } = null!;
}