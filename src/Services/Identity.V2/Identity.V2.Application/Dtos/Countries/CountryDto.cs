namespace Identity.V2.Application.Dtos.Countries;

public class CountryDto: IDto
{
    public string Id { get; set; } = string.Empty;
    public int CountryId { get; set; }
    public string? FlagIcon { get; set; }
    public string Alpha { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string Culture { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string CurrencyName { get; set; } = string.Empty;
    public string UtcTime { get; set; } = string.Empty;
    public CountryTranslationDto Translation { get; set; } = null!;
}