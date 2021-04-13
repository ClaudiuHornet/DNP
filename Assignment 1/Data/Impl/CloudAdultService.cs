﻿using System;
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
            return result;
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
            HttpResponseMessage response = await _client.DeleteAsync($@"https://localhost:5003/Adults/{adultId}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                throw new Exception($@"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}