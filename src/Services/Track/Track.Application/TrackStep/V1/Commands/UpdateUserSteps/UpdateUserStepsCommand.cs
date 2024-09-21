namespace Track.Application.TrackStep.V1.Commands.UpdateUserSteps;

public class UpdateUserStepsCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public int StepsCount { get; set; }
    public bool IsManual { get; set; }
}