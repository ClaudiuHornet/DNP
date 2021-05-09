using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IRepositoryUsers
    {
        Task<User> ValidateUserAsync(string username, string password);
        Task<User> RegisterUserAsync(string username, string password, string confirmPassword);
        Task<User> AddUserAsync(User user);
        Task<IList<User>> GetUsersAsync();
    }
}