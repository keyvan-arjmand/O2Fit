using Payment.Application.Dtos;

namespace Payment.Application.PackageDietNutritionists.V1.Commands.InsertDietNutritionist;

public class InsertDietNutritionistCommand : IRequest<string>
{
    public TranslationDto TranslationName { get; set; } = new();
    public TranslationDto TranslationDescription { get; set; } = new();
    public double Price { get; set; }
    public string CurrencyCode { get; set; } = string.Empty; 
    public ObjectId DietCategoryId { get; set; }
    public bool IsActive { get; set; }
}