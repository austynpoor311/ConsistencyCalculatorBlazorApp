using ConsistencyCalculator.Models.Entities;
using ConsistencyCalculator.Shared.Services.Interfaces.Data;
using System.Text;
using System.Text.Json;

namespace ConsistencyCalculator.Shared.Services.Data
{
    public class InjuryDataService : IInjuryDataService
    {
        private readonly HttpClient _httpClient;

        public InjuryDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Injury>> GetAllInjuries()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Injury>>
                (await _httpClient.GetStreamAsync($"api/injury"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Injury> GetInjuryById(int injuryId)
        {
            return await JsonSerializer.DeserializeAsync<Injury>
                (await _httpClient.GetStreamAsync($"api/injury/{injuryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Injury> AddInjury(Injury injury)
        {
            var injuryJson =
                new StringContent(JsonSerializer.Serialize(injury), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/injury", injuryJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Injury>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateInjury(Injury injury)
        {
            var injuryJson =
                new StringContent(JsonSerializer.Serialize(injury), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/injury", injuryJson);
        }

        public async Task DeleteInjury(int injuryId)
        {
            await _httpClient.DeleteAsync($"api/injury/{injuryId}");
        }

        public async Task DeleteAllInjuries()
        {
            await _httpClient.DeleteAsync($"api/injury/all");
        }
    }
}
