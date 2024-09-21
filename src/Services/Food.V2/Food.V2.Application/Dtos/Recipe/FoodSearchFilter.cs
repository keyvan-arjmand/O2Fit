using Food.V2.Domain.Enums;

namespace Food.V2.Application.Dtos.Recipe;

public class FoodSearchFilter
{
    public string? Id { get; set; }
    public string? FoodCode { get; set; }
    public string? Name { get; set; }
    public RecipeStatus? RecipeStatus { get; set; }
    public string? Language { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}