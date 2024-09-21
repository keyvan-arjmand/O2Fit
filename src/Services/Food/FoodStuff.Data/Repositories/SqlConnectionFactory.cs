using System;
using System.Data;
using System.Threading.Tasks;
using FoodStuff.Data.Contracts;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace FoodStuff.Data.Repositories
{
    public class SqlConnectionFactory:IDatabaseConnectionFactory
    {
        private readonly string _ConnectionString;

        public SqlConnectionFactory(string ConnectionString) => _ConnectionString = ConnectionString ??
            throw new ArgumentNullException(nameof(ConnectionString));
        
        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var sqlConnection = new NpgsqlConnection(_ConnectionString);
            await sqlConnection.OpenAsync();
            return sqlConnection;
        }
    }
}