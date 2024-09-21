using Payment.Application.Dtos;

namespace Payment.Application.PackageAdmin.V1.Query.PackagePaginationAdmin;

public class GetAllPackageAdminPaginationQuery : IRequest<PaginationResult<PackageDto>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}