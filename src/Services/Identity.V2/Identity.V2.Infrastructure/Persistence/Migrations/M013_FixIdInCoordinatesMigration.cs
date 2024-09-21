namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M013_FixIdInCoordinatesMigration:DocumentMigration<User>
{
    public M013_FixIdInCoordinatesMigration(string version) : base("0.1.3")
    {
    }

    public override void Up(BsonDocument document)
    {
        var profile = document["nutritionistProfile"].AsBsonDocument;
        var coord = profile["coordinates"].AsBsonDocument;
        coord.Add("_id", ObjectId.GenerateNewId());
    }

    public override void Down(BsonDocument document)
    {
        var profile = document["nutritionistProfile"].AsBsonDocument;
        var coord = profile["coordinates"].AsBsonDocument;
        coord.Remove("_id");
    }
}