using Discount.Application.Dtos;

namespace Discount.Application.DiscountPackageO2Fit.V1.Query.GetAllDiscountPackageO2Fit;

public record GetAllDiscountPackageAdminQuery : IRequest<List<DiscountPackageDto>>;
