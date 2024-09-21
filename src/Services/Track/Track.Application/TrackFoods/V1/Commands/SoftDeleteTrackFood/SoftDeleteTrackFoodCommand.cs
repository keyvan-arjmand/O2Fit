namespace Track.Application.TrackFoods.V1.Commands.SoftDeleteTrackFood;

public record SoftDeleteTrackFoodCommand(string Id):IRequest;