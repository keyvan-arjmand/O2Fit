namespace Identity.V2.Application.SpecialDiseases.V1.Commands.CreateSpecialDisease;

public record CreateSpecialDiseaseCommand(CreateSpecialDiseasesTranslationDto Name) : IRequest;