using Mongo.Migration.Documents;
using Mongo.Migration.Migrations.Document;
using MongoDB.Driver;
using Track.Domain.Aggregates.TrackWaterAggregate;

namespace Track.Infrastructure.Persistence.Migrations;

public class TrackWaterVersion001Migration : DocumentMigration<TrackWater>
{
    public TrackWaterVersion001Migration() : base("1.0.0")
    {
       
    }

    public override void Up(BsonDocument document)
    {
        document.Add("test", string.Empty);
    }

    public override void Down(BsonDocument document)
    {
        document.Remove("test");
    }
}