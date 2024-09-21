namespace Workout.V2.Application.Workouts.V1.Queries.GetExercisePaginated;

public record GetExercisePaginatedQuery(int Page, int PageSize): IRequest<PaginationResult<ExerciseDto>>;