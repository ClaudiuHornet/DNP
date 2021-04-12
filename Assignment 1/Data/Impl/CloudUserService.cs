using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment_1.Models;

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
                return resultUser;
            } 
            throw new Exception("User not found");
        }

        public Task<User> RegisterUser(string username, string password, string confirmPassword)
        {
            throw new System.NotImplementedException();
        }
    }
}