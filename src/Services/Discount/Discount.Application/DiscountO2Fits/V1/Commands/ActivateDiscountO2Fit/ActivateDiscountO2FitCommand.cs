namespace Discount.Application.DiscountO2Fits.V1.Commands.ActivateDiscountO2Fit;

public record ActivateDiscountO2FitCommand(string Id, bool IsActive) : IRequest;