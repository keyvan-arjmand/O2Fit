using Common.Enums.TypeEnums;

namespace EventBus.Messages.Contracts.Services.Payments.Package;

public class GetByIdPackageResult
{
    public string Id { get; init; }
    public string UserId { get; init; }
    public int Duration { get; init; }
    public PackageType PackageType { get; init; }
    public string CurrencyCode { get; init; }
    public string CurrencyId { get; init; }
    public List<int> CountryIds { get; init; } = default!;
    public double Price { get; init; }
    public double DiscountPrice { get; init; }
    public double Wage { get; init; }
}
