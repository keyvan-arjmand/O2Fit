namespace Track.Application.TrackStep.V1.Commands.SoftDeleteUserSteps;

public record SoftDeleteUserStepsCommand(string Id) : IRequest;