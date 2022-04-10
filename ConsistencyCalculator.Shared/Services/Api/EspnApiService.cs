using ConsistencyCalculator.Models.Entities;
using ConsistencyCalculator.Shared.Services.Interfaces.Api;
using ConsistencyCalculator.Shared.Services.Interfaces.Data;
using System.Text;
using System.Text.Json;

namespace ConsistencyCalculator.Shared.Services.Api
{
    public class EspnApiService : IEspnApiService
    {
        private readonly HttpClient _httpClient;

        public EspnApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddOrUpdateNbaPlayersAsync()
        {
            await _httpClient.GetAsync("api/player/nba/addorupdate");
        }

        public async Task AddOrUpdateNbaPlayerInjuries()
        {
            await _httpClient.GetAsync("api/injury/nba/addorupdate");
        }
    }
}
