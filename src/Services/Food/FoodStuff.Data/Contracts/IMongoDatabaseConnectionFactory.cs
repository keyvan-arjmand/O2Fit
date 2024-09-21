using MongoDB.Driver;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
    public interface IMongoDatabaseConnectionFactory
    {
        Task<MongoDatabaseBase> CreateConnectionAsync();
    }
}
