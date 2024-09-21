using Mongo.Migration.Migrations.Document;

namespace Order.V2.Infrastructure.Persistence.Migration;

public class M002TestMig : DocumentMigration<Domain.Aggregates.OrderAggregate.Order>
{
    public M002TestMig() : base("0.0.2")
    {
    }

    public override void Up(BsonDocument document)
    {
        document.Add("paymentFor", string.Empty);
        document.Add("userType", string.Empty);

    }

    public override void Down(BsonDocument document)
    {
        document.Remove("paymentFor");
        document.Remove("userType");

    }
}