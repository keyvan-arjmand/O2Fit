namespace Workout.V2.Infrastructure.Persistence;

public static class MongoDbPersistence
{
    public static void Configure()
    {
        //ProductMap.Configure();
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
        // BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));

        //BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

        //BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

        //var stringBsonSerializer = BsonSerializer.SerializerRegistry.GetSerializer<string>();
        //BsonSerializer.RegisterSerializer(new TodoListName(stringBsonSerializer));
        // Conventions

        BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            //  BsonSerializer.RegisterSerializer(typeof(decimal), new NotNegativeForDecimalTypesSerializer());
        });
        var pack = new ConventionPack
        {
            //new IgnoreExtraElementsConvention(true),
            //new IgnoreIfDefaultConvention(true),
            new CamelCaseElementNameConvention(),
            //new ImmutableTypeClassMapConvention()
        };
        ConventionRegistry.Register("My Solution Conventions", pack, t => true);
    }
}