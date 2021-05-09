using System;
using System.Collections.Generic;
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

        public Task<User> RegisterUserAsync(string username, string password, string confirmPassword)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> AddUserAsync(User user)
        {
            await using AssignmentDbContext dbContext = new AssignmentDbContext();
            EntityEntry<User> newlyAdded = await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return newlyAdded.Entity;
        }

        public async Task<IList<User>> GetUsersAsync()
        {
            await using AssignmentDbContext dbContext = new AssignmentDbContext();
            return await dbContext.Users.ToListAsync();
        }
    }
}