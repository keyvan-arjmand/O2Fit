using Common.Enums.TypeEnums;
using Payment.Application.Dtos;

namespace Payment.Application.PackageDietO2Fits.V1.Commands.InsertDietO2Fit;

public class InsertDietO2FitCommand : IRequest<string>
{
    public TranslationDto TranslationName { get; set; } = new();
    public TranslationDto TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public string CurrencyCode { get; set; } = string.Empty; 
    public bool IsActive { get; set; }
    public int Sort { get; set; }
    public bool IsPromote { get; set; }
    public DurationPackage DurationPackage { get; set; }
}