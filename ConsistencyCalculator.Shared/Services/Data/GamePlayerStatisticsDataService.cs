using ConsistencyCalculator.Models.Entities;
using ConsistencyCalculator.Shared.Services.Interfaces.Data;
using System.Text;
using System.Text.Json;

namespace ConsistencyCalculator.Shared.Services.Data
{
    public class GamePlayerStatisticsDataService : IGamePlayerStatisticsDataService
    {
        private readonly HttpClient _httpClient;

        public GamePlayerStatisticsDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<GamePlayerStatistics>> GetAllGamePlayerStatistics()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<GamePlayerStatistics>>
                (await _httpClient.GetStreamAsync($"api/gameplayerstatistics"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<GamePlayerStatistics>> GetGamePlayerStatisticsByPlayerId(int id)
        {
            return await JsonSerializer.DeserializeAsync<List<GamePlayerStatistics>>
                (await _httpClient.GetStreamAsync($"api/gameplayerstatistics/player/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<GamePlayerStatistics>> GetTopGamePlayerStatisticsByPlayerId(int id, int takeVal)
        {
            return await JsonSerializer.DeserializeAsync<List<GamePlayerStatistics>>
                (await _httpClient.GetStreamAsync($"api/gameplayerstatistics/player/take/{id}/{takeVal}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<GamePlayerStatistics>> GetGamePlayerStatisticsByPlayerAgainstTeam(int playerId, int opposingTeamId)
        {
            return await JsonSerializer.DeserializeAsync<List<GamePlayerStatistics>>
                (await _httpClient.GetStreamAsync($"api/gameplayerstatistics/player/opposing/{playerId}/{opposingTeamId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<GamePlayerStatistics> GetGamePlayerStatisticsByGameId(int id)
        {
            return await JsonSerializer.DeserializeAsync<GamePlayerStatistics>
                (await _httpClient.GetStreamAsync($"api/gameplayerstatistics/game/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<GamePlayerStatistics> GetGamePlayerStatisticsById(int playerId, int gameId)
        {
            return await JsonSerializer.DeserializeAsync<GamePlayerStatistics>
                (await _httpClient.GetStreamAsync($"api/gameplayerstatistics/{playerId}/{gameId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<GamePlayerStatistics> AddGamePlayerStatistics(GamePlayerStatistics gamePlayerStatistics)
        {
            var gamePlayerStatisticsJson =
                new StringContent(JsonSerializer.Serialize(gamePlayerStatistics), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/gameplayerstatistics", gamePlayerStatisticsJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<GamePlayerStatistics>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateGamePlayerStatistics(GamePlayerStatistics gamePlayerStatistics)
        {
            var gamePlayerStatisticsJson =
                new StringContent(JsonSerializer.Serialize(gamePlayerStatistics), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/gameplayerstatistics", gamePlayerStatisticsJson);
        }

        public async Task DeleteGamePlayerStatistics(int playerId, int gameId)
        {
            await _httpClient.DeleteAsync($"api/gameplayerstatistics/{playerId}/{gameId}");
        }
    }
}
