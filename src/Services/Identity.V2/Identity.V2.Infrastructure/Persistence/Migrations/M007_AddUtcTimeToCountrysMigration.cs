namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M007_AddUtcTimeToCountrysMigration:DocumentMigration<Country>
{
    public M007_AddUtcTimeToCountrysMigration(string version) : base("0.0.7")
    {
    }

    public override void Up(BsonDocument document)
    {
        document.Add("utcTime", string.Empty);
    }

    public override void Down(BsonDocument document)
    {
        document.Remove("utcTime");
    }
}