using AuthApp.Models;
using System.Threading.Tasks;

namespace AuthApp.Services
{
    public interface IUserService
    {
        Task<bool> Register(User user);
        Task<User> Login(string email, string password);
    }
}
