namespace Workout.V2.Application.Workouts.V1.Queries.GetCardioPaginated;

public record GetCardioPaginatedQuery(int Page, int PageSize) : IRequest<PaginationResult<CardioDto>>;