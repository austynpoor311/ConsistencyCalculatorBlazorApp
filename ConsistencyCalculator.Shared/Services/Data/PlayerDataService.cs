using ConsistencyCalculator.Models.Entities;
using ConsistencyCalculator.Shared.Services.Interfaces.Data;
using System.Text;
using System.Text.Json;

namespace ConsistencyCalculator.Shared.Services.Data
{
    public class PlayerDataService : IPlayerDataService
    {
        private readonly HttpClient _httpClient;

        public PlayerDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Player>>
                (await _httpClient.GetStreamAsync($"api/player"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Player> GetPlayerById(int playerId)
        {
            return await JsonSerializer.DeserializeAsync<Player>
                (await _httpClient.GetStreamAsync($"api/player/{playerId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Player> GetPlayerRemoteById(int remoteId)
        {
            return await JsonSerializer.DeserializeAsync<Player>
                (await _httpClient.GetStreamAsync($"api/player/remote/{remoteId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Player> AddPlayer(Player player)
        {
            var playerJson =
                new StringContent(JsonSerializer.Serialize(player), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/player", playerJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Player>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdatePlayer(Player player)
        {
            var playerJson =
                new StringContent(JsonSerializer.Serialize(player), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/player", playerJson);
        }

        public async Task DeletePlayer(int playerId)
        {
            await _httpClient.DeleteAsync($"api/player/{playerId}");
        }
    }
}
