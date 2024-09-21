using Identity.V2.Domain.Aggregates.CountryAggregate;
using Mongo.Migration.Migrations.Document;

namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M002_AddObjectIdAndIsDeleteToTraslationMigration: DocumentMigration<Country>
{
    public M002_AddObjectIdAndIsDeleteToTraslationMigration(string version) : base("0.0.2")
    {
    }

    public override void Up(BsonDocument document)
    {
        var translation = document["translation"].AsBsonDocument;
        translation.Add("_id", ObjectId.GenerateNewId());
        translation.Add("isDelete", false);
    }

    public override void Down(BsonDocument document)
    {
        var translation = document["translation"].AsBsonDocument;
        translation.Remove("_id");
        translation.Remove("isDelete");
    }
}