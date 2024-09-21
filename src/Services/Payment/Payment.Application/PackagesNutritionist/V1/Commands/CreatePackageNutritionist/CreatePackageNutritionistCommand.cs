using Common.Enums.TypeEnums;
using Payment.Application.Dtos;
using Payment.Domain.Enums;
using Payment.Domain.ValueObjects;

namespace Payment.Application.PackagesNutritionist.V1.Commands.CreatePackageNutritionist;

public class CreatePackageNutritionistCommand : IRequest<string>
{
    public TranslationDto Name { get; set; } = new();
    public TranslationDto Description { get; set; } = new();
    public double Price { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string DietCategoryId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int Sort { get; set; }
    public bool IsPromote { get; set; }
    public List<int> CountryIds { get; set; } = default!;
}