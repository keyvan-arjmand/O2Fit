namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M012_FixCoordinatesMigration:DocumentMigration<User>
{
    public M012_FixCoordinatesMigration(string version) : base("0.1.2")
    {
    }

    public override void Up(BsonDocument document)
    {
        var profile = document["nutritionistProfile"].AsBsonDocument;
        var coord = profile["coordinates"].AsBsonDocument;
        coord.Add("lat", 0.00000);
        coord.Add("long", 0.00000);

    }

    public override void Down(BsonDocument document)
    {
        var profile = document["nutritionistProfile"].AsBsonDocument;
        var coord = profile["coordinates"].AsBsonDocument;
        coord.Remove("lat");
        coord.Remove("long");

    }
}