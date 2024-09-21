namespace Track.Application.TrackStep.V1.Commands.UpdateUserStepsByAppId;

public class UpdateUserStepsByAppIdCommand : IRequest
{
    public int StepsCount { get; set; }
    public string AppId { get; set; } = string.Empty;
    public bool IsManual { get; set; }
}