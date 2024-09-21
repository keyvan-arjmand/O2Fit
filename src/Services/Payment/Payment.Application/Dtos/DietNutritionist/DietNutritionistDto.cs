namespace Payment.Application.Dtos.DietNutritionist;

public class DietNutritionistDto
{
    public TranslationDto TranslationName { get; set; } = new();
    public TranslationDto TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public double Wage { get; set; }
    public int Duration { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string DietCategoryId { get; set; } = string.Empty;
    public TranslationDto DietCategoryName { get; set; } = new();
    public bool IsActive { get; set; }
}