using Payment.Application.Dtos;
using Payment.Domain.ValueObjects;

namespace Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionistTranslationDescription;

public record UpdateTranslationDescriptionNutritionistCommand
    (string Id, TranslationDto TranslationDescription) : IRequest;