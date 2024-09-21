using Identity.V2.Domain.Aggregates.CountryAggregate;
using Mongo.Migration.Migrations.Document;

namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M001_AddCityEmptyArrayMigration : DocumentMigration<Country>
{
    public M001_AddCityEmptyArrayMigration() : base("0.0.1")
    {
        
    }

    public override void Up(BsonDocument document)
    {
 
        document.Add("cities", new BsonArray());
    }

    public override void Down(BsonDocument document)
   {
        document.Remove("cities");
    }
}