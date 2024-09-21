using Payment.Application.Dtos;

namespace Payment.Application.PackagesNutritionist.V1.Query.PackagePagination;

public class GetAllPackageNutritionistPaginationQuery : IRequest<PaginationResult<PackageDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}