namespace Food.V2.Application.Dtos.Ingredients;

public class SearchIngredientByNamePaginated
{
    public string Name { get; set; } = string.Empty;
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}