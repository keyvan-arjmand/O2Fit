namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M009_AddStateIdAndCityIdToUserMigration:DocumentMigration<User>
{
    public M009_AddStateIdAndCityIdToUserMigration(string version) : base("0.0.9")
    {
    }

    public override void Up(BsonDocument document)
    {
        document.Add("stateId", ObjectId.Empty);
        document.Add("cityId", ObjectId.Empty);

    }

    public override void Down(BsonDocument document)
    {
        document.Remove("stateId");
        document.Remove("cityId");
    }
}