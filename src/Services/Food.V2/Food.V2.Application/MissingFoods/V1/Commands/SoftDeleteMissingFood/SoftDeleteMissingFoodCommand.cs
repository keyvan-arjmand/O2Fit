namespace Food.V2.Application.MissingFoods.V1.Commands.SoftDeleteMissingFood;

public record SoftDeleteMissingFoodCommand(string Id) : IRequest;