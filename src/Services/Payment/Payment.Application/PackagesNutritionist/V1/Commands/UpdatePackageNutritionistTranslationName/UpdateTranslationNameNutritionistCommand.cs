using Payment.Application.Dtos;
using Payment.Domain.ValueObjects;

namespace Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionistTranslationName;

public record UpdateTranslationNameNutritionistCommand(string Id, TranslationDto TranslationName) : IRequest;
