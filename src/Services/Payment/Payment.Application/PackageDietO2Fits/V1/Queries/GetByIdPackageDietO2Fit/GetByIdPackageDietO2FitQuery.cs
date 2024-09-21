using Payment.Application.Dtos.DietNutritionist;
using Payment.Application.Dtos.DietO2Fit;

namespace Payment.Application.PackageDietO2Fits.V1.Queries.GetByIdPackageDietO2Fit;

public record GetByIdPackageDietO2FitQuery(string Id, string CurrencyCode) : IRequest<DietO2FitDto>;