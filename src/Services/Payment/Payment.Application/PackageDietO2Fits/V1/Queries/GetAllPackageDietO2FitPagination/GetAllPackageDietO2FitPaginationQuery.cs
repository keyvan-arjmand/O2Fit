using Payment.Application.Dtos.DietNutritionist;
using Payment.Application.Dtos.DietO2Fit;

namespace Payment.Application.PackageDietO2Fits.V1.Queries.GetAllPackageDietO2FitPagination;

public record GetAllPackageDietO2FitPaginationQuery
    (string CurrencyCode, int Page, int PageSize) : IRequest<PaginationResult<DietO2FitDto>>;