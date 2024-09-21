using Common.Enums.TypeEnums;
using Discount.Application.Dtos;

namespace Discount.Application.DiscountNutritionists.V1.Commands.GenerateDiscountNutritionist;



public class GenerateDiscountNutritionistCommand:IRequest
{
    public TranslationDto Description { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDateTime { get; set; }
    public string? UserId { get; set; }
    public List<int>? CountryIds { get; set; }
    public int UsableCount { get; set; }
    public bool IsActive { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public double Amount { get; set; }
}