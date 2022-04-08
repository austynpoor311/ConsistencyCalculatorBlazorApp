using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models;
using ConsistencyCalculator.Shared.Services.Interfaces.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ConsistencyCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStatisticsController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerDataService _playerDataService;

        public GameStatisticsController(IPlayerRepository playerRepository, IPlayerDataService playerDataService)
        {
            _playerRepository = playerRepository;
            _playerDataService = playerDataService;
        }

        [HttpGet("nba/addorupdate")]
        public async Task AddOrUpdateNbaGameStatistics()
        {
            //positionIds: 3 = guard, 5 = small forward, 9 = center
            var data = new PlayerStatistics();
            var handler = new HttpClientHandler();
            var players = _playerRepository.GetAllPlayers();

            // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
            handler.AutomaticDecompression = ~DecompressionMethods.None;

            foreach(var player in players)
            {
                // In production code, don't destroy the HttpClient through using, but better use IHttpClientFactory factory or at least reuse an existing HttpClient instance
                // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests
                // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
                using (var httpClient = new HttpClient(handler))
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://site.web.api.espn.com/apis/common/v3/sports/basketball/nba/athletes/{player.RemoteId}/gamelog?region=us&lang=en&contentorigin=espn"))
                    {
                        request.Headers.TryAddWithoutValidation("authority", "site.web.api.espn.com");
                        request.Headers.TryAddWithoutValidation("pragma", "no-cache");
                        request.Headers.TryAddWithoutValidation("cache-control", "no-cache");
                        request.Headers.TryAddWithoutValidation("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"98\", \"Google Chrome\";v=\"98\"");
                        request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
                        request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.102 Safari/537.36");
                        request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "\"Windows\"");
                        request.Headers.TryAddWithoutValidation("accept", "*/*");
                        request.Headers.TryAddWithoutValidation("origin", "https://www.espn.com");
                        request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-site");
                        request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
                        request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
                        request.Headers.TryAddWithoutValidation("referer", "https://www.espn.com/");
                        request.Headers.TryAddWithoutValidation("accept-language", "en-US,en;q=0.9");

                        var response = await httpClient.SendAsync(request);
                        string responseBody = await response.Content.ReadAsStringAsync();
                        data = JsonSerializer.Deserialize<PlayerStatistics>(responseBody);
                    }
                }
            }

            var recentGames = data.SeasonTypes
                .Where(st => st.DisplayName == "2021-22 Regular Season")
                .SelectMany(st => st.Categories)
                .SelectMany(c => c.Events).ToList();

            var statAverages = data.SeasonTypes.Where(st => st.DisplayName == "2021-22 Regular Season")
                .Select(st => st.Summary)
                .SelectMany(s => s.Stats).Where(stats => stats.DisplayName == "Averages")
                .SelectMany(a => a.Stats).ToList();

            //var players = await _playerDataService.GetAllPlayers();
            //var playerSelectListItems = players.Select(t => new SelectListItem
            //{
            //    Value = t.Id.ToString(),
            //    Text = t.FirstName + ' ' + t.LastName
            //});

            //var model = new PlayerStatisticsViewModel
            //{
            //    RecentGamesStats = recentGames,
            //    Averages = statAverages,
            //    Players = new SelectList(playerSelectListItems.OrderBy(e => e.Text), "Value", "Text")
            //};
        }
    }
}
