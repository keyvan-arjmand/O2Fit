using System.Data;
using System.Threading.Tasks;

namespace Identity.Data.Contracts
{
    public interface IDatabaseConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}