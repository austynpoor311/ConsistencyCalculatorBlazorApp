using ConsistencyCalculator.Models.Entities;
using ConsistencyCalculator.Shared.Services.Interfaces.Data;
using System.Text;
using System.Text.Json;

namespace ConsistencyCalculator.Shared.Services.Data
{
    public class PositionDataService : IPositionDataService
    {
        private readonly HttpClient _httpClient;

        public PositionDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Position>> GetAllPositions()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Position>>
                (await _httpClient.GetStreamAsync($"api/position"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Position> GetPositionById(int positionId)
        {
            return await JsonSerializer.DeserializeAsync<Position>
                (await _httpClient.GetStreamAsync($"api/position/{positionId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Position> AddPosition(Position position)
        {
            var positionJson =
                new StringContent(JsonSerializer.Serialize(position), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/position", positionJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Position>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdatePosition(Position position)
        {
            var positionJson =
                new StringContent(JsonSerializer.Serialize(position), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/position", positionJson);
        }

        public async Task DeletePosition(int positionId)
        {
            await _httpClient.DeleteAsync($"api/position/{positionId}");
        }
    }
}
