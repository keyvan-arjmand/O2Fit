namespace Identity.V2.Application.Dtos.SpecialDiseases;

public class SpecialDiseaseDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public SpecialDiseaseDto Name { get; set; } = default!;
}