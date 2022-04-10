using ConsistencyCalculator.Models.Entities;
using ConsistencyCalculator.Shared.Services.Interfaces.Data;
using System.Text;
using System.Text.Json;

namespace ConsistencyCalculator.Shared.Services.Data
{
    public class GameDataService : IGameDataService
    {
        private readonly HttpClient _httpClient;

        public GameDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Game>>
                (await _httpClient.GetStreamAsync($"api/game"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Game> GetGameById(int gameId)
        {
            return await JsonSerializer.DeserializeAsync<Game>
                (await _httpClient.GetStreamAsync($"api/game/{gameId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Game> GetGameByRemoteId(string remoteId)
        {
            return await JsonSerializer.DeserializeAsync<Game>
                (await _httpClient.GetStreamAsync($"api/game/remote/{remoteId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Game> AddGame(Game game)
        {
            var gameJson =
                new StringContent(JsonSerializer.Serialize(game), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/game", gameJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Game>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateGame(Game game)
        {
            var gameJson =
                new StringContent(JsonSerializer.Serialize(game), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/game", gameJson);
        }

        public async Task DeleteGame(int gameId)
        {
            await _httpClient.DeleteAsync($"api/game/{gameId}");
        }
    }
}
