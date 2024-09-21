namespace Identity.V2.Application.Dtos.SpecialDiseases;

public class CreateSpecialDiseasesTranslationDto : IDto
{
    public string? Persian { get; set; }
    public string? English { get; set; }
    public string? Arabic { get; set; }
}