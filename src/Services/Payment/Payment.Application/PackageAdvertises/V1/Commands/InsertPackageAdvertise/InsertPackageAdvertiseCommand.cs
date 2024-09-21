using Common.Enums.TypeEnums;
using Payment.Application.Dtos;

namespace Payment.Application.PackageAdvertises.V1.Commands.InsertPackageAdvertise;

public class InsertPackageAdvertiseCommand:IRequest
{
    public TranslationDto TranslationTitle { get; set; } = new();
    public TranslationDto? TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public double Cost { get; set; }//per click
    public string CurrencyCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public AdvertisePackageType AdvertisePackageType { get; set; }
}