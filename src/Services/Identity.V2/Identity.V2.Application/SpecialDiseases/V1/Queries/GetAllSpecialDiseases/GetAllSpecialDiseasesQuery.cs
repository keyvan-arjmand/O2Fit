namespace Identity.V2.Application.SpecialDiseases.V1.Queries.GetAllSpecialDiseases;

public record GetAllSpecialDiseasesQuery() : IRequest<List<SpecialDiseaseDto>>;