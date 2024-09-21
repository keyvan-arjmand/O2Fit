namespace Workout.V2.Application.Workouts.V1.Queries.GetCardioById;

public record GetCardioByIdQuery(string Id) : IRequest<CardioDto>;