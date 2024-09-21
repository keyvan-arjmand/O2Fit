using Food.V2.Application.Dtos.Brand;
using Food.V2.Application.Dtos.DietCategory;
using Food.V2.Application.Dtos.FoodCategory;
using Food.V2.Application.Dtos.Nationality;
using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Food;

public class GetUserFoodDto
{
    public string FoodId { get; set; } = string.Empty;
    public TranslationDto Name { get; set; } = null!;
    public BakingType BakingType { get; set; }
    public TimeSpan BakingTime { get; set; }
    public string BarcodeGs1 { get; set; } = string.Empty;
    public string BarcodeNational { get; set; } = string.Empty;
    public string ImageUri { get; set; } = string.Empty;
    public string ImageThumb { get; set; } = string.Empty;
    public FoodType FoodType { get; set; }
    public List<decimal> NutrientValue { get; set; } = null!;
    public int PersonCount { get; set; }
    public string BrandId { get; set; } = string.Empty;
    public TranslationDto Brand { get; set; } = null!;
    public string DefaultMeasureUnitId { get; set; } = null!;
    public List<string> NationalityIds { get; set; } = null!;
    public List<FoodIngredientDto> Ingredients { get; set; } = null!;
    public List<MeasureUnitDto> MeasureUnits { get; set; } = null!;
}