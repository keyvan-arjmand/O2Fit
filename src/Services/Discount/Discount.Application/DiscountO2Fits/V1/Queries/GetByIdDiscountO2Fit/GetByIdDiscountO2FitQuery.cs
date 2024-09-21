using Discount.Application.Dtos;

namespace Discount.Application.DiscountO2Fits.V1.Queries.GetByIdDiscountO2Fit;

public record GetByIdDiscountO2FitQuery(string Id):IRequest<DiscountO2FitDto>;