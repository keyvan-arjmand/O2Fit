namespace Identity.V2.Application.Dtos.Countries;

public class CityDto : IDto
{
    public string Id { get; set; } = string.Empty;

    public CityTranslationDto Name { get; set; } = default!;
}