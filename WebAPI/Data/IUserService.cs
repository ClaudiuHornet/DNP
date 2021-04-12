using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(string username, string password);
        Task<User> RegisterUserAsync(string username, string password, string confirmPassword);
    }
}