using Track.Application.Dtos;

namespace Track.Application.TrackStep.V1.Queries.GetUserSteps;

public record GetUserStepsQuery(string Id, DateTime Date) : IRequest<List<UserStepsDto>>;