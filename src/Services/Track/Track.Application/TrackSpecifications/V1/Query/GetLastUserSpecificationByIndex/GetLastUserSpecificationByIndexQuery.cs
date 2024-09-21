using Track.Application.Dtos;

namespace Track.Application.TrackSpecifications.V1.Query.GetLastUserSpecificationByIndex;

public record GetLastUserSpecificationByIndexQuery(string Id, int Index):IRequest<UserTrackSpecificationDto>;