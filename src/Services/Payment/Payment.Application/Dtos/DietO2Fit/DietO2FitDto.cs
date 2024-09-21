using Common.Enums.TypeEnums;

namespace Payment.Application.Dtos.DietO2Fit;

public class DietO2FitDto
{
    public TranslationDto TranslationName { get; set; } = new();
    public TranslationDto TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public double Wage { get; set; }
    public int Duration { get; set; }
    public string CurrencyCode { get; set; } = string.Empty; 
    public bool IsActive { get; set; }
    public int Sort { get; set; }
    public bool IsPromote { get; set; }
}