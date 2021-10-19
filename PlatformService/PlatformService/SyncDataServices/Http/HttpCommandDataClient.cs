using PlatformService.DTOs.ReadDTOs;
using PlatformService.SyncDataServices.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : IHttpCommandDataClient
    {
        private readonly HttpClient _httpClient;

        public HttpCommandDataClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendPlatformToCommand(ReadPlatformDTO readPlatformDTO)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(readPlatformDTO),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("http://localhost:6000/api/platforms", httpContent);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("Sync POST to CommandsService was OK.");
            else
                Console.WriteLine("Sync POST to CommandsService was NOT OK.");
        }
    }
}
