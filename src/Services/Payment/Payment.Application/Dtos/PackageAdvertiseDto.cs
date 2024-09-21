using Common.Enums.TypeEnums;
using EventBus.Messages.Contracts.Services.Foods;

namespace Payment.Application.Dtos;

public class PackageAdvertiseDto
{
    public Translation TranslationName { get; set; } = new();
    public Translation? TranslationDescription { get; set; } = new();
    public List<string> CityIds { get; set; } = new();
    public int ViewCount { get; set; }
    public double Price { get; set; }
    public string PackageType { get; set; } = string.Empty;
    public string CurrencyId { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public AdvertisePackageType AdvertisePackageType { get; set; }
}