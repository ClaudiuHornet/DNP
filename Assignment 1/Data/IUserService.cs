using Models;

namespace Assignment_1.Data
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
        User RegisterUser(string username, string password, string confirmPassword);
    }
}