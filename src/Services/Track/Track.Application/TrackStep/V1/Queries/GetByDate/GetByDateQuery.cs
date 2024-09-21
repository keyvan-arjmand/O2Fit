
using Track.Application.Dtos;

namespace Track.Application.TrackStep.V1.Queries.GetByDate;

public record GetByDateQuery(string Id, int Steps) : IRequest<List<UserStepsDto>>;