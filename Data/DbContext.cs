using System.Data;
using Microsoft.Data.SqlClient;

namespace AuthApp.Data
{
    public class DbContext
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DbContext(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
