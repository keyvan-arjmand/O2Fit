using Food.V2.Application.Dtos.Nationality;

namespace Food.V2.Application.Nationalities.V1.Queries.GetAllNationalityPagination;

public record GetAllNationalityPaginationQuery(int Page, int PageSize):IRequest<List<NationalityDto>>;