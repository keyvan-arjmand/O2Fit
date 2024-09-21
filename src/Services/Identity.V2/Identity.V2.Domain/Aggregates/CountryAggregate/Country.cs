namespace Identity.V2.Domain.Aggregates.CountryAggregate;

//[RuntimeVersion("0.0.4")]
//[StartUpVersion("0.0.4")]
[RuntimeVersion("0.1.6")]
[BsonIgnoreExtraElements]
[CollectionLocation("countrys","default")]
public class Country : AggregateRoot, IDocument
{
    public int CountryId { get; set; }
    public string? FlagIcon { get; set; }
    public Translation Translation { get; set; } = default!;
    public string Alpha { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string Culture { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public string CurrencyName { get; set; } = string.Empty;
    public string UtcTime { get; set; } = string.Empty;
    public List<State> States { get; set; } = new();
    public DocumentVersion Version { get; set; }

}