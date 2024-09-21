using System.Data;
using System.Threading.Tasks;

namespace FoodStuff.Data.Contracts
{
    public interface IDatabaseConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}