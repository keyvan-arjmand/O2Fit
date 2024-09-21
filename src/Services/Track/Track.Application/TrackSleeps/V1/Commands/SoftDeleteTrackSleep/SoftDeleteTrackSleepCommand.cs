namespace Track.Application.TrackSleeps.V1.Commands.SoftDeleteTrackSleep;

public record SoftDeleteTrackSleepCommand(string Id) : IRequest;