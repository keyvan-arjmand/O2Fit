using Track.Application.Dtos;

namespace Track.Application.TrackStep.V1.Commands.InsertUserTrackSteps;

public class InsertUserTrackStepsCommand : IRequest<UserStepsDto>
{
    public string UserId { get; set; } = string.Empty;
    public DateTime InsertDate { get; set; }
    public int StepsCount { get; set; }
    public string AppId { get; set; } = string.Empty;
    public bool IsManual { get; set; }
}