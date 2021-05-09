using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPI.DataAccess;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Repository;
using WebAPI.Repository.Impl;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SeedDatabase();
            CreateHostBuilder(args).Build().Run();
        }

        private static async Task SeedDatabase()
        {
            IRepositoryAdults repositoryAdults = new AdultRepository();
            List<Adult> adults = repositoryAdults.GetAdultsAsync().GetAwaiter().GetResult().ToList();
            if (!adults.Any())
            {
                Console.WriteLine("No adults, seeding database from file");
                IList<Adult> seedAdults = GetAdultsFromFile();
                foreach (var adult in seedAdults)
                {
                    await repositoryAdults.AddAdultAsync(adult);
                }
            }

            IRepositoryUsers repositoryUsers = new UsersRepository();
            List<User> users = repositoryUsers.GetUsersAsync().GetAwaiter().GetResult().ToList();
            if (!users.Any())
            {
                Console.WriteLine("No users, seeding database from file");
                IList<User> seedUsers = GetUsersFromFile();
                foreach (var user in seedUsers)
                {
                    await repositoryUsers.AddUserAsync(user);
                }
            }

        }

        private static IList<User> GetUsersFromFile()
        {
            Console.WriteLine("Getting Users from file");
            FileContext fileContext = new FileContext();
            IList<User> users = fileContext.Users;
            return users;
        }

        private static IList<Adult> GetAdultsFromFile()
        {
            Console.WriteLine("Getting Adults from file");
            FileContext fileContext = new FileContext();
            IList<Adult> adults = fileContext.Adults;
            return adults;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}