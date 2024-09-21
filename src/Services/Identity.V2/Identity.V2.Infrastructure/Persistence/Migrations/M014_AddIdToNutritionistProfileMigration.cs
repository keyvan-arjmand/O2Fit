namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M014_AddIdToNutritionistProfileMigration:DocumentMigration<User>
{
    public M014_AddIdToNutritionistProfileMigration(string version) : base("0.1.4")
    {
    }

    public override void Up(BsonDocument document)
    {
        var profile = document["nutritionistProfile"].AsBsonDocument;
        profile.Add("_id", ObjectId.GenerateNewId());
    }

    public override void Down(BsonDocument document)
    {
        var profile = document["nutritionistProfile"].AsBsonDocument;
        profile.Remove("_id");
    }
}