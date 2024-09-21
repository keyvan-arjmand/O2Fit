namespace Identity.V2.Application.SpecialDiseases.V1.Commands.UpdateSpecialDisease;

public record UpdateSpecialDiseaseCommand(string Id, CreateSpecialDiseasesTranslationDto Name) : IRequest;