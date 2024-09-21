using Discount.Application.Dtos;

namespace Discount.Application.DiscountO2Fits.V1.Commands.ApplyDiscountCodeO2Fit;

public record ApplyDiscountCodeO2FitCommand(string DiscountCode, string PackageId, int UserCountryId, string UserId,
    string UserCurrencyCode) : IRequest<ApplyDiscountCodeO2FitResult>;