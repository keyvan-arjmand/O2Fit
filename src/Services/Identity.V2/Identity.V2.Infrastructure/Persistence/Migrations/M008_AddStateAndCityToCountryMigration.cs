namespace Identity.V2.Infrastructure.Persistence.Migrations;

public class M008_AddStateAndCityToCountryMigration:DocumentMigration<Country>
{
    public M008_AddStateAndCityToCountryMigration(string version) : base("0.0.8")
    {
    }

    public override void Up(BsonDocument document)
    {
        //var translation = new Translation();
        document.Add("states", new BsonArray());
        //var states = document["states"].AsBsonArray;
        //foreach (var bsonValue in states)
        //{
        //    var doc = bsonValue.AsBsonDocument; 
        //    doc.Add("name", translation.ToJson());
        //    doc.Add("cities", new BsonArray());
        //    var cities = doc["cities"].AsBsonArray;
        //    foreach (var cityObj in cities)
        //    {
        //        var cityDoc = cityObj.AsBsonDocument;
        //        cityDoc.Add("name", translation.ToJson());
        //    }
        //}
        
    }

    public override void Down(BsonDocument document)
    {
        document.Remove("states");
    }
}