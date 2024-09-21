using Identity.V2.Domain.Aggregates.CountryAggregate;
using Mongo.Migration.Migrations.Document;

namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M005_RemoveCityArrayFromCountrysMigration : DocumentMigration<Country>
{
    public M005_RemoveCityArrayFromCountrysMigration(string version) : base("0.0.5")
    {
    }

    public override void Up(BsonDocument document)
    {
        document.Remove("cities");
    }

    public override void Down(BsonDocument document)
    {
        document.Add("cities", new BsonArray());
    }
}