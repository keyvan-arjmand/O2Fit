namespace Identity.V2.Application.SpecialDiseases.V1.Queries.GetSpecialDiseaseById;

public record GetSpecialDiseaseByIdQuery(string Id) : IRequest<SpecialDiseaseDto>;
