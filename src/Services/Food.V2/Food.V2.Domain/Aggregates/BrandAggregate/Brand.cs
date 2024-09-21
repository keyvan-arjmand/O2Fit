namespace Food.V2.Domain.Aggregates.BrandAggregate;

public class Brand: AggregateRoot
{
    public string? LogoUri { get; set; }

    public string? Address { get; set; }
    public BrandTranslation Translation { get; set; } = new();
}