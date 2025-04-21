using AuthApp.Models;
using AuthApp.Repositories;
using System.Threading.Tasks;

namespace AuthApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Register(User user)
        {
            var result = await _repo.Register(user);
            return result > 0;
        }

        public async Task<User> Login(string email, string password)
        {
            return await _repo.Login(email, password);
        }
    }
}
