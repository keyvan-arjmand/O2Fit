using Common.Enums.Foods;
using EventBus.Messages.Contracts.Services.Foods.MeasureUnit;

namespace EventBus.Messages.Contracts.Services.Foods.Food;

public class GetFoodResult
{
    public string Id { get; init; }
    public string FoodCode { get; init; }
    public int FoodType { get; init; }
    public int FoodMeal { get; init; }
    public List<decimal> NutrientValue { get; init; }
    public string FoodNamePersian { get; init; }
    public string FoodNameEnglish { get; init; }
    public string FoodNameArabic { get; init; }
    public List<MeasureUnitFood> MeasureUnits { get; init; }
}