namespace Food.V2.Application.Dtos.Food;

public class FoodWeightsDto
{
    public decimal BeforeBaking { get; set; }
    public decimal AfterBaking { get; set; }
    public decimal EvaporatedWater { get; set; }
    public decimal DryIngredient { get; set; }
    public decimal Water { get; set; }
}