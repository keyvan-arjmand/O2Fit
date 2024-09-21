namespace Food.V2.Application.Dtos.Ingredients;

public class CalculatedIngredientDto
{
    public List<decimal> CalculatedIngredient { get; set; } = new();
    public decimal SumWeight { get; set; }
}