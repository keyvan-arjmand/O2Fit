using Track.Application.Dtos;

namespace Track.Application.TrackStep.V1.Queries.GetUserStepsHistory;

public record GetUserStepsHistoryQuery(string Id, int Days) : IRequest<List<UserStepsDto>>;
