using Dapper;
using AuthApp.Models;
using AuthApp.Data;
using System.Threading.Tasks;

namespace AuthApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _db;

        public UserRepository(DbContext db)
        {
            _db = db;
        }

        public async Task<int> Register(User user)
        {
            var sql = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";
            using var conn = _db.CreateConnection();
            return await conn.ExecuteAsync(sql, user);
        }

        public async Task<User> Login(string email, string password)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
            using var conn = _db.CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { Email = email, Password = password });
        }
    }
}
