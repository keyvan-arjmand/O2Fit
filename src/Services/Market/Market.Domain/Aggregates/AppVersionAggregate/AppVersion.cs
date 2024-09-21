using Market.Domain.Common;
using Market.Domain.Enums;

namespace Market.Domain.Aggregates.AppVersionAggregate;

public class AppVersion : AggregateRoot
{
    public List<string> Version { get; set; } = new();
    public TranslationAppVersion Description { get; set; } = new();
    public bool IsForced { get; set; }
    public List<MarketType> MarketTypes { get; set; } = new();
    public string Link { get; set; } = string.Empty;
}