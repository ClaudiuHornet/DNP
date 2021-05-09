using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Repository.Impl
{
    public class UsersRepository : IRepositoryUsers
    {
        public async Task<User> ValidateUserAsync(string username, string password)
        {
            await using AssignmentDbContext dbContext = new AssignmentDbContext();
            User user = dbContext.Users.FirstOrDefaultAsync(user1 => user1.UserName.Equals(username)).GetAwaiter()
                .GetResult();
            Console.WriteLine(user.ToString());
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (!user.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }

            return user;
        }

        public async Task<User> RegisterUserAsync(string username, string password, string confirmPassword)
        {
            await using AssignmentDbContext dbContext = new AssignmentDbContext();
            User user = dbContext.Users.FirstOrDefaultAsync(user1 => user1.UserName.Equals(username)).GetAwaiter().GetResult();
            // Console.WriteLine(user.ToString());
            if (user != null)
            {
                throw new Exception("User already exists");
            }

            User newUser = new User()
            {
                UserName = username,
                Password = password,
                Role = "User",
                SecurityLevel = 1
            };

            Console.WriteLine(newUser.ToString());
            
            await AddUserAsync(newUser);
            Console.WriteLine("end of registerUserAsync userrepository");
            return newUser;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await using AssignmentDbContext dbContext = new AssignmentDbContext();
            EntityEntry<User> newlyAdded = await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            Console.WriteLine("Success");
            return newlyAdded.Entity;
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            await using AssignmentDbContext dbContext = new AssignmentDbContext();
            return await dbContext.Users.ToListAsync();
        }
    }
}