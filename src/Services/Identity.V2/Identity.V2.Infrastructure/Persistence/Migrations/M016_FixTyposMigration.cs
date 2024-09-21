namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M016_FixTyposMigration:DocumentMigration<User>
{
    public M016_FixTyposMigration(string version) : base("0.1.6")
    {
    }

    public override void Up(BsonDocument document)
    {
        var nutritionistProfile = document["nutritionistProfile"].AsBsonDocument;
        nutritionistProfile.Remove("dietTypeIds");
        nutritionistProfile.Add("dietTypesIds", new BsonArray());
        nutritionistProfile.Remove("specialDiseaseIds");
        nutritionistProfile.Add("specialDiseasesIds", new BsonArray());
        
    }

    public override void Down(BsonDocument document)
    {
        var nutritionistProfile = document["nutritionistProfile"].AsBsonDocument;
        nutritionistProfile.Remove("specialDiseasesIds");
        nutritionistProfile.Remove("dietTypesIds");
        nutritionistProfile.Add("dietTypeIds", new BsonArray());
        nutritionistProfile.Add("specialDiseaseIds", new BsonArray());
    }
}