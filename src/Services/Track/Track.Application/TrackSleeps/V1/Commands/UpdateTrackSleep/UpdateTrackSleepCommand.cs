using Track.Application.Dtos;
using Track.Domain.Enums;

namespace Track.Application.TrackSleeps.V1.Commands.UpdateTrackSleep;

public class UpdateTrackSleepCommand:IRequest<UserTrackSleepDto>
{
    public string Id { get; set; } = string.Empty;
    public int Rate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string AppId { get; set; } = string.Empty;
}