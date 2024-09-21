using FoodStuff.Data.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace FoodStuff.Data.Repositories
{
    public class MongoConnectionFactory : IMongoDatabaseConnectionFactory
    {
        private readonly string _ConnectionString;

        public MongoConnectionFactory(string ConnectionString) => _ConnectionString = ConnectionString ??
            throw new ArgumentNullException(nameof(ConnectionString));

        public async Task<MongoDatabaseBase> CreateConnectionAsync()
        {

            const string connectionUri = "mongodb+srv://key1:<password>@cluster0.tza3ogk.mongodb.net/?retryWrites=true&w=majority";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return (MongoDatabaseBase)client.GetDatabase("");
        }
    }
}
