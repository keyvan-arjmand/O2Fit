using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Dtos.Brand;

public class BrandDto
{
    public string Id { get; set; } = string.Empty;
    public string? LogoUri { get; set; }
    public string? Address { get; set; }
    public TranslationDto Translation { get; set; } = new();
}