using Food.V2.Application.Dtos.Brand;

namespace Food.V2.Application.Brands.V1.Queries.SearchByName;

public class SearchBrandByNameQuery : IRequest<List<BrandDto>>
{
    public string Name { get; set; } = string.Empty;
    public string? Language { get; set; } = string.Empty;
}