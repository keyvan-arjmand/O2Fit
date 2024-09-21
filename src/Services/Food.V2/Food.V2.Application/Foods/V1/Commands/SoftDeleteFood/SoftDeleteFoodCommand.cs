namespace Food.V2.Application.Foods.V1.Commands.SoftDeleteFood;

public record SoftDeleteFoodCommand(string Id) : IRequest;