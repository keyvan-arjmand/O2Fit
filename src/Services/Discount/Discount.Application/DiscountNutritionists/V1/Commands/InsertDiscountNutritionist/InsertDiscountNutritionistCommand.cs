using Common.Enums.TypeEnums;
using Discount.Application.Dtos;

namespace Discount.Application.DiscountNutritionists.V1.Commands.InsertDiscountNutritionist;

public class InsertDiscountNutritionistCommand : IRequest
{
    public string Code { get; set; } = string.Empty;
    public TranslationDto Description { get; set; } = default!;
    public DateTime StartDate { get; set; }
    public DateTime EndDateTime { get; set; }
    public PackageType PackageType { get; set; }
    public string? UserId { get; set; }
    public List<int>? CountryIds { get; set; }
    public int UsableCount { get; set; }
    public bool IsActive { get; set; }
    public double Amount { get; set; }
}