using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Data.Impl
{
    public class UserService : IUserService
    {
        [Inject] private FileContext _fileContext { get; set; }
        private List<User> _users;

        public UserService()
        {
            _fileContext = new FileContext();
            _users = _fileContext.Users.ToList();
            foreach (var user in _users)
            {
                Console.WriteLine(user.UserName);
            }
            _users.Add(new User()
            {
                UserName = "Claudiu",
                Password = "FancyPassword",
                Role = "admin",
                SecurityLevel = 3
            });
            
            _users.Add(new User(){
                UserName = "Manager",
                Password = "AnotherFancyPassword",
                Role = "manager",
                SecurityLevel = 2
            });
            
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            
            User first = _users.FirstOrDefault(user => user.UserName.Equals(username));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }

        public async Task<User> RegisterUserAsync(string username, string password, string confirmPassword)
        {
            User first = _users.FirstOrDefault(user => user.UserName.Equals(username));
            if (first != null)
            {
                throw new Exception("User already exists");
            }

            if (!password.Equals(confirmPassword))
            {
                throw new Exception("Passwords don't match");
            }

            User newUser = new User()
            {
                UserName = username,
                Password = password,
                Role = "User",
                SecurityLevel = 1
            };
            
            _users.Add(newUser);
            _fileContext.Users.Add(newUser);
            _fileContext.SaveUsers();

            return newUser;
        }
    }
}