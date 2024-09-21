namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M015_AddMissingFieldsAndEditChangedFieldsMigration:DocumentMigration<User>
{
    public M015_AddMissingFieldsAndEditChangedFieldsMigration(string version) : base("0.1.5")
    {
    }

    public override void Up(BsonDocument document)
    {
        document.Add("status", 1);
        var nutritionistProfile = document["nutritionistProfile"].AsBsonDocument;
        nutritionistProfile.Remove("dietType");
        nutritionistProfile.Add("dietTypeIds", new BsonArray());
        nutritionistProfile.Remove("specialDisease");
        nutritionistProfile.Add("specialDiseaseIds", new BsonArray());
        nutritionistProfile.Remove("image1Name");
        nutritionistProfile.Add("officeImage1Name", string.Empty);
        nutritionistProfile.Remove("image2Name");
        nutritionistProfile.Add("officeImage2Name", string.Empty);
        nutritionistProfile.Remove("image3Name");
        nutritionistProfile.Add("officeImage3Name", string.Empty);
        nutritionistProfile.Remove("image4Name");
        nutritionistProfile.Add("officeImage4Name", string.Empty);
        
    }

    public override void Down(BsonDocument document)
    {
        document.Remove("status");
        var nutritionistProfile = document["nutritionistProfile"].AsBsonDocument;
        nutritionistProfile.Remove("dietTypeIds");
        nutritionistProfile.Remove("specialDiseaseIds");
        nutritionistProfile.Remove("officeImage1Name");
        nutritionistProfile.Remove("officeImage2Name");
        nutritionistProfile.Remove("officeImage3Name");
        nutritionistProfile.Remove("officeImage4Name");

    }
}