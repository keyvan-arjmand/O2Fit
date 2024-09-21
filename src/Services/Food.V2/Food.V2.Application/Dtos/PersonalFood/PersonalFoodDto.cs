namespace Food.V2.Application.Dtos.PersonalFood;

public class PersonalFoodDto
{
    public string? PersonalFoodId { get; set; }
    public string? ParentFoodId { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public BakingType BakingType { get; set; }
    public string BakingTypeName { get; set; }= string.Empty;
    public TimeSpan BakingTime { get; set; }
    public string ImageUri { get; set; }= string.Empty;
    public List<decimal> NutrientValue { get; set; }=new ();
    public List<string> MeasureUnits { get; set; }=new (); //MeasureUnits ParentFoodId
    public List<PersonalIngredientDto> Ingredients { get; set; }=new ();
    public string AppId { get; set; }
}