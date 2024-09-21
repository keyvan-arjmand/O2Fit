namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M006_AddAlphaAndCountryCodeAndCultureAndCurrencyCodeAndCurrencyNameToCountrysMigration: DocumentMigration<Country>
{
    public M006_AddAlphaAndCountryCodeAndCultureAndCurrencyCodeAndCurrencyNameToCountrysMigration(string version) :
        base("0.0.6")
    {
    }

    public override void Up(BsonDocument document)
    {
        document.Add("alpha", string.Empty);
        document.Add("countryCode", string.Empty);
        document.Add("culture", string.Empty);
        document.Add("currencyCode", string.Empty);
        document.Add("currencyName", string.Empty);
    }

    public override void Down(BsonDocument document)
    {
        document.Remove("alpha");
        document.Remove("countryCode");
        document.Remove("culture");
        document.Remove("currencyCode");
        document.Remove("currencyName");
    }
}