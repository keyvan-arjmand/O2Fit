using Payment.Application.Dtos;
using Payment.Domain.Enums;
using Payment.Domain.ValueObjects;

namespace Payment.Application.PackagesNutritionist.V1.Commands.UpdatePackageNutritionist;

public record UpdatePackageNutritionistCommand(string Id, TranslationDto Name, TranslationDto Description, double Price, string CurrencyCode, bool IsActive, int Sort, bool IsPromote, string DietCategoryId, List<int> CountryIds) : IRequest;