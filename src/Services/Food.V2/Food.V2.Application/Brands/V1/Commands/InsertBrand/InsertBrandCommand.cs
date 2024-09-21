using Food.V2.Application.Dtos.Translation;

namespace Food.V2.Application.Brands.V1.Commands.InsertBrand;

public class InsertBrandCommand:IRequest
{
    public string? LogoUri { get; set; }

    public string? Address { get; set; }
    public TranslationDto Translation { get; set; } = new();
}