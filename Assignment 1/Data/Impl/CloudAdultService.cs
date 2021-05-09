using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment_1.Models;

namespace Assignment_1.Data.Impl
{
    public class CloudAdultService : IAdultData
    {
        private string uri = "https://localhost:5003";
        private HttpClient _client;

        public CloudAdultService()
        {
            _client = new HttpClient();
        }

        public async Task<IList<Adult>> GetAdultsAsync()
        {
            Task<string> stringAsync = _client.GetStringAsync(uri+"/Adults");
            string message = await stringAsync;
            List<Adult> result = JsonSerializer.Deserialize<List<Adult>>(message, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            foreach (var adult in result)
            {
                Console.WriteLine("adult from CloudAdultService : " + adult);
            }
            return result;
        }

        public async Task<Adult> GetFilteredAdultsAsync(int? id)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync($"https://localhost:5003/Adults/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine(responseMessage.StatusCode);
                Console.WriteLine("something");
            }
            else
            {
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.StatusCode}");
            }
            
            string result = await responseMessage.Content.ReadAsStringAsync();
            Adult adult = JsonSerializer.Deserialize<Adult>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return adult;
        }

        public async Task AddAdultAsync(Adult adult)
        {
            string todoAsJson = JsonSerializer.Serialize(adult);
            HttpContent content = new StringContent(todoAsJson,
                Encoding.UTF8,
                "application/json");
            await _client.PostAsync(uri+"/Adults", content);
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"https://localhost:5003/Adults/{adultId}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}