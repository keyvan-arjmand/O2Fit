using Discount.Application.Dtos;

namespace Discount.Application.DiscountPackageO2Fit.V1.Query.GetByIdDiscountPackageO2Fit;

public record GetByIdDiscountPackageO2FitQuery(string Id) : IRequest<DiscountPackageDto>;
