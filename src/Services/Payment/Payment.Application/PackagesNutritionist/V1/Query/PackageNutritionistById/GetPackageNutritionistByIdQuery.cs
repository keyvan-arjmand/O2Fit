using Payment.Application.Dtos;

namespace Payment.Application.PackagesNutritionist.V1.Query.PackageNutritionistById;

public class GetPackageNutritionistByIdQuery : IRequest<PackageDto>
{
    public string Id { get; set; } = string.Empty;
}