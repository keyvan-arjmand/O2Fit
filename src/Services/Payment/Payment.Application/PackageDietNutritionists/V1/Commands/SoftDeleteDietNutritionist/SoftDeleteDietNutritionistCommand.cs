namespace Payment.Application.PackageDietNutritionists.V1.Commands.SoftDeleteDietNutritionist;

public record SoftDeleteDietNutritionistCommand(string Id) : IRequest;