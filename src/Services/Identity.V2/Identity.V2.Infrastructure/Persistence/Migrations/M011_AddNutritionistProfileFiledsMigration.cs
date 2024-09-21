namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M011_AddNutritionistProfileFiledsMigration:DocumentMigration<User>
{
    public M011_AddNutritionistProfileFiledsMigration(string version) : base("0.1.1")
    {
    }

    public override void Up(BsonDocument document)
    {
        var profile = document["nutritionistProfile"].AsBsonDocument;
        profile.Add("gender", 0);
        profile.Add("profileImageName", string.Empty);
        profile.Add("firstName", string.Empty);
        profile.Add("lastName", string.Empty);
        profile.Add("scientificDegree", 0);
        profile.Add("medicalSystemNumber", string.Empty);
        profile.Add("experienceYear", 0);
        profile.Add("activityExpirationDate", DateTime.MinValue);
        profile.Add("licenseImageName", string.Empty);
        profile.Add("otherDocumentsImageName", string.Empty);
        profile.Add("aboutTheSpecialist", string.Empty);
        profile.Add("categoryOfFitnessGoal", 0);
        profile.Add("dietType", ObjectId.Empty);
        profile.Add("specialDisease", new BsonArray());
        profile.Add("hasOffice", false);
        profile.Add("officeAddress", string.Empty);
        profile.Add("officePhoneNumber", string.Empty);
        profile.Add("image1Name", string.Empty);
        profile.Add("image2Name", string.Empty);
        profile.Add("image3Name", string.Empty);
        profile.Add("image4Name", string.Empty);
        profile.Add("coordinates", new BsonDocument());

    }

    public override void Down(BsonDocument document)
    {
        var profile = document["nutritionistProfile"].AsBsonDocument;
        profile.Remove("gender");
        profile.Remove("profileImageName");
        profile.Remove("firstName");
        profile.Remove("lastName");
        profile.Remove("scientificDegree");
        profile.Remove("medicalSystemNumber");
        profile.Remove("experienceYear");
        profile.Remove("activityExpirationDate");
        profile.Remove("licenseImageName");
        profile.Remove("otherDocumentsImageName");
        profile.Remove("aboutTheSpecialist");
        profile.Remove("categoryOfFitnessGoal");
        profile.Remove("dietType");
        profile.Remove("specialDisease");
        profile.Remove("hasOffice");
        profile.Remove("officeAddress");
        profile.Remove("officePhoneNumber");
        profile.Remove("image1Name");
        profile.Remove("image2Name");
        profile.Remove("image3Name");
        profile.Remove("image4Name");
        profile.Remove("coordinates");
    }
}