using Identity.V2.Domain.Aggregates.CountryAggregate;
using Mongo.Migration.Migrations.Document;

namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M003_AddIsDeleteToCountryMigration:DocumentMigration<Country>
{
    public M003_AddIsDeleteToCountryMigration(string version) : base("0.0.3")
    {
    }

    public override void Up(BsonDocument document)
    {
        document.Add("isDelete", false);
    }

    public override void Down(BsonDocument document)
    {
        document.Remove("isDelete");
    }
}