namespace Identity.V2.Application.Dtos.SpecialDiseases;

public class SpecialDiseasesTranslationDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string? Persian { get; set; }
    public string? English { get; set; }
    public string? Arabic { get; set; }
}