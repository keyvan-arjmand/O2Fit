namespace Workout.V2.Application.Workouts.V1.Queries.GetBodyBuildingPaginated;

public record GetBodyBuildingPaginatedQuery(int Page, int PageSize) : IRequest<PaginationResult<BodyBuildingDto>>;