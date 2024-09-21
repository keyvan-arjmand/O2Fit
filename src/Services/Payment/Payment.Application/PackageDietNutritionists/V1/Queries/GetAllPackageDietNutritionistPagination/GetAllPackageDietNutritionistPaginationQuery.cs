using Payment.Application.Dtos.DietNutritionist;

namespace Payment.Application.PackageDietNutritionists.V1.Queries.GetAllPackageDietNutritionistPagination;

public record GetAllPackageDietNutritionistPaginationQuery
    (string CurrencyCode, int Page, int PageSize) : IRequest<PaginationResult<DietNutritionistDto>>;