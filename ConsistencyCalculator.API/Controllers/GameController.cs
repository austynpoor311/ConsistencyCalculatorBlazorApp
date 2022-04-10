using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models;
using ConsistencyCalculator.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ConsistencyCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public GameController(IGameRepository gameRepository, IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public IActionResult GetAllGames()
        {
            return Ok(_gameRepository.GetAllGames());
        }

        [HttpGet("{id}")]
        public IActionResult GetGameById(int id)
        {
            return Ok(_gameRepository.GetGameById(id));
        }

        [HttpGet("remote/{id}")]
        public IActionResult GetGameByRemoteId(string id)
        {
            return Ok(_gameRepository.GetGameByRemoteId(id));
        }

        [HttpPost]
        public IActionResult CreateGame([FromBody] Game game)
        {
            if (game == null)
                return BadRequest();

            var createdGame = _gameRepository.AddGame(game);

            return Created("Game", createdGame);
        }

        [HttpPut]
        public IActionResult UpdateGame([FromBody] Game game)
        {
            if (game == null)
                return BadRequest();

            var gameToUpdate = _gameRepository.GetGameById(game.Id);

            if (gameToUpdate == null)
                return NotFound();

            _gameRepository.UpdateGame(game);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            if (id == 0)
                return BadRequest();

            var gameToDelete = _gameRepository.GetGameById(id);
            if (gameToDelete == null)
                return NotFound();

            _gameRepository.DeleteGame(id);

            return NoContent();//success
        }

        [HttpGet("nba/addorupdate")]
        public async Task AddOrUpdateNbaGames()
        {
            //positionIds: 3 = guard, 5 = small forward, 9 = center
            var data = new PlayerStatistics();
            var players = _playerRepository.GetAllPlayers().ToList();
            var existingGames = _gameRepository.GetAllGames().ToList();
            var teamRemoteIds = _teamRepository.GetAllTeams().Select(t => t.RemoteId).ToList();
            var addedGameIds = new List<string>();
            var gamesToAdd = new List<Game>();

            foreach (var player in players)
            {
                var handler = new HttpClientHandler();

                // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
                handler.AutomaticDecompression = ~DecompressionMethods.None;

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

                var games = data.Games;
                if (games != null)
                {
                    foreach (var game in games.Values)
                    {
                        if (!addedGameIds.Contains(game.Id) && !existingGames.Any(g => g.RemoteId == game.Id) && teamRemoteIds.Contains(game.HomeTeamId) && teamRemoteIds.Contains(game.AwayTeamId))
                        {
                            var awayTeam = _teamRepository.GetTeamByRemoteId(game.AwayTeamId);
                            var homeTeam = _teamRepository.GetTeamByRemoteId(game.HomeTeamId);
                            var newGame = new Game
                            {
                                RemoteId = game.Id,
                                AwayTeamId = awayTeam.Id,
                                HomeTeamId = homeTeam.Id,
                                AwayTeam = awayTeam,
                                HomeTeam = homeTeam,
                                GameDate = DateTime.Parse(game.GameDate),
                                Score = game.Score,
                                HomeTeamScore = game.HomeTeamScore,
                                AwayTeamScore = game.AwayTeamScore,
                                GameResult = game.GameResult,
                                LeagueAbbreviation = game.LeagueAbbreviation,
                                LeagueName = game.LeagueName,
                                LeagueShortName = game.LeagueShortName
                            };

                            gamesToAdd.Add(newGame);
                            addedGameIds.Add(newGame.RemoteId);
                        }
                    }

                }
            }

            foreach (var gameToAdd in gamesToAdd)
            {
                if(teamRemoteIds.Contains(gameToAdd.HomeTeam.RemoteId) && teamRemoteIds.Contains(gameToAdd.AwayTeam.RemoteId))
                {
                    _gameRepository.AddGame(gameToAdd);
                }
            }
        }
    }
}
