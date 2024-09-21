using Food.V2.Application.Dtos.PersonalFood;

namespace Food.V2.Application.PersonalFoods.V1.Commands.UpdatePersonalFood;

public class UpdatePersonalFoodCommand : IRequest<PersonalFoodDto>
{
    public string Id { get; set; } = string.Empty;
    // public string UserId { get; set; } = string.Empty;
    public string FoodName { get; set; } = string.Empty;
    public BakingType BakingType { get; set; }
    public TimeSpan BakingTime { get; set; }
    public string ImageUri { get; set; } = string.Empty;
    public string? ParentFoodId { get; set; }
    public List<IngredientMeasureUnitDto> Ingredients { get; set; } = new();
    // public string AppId { get; set; } = string.Empty;
}