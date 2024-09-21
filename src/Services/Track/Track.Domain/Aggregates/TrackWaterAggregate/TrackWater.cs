using Common.Constants.Track.Migrations.TrackWater;
using Mongo.Migration.Documents;
using Mongo.Migration.Documents.Attributes;
using Track.Domain.Common;
using Track.Domain.ValueObjects;

namespace Track.Domain.Aggregates.TrackWaterAggregate;

[RuntimeVersion(MigrationVersion.CurrentVersion)]
[CollectionLocation(MigrationVersion.CollectionName, MigrationVersion.DataBaseName)]
public class TrackWater : AggregateRoot, IDocument
{
    public DateTime InsertDate { get; set; }
    public NotNegativeForDecimalTypes Value { get; set; } = new();
    public string AppId { get; set; } = string.Empty;
}