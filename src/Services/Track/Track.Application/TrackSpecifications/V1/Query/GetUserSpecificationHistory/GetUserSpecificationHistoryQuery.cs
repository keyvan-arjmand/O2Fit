using Track.Application.Dtos;

namespace Track.Application.TrackSpecifications.V1.Query.GetUserSpecificationHistory;

public record GetUserSpecificationHistoryQuery
    (string UserId, DateTime DateTime) : IRequest<List<UserTrackSpecificationDto>>;