namespace Identity.V2.Application.SpecialDiseases.V1.Commands.SoftDeleteSpecialDisease;

public record SoftDeleteSpecialDiseaseCommand(string Id): IRequest;