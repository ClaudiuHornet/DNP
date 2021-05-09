using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Data.Impl
{
    public class CloudUserService : IUserService
    {
        private string uri = "https://localhost:5003";
        private HttpClient _client;

        public CloudUserService()
        {
            _client = new HttpClient();
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            HttpResponseMessage response = await _client.GetAsync($"https://localhost:5003/users?username={username}&password={password}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string userAsJson = await response.Content.ReadAsStringAsync();
                User resultUser = JsonSerializer.Deserialize<User>(userAsJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                });
                Console.WriteLine(resultUser.ToString());
                return resultUser;
            } 
            throw new Exception("User not found");
        }

        public async Task<User> RegisterUserAsync(string username, string password, string confirmPassword)
        {
            if (!(password.Equals(confirmPassword)))
            {
                throw new Exception("Passwords don't match");
            }

            User userToSend = new User()
            {
                UserName = username,
                Password = password,
                Role = "User",
                SecurityLevel = 1
            };
            Console.WriteLine(userToSend);
            string userAsJson = JsonSerializer.Serialize(userToSend);
            HttpContent content = new StringContent(userAsJson, Encoding.UTF8, "application/json");
            await _client.PostAsync(uri + "/Users", content);
            return userToSend;
        }
    }
}