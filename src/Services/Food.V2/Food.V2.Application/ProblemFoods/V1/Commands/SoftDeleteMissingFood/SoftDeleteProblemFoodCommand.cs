namespace Food.V2.Application.ProblemFoods.V1.Commands.SoftDeleteMissingFood;

public record SoftDeleteProblemFoodCommand(string Id) : IRequest;