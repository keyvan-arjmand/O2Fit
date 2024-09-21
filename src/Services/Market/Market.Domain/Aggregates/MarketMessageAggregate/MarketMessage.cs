using Market.Domain.Common;
using Market.Domain.Enums;

namespace Market.Domain.Aggregates.MarketMessageAggregate;

public class MarketMessage:AggregateRoot
{
    public List<string> Version { get; set; } = new();
    public TranslationMarketMessage Title { get; set; } = new();
    public TranslationMarketMessage Description { get; set; } = new();
    public TranslationMarketMessage ButtonName { get; set; } = new();
    public string Link { get; set; } = string.Empty;
    public TargetLink Target { get; set; }
    public string ImageName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Postpone { get; set; }
}