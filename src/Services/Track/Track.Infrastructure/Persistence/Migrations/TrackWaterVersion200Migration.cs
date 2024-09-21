using Mongo.Migration.Migrations.Document;
using Track.Domain.Aggregates.TrackWaterAggregate;

namespace Track.Infrastructure.Persistence.Migrations;

public class TrackWaterVersion200Migration : DocumentMigration<TrackWater>
{
    public TrackWaterVersion200Migration() : base("2.0.0")
    {
    }

    public override void Up(BsonDocument document)
    {
        document.Remove("test");
        document.Add("test2", string.Empty);
    }

    public override void Down(BsonDocument document)
    {
        document.Add("test", string.Empty);
        document.Remove("test2");
    }
}