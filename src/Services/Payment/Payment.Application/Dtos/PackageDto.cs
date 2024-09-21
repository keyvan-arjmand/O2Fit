using Common.Enums.TypeEnums;

namespace Payment.Application.Dtos;

public class PackageDto
{
    public string Id { get; set; } = string.Empty;
    public double DiscountPrice { get; set; }
    public double Wage { get; set; }
    public TranslationDto? TranslationName { get; set; }
    public TranslationDto? TranslationDescription { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<int> CountryIds { get; set; } = default!;
    public double Price { get; set; }
    public int Duration { get; set; }
    public string CurrencyId { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string DietCategoryId { get; set; } = string.Empty;
    public TranslationDto DietCategoryName { get; set; } = new();
    public string PackageType { get; set; } = string.Empty; //package type enum
    public bool IsActive { get; set; }
    public int Sort { get; set; }
    public bool IsPromote { get; set; }
}