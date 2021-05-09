using System.Threading.Tasks;
using Assignment_1.Models;

namespace Assignment_1.Data
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(string username, string password);
        Task<User> RegisterUserAsync(string username, string password, string confirmPassword);
    }
}