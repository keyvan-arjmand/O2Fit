using MongoDB.Driver;
using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.SequenceAggregate;

public class Sequence : AggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public long Value { get; set; }
}