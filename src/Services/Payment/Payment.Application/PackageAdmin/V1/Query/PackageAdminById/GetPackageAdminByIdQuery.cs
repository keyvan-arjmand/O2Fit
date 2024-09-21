using Payment.Application.Dtos;

namespace Payment.Application.PackageAdmin.V1.Query.PackageAdminById;

public class GetPackageAdminByIdQuery : IRequest<PackageDto>
{
    public string Id { get; set; } = string.Empty;
}