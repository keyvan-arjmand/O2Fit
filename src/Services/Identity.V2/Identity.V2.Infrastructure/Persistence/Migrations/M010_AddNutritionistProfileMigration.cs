namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M010_AddNutritionistProfileMigration:DocumentMigration<User>
{
    public M010_AddNutritionistProfileMigration(string version) : base("0.1.0")
    {
    }

    public override void Up(BsonDocument document)
    {
        document.Add("nutritionistProfile", new BsonDocument());
    }

    public override void Down(BsonDocument document)
    {
        document.Remove("nutritionistProfile");
    }
}