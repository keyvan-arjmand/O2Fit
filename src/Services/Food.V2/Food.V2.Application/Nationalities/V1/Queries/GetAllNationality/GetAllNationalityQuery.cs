using Food.V2.Application.Dtos.Nationality;

namespace Food.V2.Application.Nationalities.V1.Queries.GetAllNationality;

public record GetAllNationalityQuery : IRequest<List<NationalityDto>>;