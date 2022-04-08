using ConsistencyCalculator.Models.Entities;
using ConsistencyCalculator.Shared.Services.Interfaces.Data;
using System.Text;
using System.Text.Json;

namespace ConsistencyCalculator.Shared.Services.Data
{
    public class TeamDataService : ITeamDataService
    {
        private readonly HttpClient _httpClient;

        public TeamDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Team>>
                (await _httpClient.GetStreamAsync($"api/team"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Team> GetTeamById(int teamId)
        {
            return await JsonSerializer.DeserializeAsync<Team>
                (await _httpClient.GetStreamAsync($"api/team/{teamId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Team> AddTeam(Team team)
        {
            var teamJson =
                new StringContent(JsonSerializer.Serialize(team), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/team", teamJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Team>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateTeam(Team team)
        {
            var teamJson =
                new StringContent(JsonSerializer.Serialize(team), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/team", teamJson);
        }

        public async Task DeleteTeam(int teamId)
        {
            await _httpClient.DeleteAsync($"api/team/{teamId}");
        }
    }
}
