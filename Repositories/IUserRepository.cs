using AuthApp.Models;
using System.Threading.Tasks;

namespace AuthApp.Repositories
{
    public interface IUserRepository
    {
        Task<int> Register(User user);
        Task<User> Login(string email, string password);
    }
}
