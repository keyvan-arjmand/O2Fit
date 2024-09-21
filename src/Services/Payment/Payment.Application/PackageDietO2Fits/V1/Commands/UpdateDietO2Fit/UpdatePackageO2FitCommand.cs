using Common.Enums.TypeEnums;
using Payment.Application.Dtos;

namespace Payment.Application.PackageDietO2Fits.V1.Commands.UpdateDietO2Fit;

public class UpdatePackageO2FitCommand : IRequest
{
    public string Id { get; set; } = string.Empty;
    public TranslationDto TranslationName { get; set; } = new();
    public TranslationDto TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public DurationPackage DurationPackage { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}