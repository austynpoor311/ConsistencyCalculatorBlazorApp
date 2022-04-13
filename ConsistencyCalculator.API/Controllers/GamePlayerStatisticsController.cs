using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models;
using ConsistencyCalculator.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace ConsistencyCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePlayerStatisticsController : Controller
    {
        private readonly IGamePlayerStatisticsRepository _gamePlayerStatisticsRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameRepository _gameRepository;

        public GamePlayerStatisticsController(IGamePlayerStatisticsRepository gamePlayerStatisticsRepository, IPlayerRepository playerRepository, IGameRepository gameRepository)
        {
            _gamePlayerStatisticsRepository = gamePlayerStatisticsRepository;
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public IActionResult GetAllGamePlayerStatisticss()
        {
            return Ok(_gamePlayerStatisticsRepository.GetAllGamePlayerStatistics());
        }

        [HttpGet("{playerId}/{gameId}")]
        public IActionResult GetGamePlayerStatisticsById(int playerId, int gameId)
        {
            return Ok(_gamePlayerStatisticsRepository.GetGamePlayerStatisticsById(playerId, gameId));
        }

        [HttpGet("player/{id}")]
        public IActionResult GetGamePlayerStatisticsByPlayerId(int id)
        {
            return Ok(_gamePlayerStatisticsRepository.GetGamePlayerStatisticsByPlayerId(id));
        }

        [HttpGet("player/take/{id}/{takeVal}")]
        public IActionResult GetTopGamePlayerStatisticsByPlayerId(int id, int takeVal)
        {
            return Ok(_gamePlayerStatisticsRepository.GetTopGamePlayerStatisticsByPlayerId(id, takeVal));
        }

        [HttpGet("player/opposing/{id}/{opposingTeamId}")]
        public IActionResult GetGamePlayerStatisticsByPlayerAgainstTeam(int id, int opposingTeamId)
        {
            return Ok(_gamePlayerStatisticsRepository.GetGamePlayerStatisticsByPlayerAgainstTeam(id, opposingTeamId));
        }

        [HttpGet("game/{id}")]
        public IActionResult GetGamePlayerStatisticsByGameId(int id)
        {
            return Ok(_gamePlayerStatisticsRepository.GetGamePlayerStatisticsByGameId(id));
        }

        [HttpPost]
        public IActionResult CreateGamePlayerStatistics([FromBody] GamePlayerStatistics gamePlayerStatistics)
        {
            if (gamePlayerStatistics == null)
                return BadRequest();

            var createdGamePlayerStatistics = _gamePlayerStatisticsRepository.AddGamePlayerStatistics(gamePlayerStatistics);

            return Created("GamePlayerStatistics", createdGamePlayerStatistics);
        }

        [HttpPut]
        public IActionResult UpdateGamePlayerStatistics([FromBody] GamePlayerStatistics gamePlayerStatistics)
        {
            if (gamePlayerStatistics == null)
                return BadRequest();

            var gameplayerstatisticsToUpdate = _gamePlayerStatisticsRepository.GetGamePlayerStatisticsById(gamePlayerStatistics.Player.Id, gamePlayerStatistics.Game.Id);

            if (gameplayerstatisticsToUpdate == null)
                return NotFound();

            _gamePlayerStatisticsRepository.UpdateGamePlayerStatistics(gamePlayerStatistics);

            return NoContent(); //success
        }

        [HttpDelete("{playerId}/{gameId}")]
        public IActionResult DeleteGamePlayerStatistics(int playerId, int gameId)
        {
            if (playerId == 0 || gameId == 0)
                return BadRequest();

            var gameplayerstatisticsToDelete = _gamePlayerStatisticsRepository.GetGamePlayerStatisticsById(playerId, gameId);
            if (gameplayerstatisticsToDelete == null)
                return NotFound();

            _gamePlayerStatisticsRepository.DeleteGamePlayerStatistics(playerId, gameId);

            return NoContent();//success
        }

        [HttpGet("nba/addorupdate")]
        public async Task AddOrUpdateNbaGameStatistics()
        {
            //positionIds: 3 = guard, 5 = small forward, 9 = center
            var data = new PlayerStatistics();
            
            var players = _playerRepository.GetAllPlayers().ToList();

            var gamePlayerStatisticsToAddList = new List<GamePlayerStatistics>();
            var gamePlayerStatisticsToUpdateList = new List<GamePlayerStatistics>();
            var addedGamePlayerStatistics = new List<GamePlayerStatistics>();
            var existingGamePlayerStatistics = _gamePlayerStatisticsRepository.GetAllGamePlayerStatistics().ToList();

            foreach (var player in players)
            {
                var handler = new HttpClientHandler();

                // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
                handler.AutomaticDecompression = ~DecompressionMethods.None;

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


                if(data.SeasonTypes.SelectMany(st => st.Categories).SelectMany(c => c.Events).Select(e => e.Stats).Count() > 0)
                {
                var gameStatsList = data.SeasonTypes
                    .SelectMany(st => st.Categories)
                    .SelectMany(c => c.Events)
                    .ToDictionary(e => e.EventId, e => e.Stats);

                foreach (var gameStats in gameStatsList)
                {
                    var playerFromDb = _playerRepository.GetPlayerByRemoteId(player.RemoteId);
                    var gameFromDb = _gameRepository.GetGameByRemoteId(gameStats.Key);
                    var playerIdFromDb = playerFromDb?.Id;
                    var gameIdFromDb = gameFromDb?.Id;

                    var gamePlayerStatisticsExist = existingGamePlayerStatistics.Any(egps => playerIdFromDb.HasValue && egps.Player.Id == playerIdFromDb && gameIdFromDb.HasValue && egps.Game.Id == gameIdFromDb);

                    if(gameIdFromDb.HasValue && playerIdFromDb.HasValue)
                    {
                        //check if new player
                        if (!gamePlayerStatisticsExist && !addedGamePlayerStatistics.Any(gps => gps.Player.Id == playerIdFromDb && gps.Game.Id == gameIdFromDb))
                        {
                            //fields goals
                            var splitFieldGoals = gameStats.Value[1].Split("-");
                            var fieldGoalsMade = Int32.Parse(splitFieldGoals[0]);
                            var fieldGoalAttempts = Int32.Parse(splitFieldGoals[1]);

                            //threes
                            var splitThrees = gameStats.Value[3].Split("-");
                            var threePointersMade = Int32.Parse(splitThrees[0]);
                            var threePointAttempts = Int32.Parse(splitThrees[1]);

                            //free throws
                            var splitFreeThrows = gameStats.Value[5].Split("-");
                            var freeThrowsMade = Int32.Parse(splitFreeThrows[0]);
                            var freeThrowAttempts = Int32.Parse(splitFreeThrows[1]);

                            //new game-player stats
                            var newGamePlayerStatistics = new GamePlayerStatistics
                            {
                                Player = playerFromDb,
                                Game = gameFromDb,
                                Minutes = int.Parse(gameStats.Value[0]),
                                FieldGoalAttempts = fieldGoalAttempts,
                                FieldGoalsMade = fieldGoalsMade,
                                FieldGoalPercentage = fieldGoalAttempts > 0 ? ((double)fieldGoalsMade / (double)fieldGoalAttempts) * 100 : 0.0,
                                ThreePointAttempts = threePointAttempts,
                                ThreePointersMade = threePointersMade,
                                ThreePointPercentage = threePointAttempts > 0 ? ((double)threePointersMade / (double)threePointAttempts) * 100 : 0.0,
                                FreeThrowAttempts = freeThrowAttempts,
                                FreeThrowsMade = freeThrowsMade,
                                FreeThrowPercentage = freeThrowAttempts > 0 ? ((double)freeThrowsMade / (double)freeThrowAttempts) * 100 : 0.0,
                                Rebounds = int.Parse(gameStats.Value[7]),
                                Assists = int.Parse(gameStats.Value[8]),
                                Blocks = int.Parse(gameStats.Value[9]),
                                Steals = int.Parse(gameStats.Value[10]),
                                PlayerFouls = int.Parse(gameStats.Value[11]),
                                Turnovers = int.Parse(gameStats.Value[12]),
                                Points = int.Parse(gameStats.Value[13])
                            };

                            gamePlayerStatisticsToAddList.Add(newGamePlayerStatistics);
                            addedGamePlayerStatistics.Add(newGamePlayerStatistics);
                        }
                        else
                        {
                            var gamePlayerStatisticsToUpdate = _gamePlayerStatisticsRepository.GetGamePlayerStatisticsById(playerIdFromDb.Value, gameIdFromDb.Value);

                            gamePlayerStatisticsToUpdateList.Add(gamePlayerStatisticsToUpdate);
                        }
                    }
                    //do nothing
                }
                }

            }

            foreach (var gamePlayerStatisticsToUpdate in gamePlayerStatisticsToUpdateList)
            {
                _gamePlayerStatisticsRepository.UpdateGamePlayerStatistics(gamePlayerStatisticsToUpdate);
            }

            foreach (var gamePlayerStatisticsToAdd in gamePlayerStatisticsToAddList)
            {
                _gamePlayerStatisticsRepository.AddGamePlayerStatistics(gamePlayerStatisticsToAdd);
            }

            //var statAverages = data.SeasonTypes.Where(st => st.DisplayName == "2021-22 Regular Season")
            //    .Select(st => st.Summary)
            //    .SelectMany(s => s.Stats).Where(stats => stats.DisplayName == "Averages")
            //    .SelectMany(a => a.Stats).ToList();

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