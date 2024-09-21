using Track.Application.Dtos;

namespace Track.Application.TrackSpecifications.V1.Query.GetLastUserTrackSpecification;

public record GetLastUserTrackSpecificationQuery(string Id) : IRequest<List<UserTrackSpecificationDto>>;
