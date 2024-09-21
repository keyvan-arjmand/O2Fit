namespace Payment.Application.PackagesNutritionist.V1.Commands.SoftDeletePackageNutritionist;

public record DeletePackageNutritionistCommand(string Id) : IRequest;