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
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ITeamRepository _teamRepository;

        public PlayerController(IPlayerRepository playerRepository, IPositionRepository positionRepository, ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _positionRepository = positionRepository;
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public IActionResult GetAllPlayers()
        {
            return Ok(_playerRepository.GetAllPlayers());
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayerById(int id)
        {
            return Ok(_playerRepository.GetPlayerById(id));
        }

        [HttpGet("remote/{id}")]
        public IActionResult GetPlayerByRemoteId(string id)
        {
            return Ok(_playerRepository.GetPlayerByRemoteId(id));
        }

        [HttpPost]
        public IActionResult CreatePlayer([FromBody] Player player)
        {
            if (player == null)
                return BadRequest();

            if (player.FirstName == string.Empty || player.LastName == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPlayer = _playerRepository.AddPlayer(player);

            return Created("player", createdPlayer);
        }

        [HttpPut]
        public IActionResult UpdatePlayer([FromBody] Player player)
        {
            if (player == null)
                return BadRequest();

            if (player.FirstName == string.Empty || player.LastName == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var playerToUpdate = _playerRepository.GetPlayerById(player.Id);

            if (playerToUpdate == null)
                return NotFound();

            _playerRepository.UpdatePlayer(player);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            if (id == 0)
                return BadRequest();

            var playerToDelete = _playerRepository.GetPlayerById(id);
            if (playerToDelete == null)
                return NotFound();

            _playerRepository.DeletePlayer(id);

            return NoContent();//success
        }

        [HttpGet("nba/addorupdate")]
        public async Task AddOrUpdateNbaPlayers()
        {
            var teams = _teamRepository.GetAllTeams();

            var playersToAdd = new List<Player>();
            var positionsToAdd = new List<Models.Entities.Position>();
            var playersToUpdate = new List<Player>();
            var addedPlayerIds = new List<int>();
            var addedPositionIds = new List<int>();
            var positions = _positionRepository.GetAllPositions();
            var players = _playerRepository.GetAllPlayers();

            //var positions = new List<Models.Entities.Position>();

            foreach (var team in teams)
            {
                var playersResponse = new PlayersByTeamResponse();
                var player = new PlayerResponse();
                var handler = new HttpClientHandler();

                // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
                handler.AutomaticDecompression = ~DecompressionMethods.None;

                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"http://sports.core.api.espn.com/v2/sports/basketball/leagues/nba/seasons/2022/teams/{team.RemoteId}/athletes?lang=en&region=us"))
                    {
                        var response = await httpClient.SendAsync(request);
                        string responseBody = await response.Content.ReadAsStringAsync();
                        playersResponse = JsonSerializer.Deserialize<PlayersByTeamResponse>(responseBody);
                    }
                }

                var playerEndpoints = playersResponse.Items
                    .Select(i => i.Ref).ToList();

                foreach (var playerEndpoint in playerEndpoints)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), playerEndpoint))
                        {
                            var response = await httpClient.SendAsync(request);
                            string responseBody = await response.Content.ReadAsStringAsync();
                            player = JsonSerializer.Deserialize<PlayerResponse>(responseBody);
                        }
                    }

                    if (!addedPlayerIds.Contains(Int32.Parse(player.Id)) && !players.Any(p => p.RemoteId == player.Id))
                    {
                        var newPlayer = new Player
                        {
                            RemoteId = player.Id,
                            FirstName = player.FirstName,
                            LastName = player.LastName,
                            FullName = player.FullName,
                            Age = player.Age,
                            Weight = player.Weight,
                            Height = player.Height,
                            Team = team,
                            Injuries = player.Injuries.Select(i => new Injury
                            {
                                RemoteId = i.Id,
                                LongComment = i.LongComment,
                                ShortComment = i.ShortComment,
                                Status = i.Status,
                                DateString = i.Date,
                                Type = i.Details.Type,
                                Location = i.Details.Location,
                                Side = i.Details.Side,
                                Detail = i.Details.Detail
                            }).ToList()
                        };

                        var position = positions.Where(p => p.RemoteId == player.Position.Id).FirstOrDefault();

                        if (position == null)
                        {
                            position = new Models.Entities.Position
                            {
                                RemoteId = player.Position.Id,
                                Name = player.Position.Name,
                                Abbreviation = player.Position.Abbreviation
                            };
                        }

                        newPlayer.Position = position;

                        addedPlayerIds.Add(newPlayer.Id);
                        playersToAdd.Add(newPlayer);
                    }
                    else
                    {
                        var playerToUpdate = _playerRepository.GetPlayerByRemoteId(player.Id);

                        playersToUpdate.Add(playerToUpdate);
                    }
                }
            }

            //foreach (var positionToAdd in positionsToAdd)
            //{
            //    if (!addedPositionIds.Contains(positionToAdd.Id))
            //    {
            //        _positionRepository.AddPosition(positionToAdd);
            //        addedPositionIds.Add(positionToAdd.Id);
            //    }
            //}

            foreach (var playerToUpdate in playersToUpdate)
            {
                _playerRepository.UpdatePlayer(playerToUpdate);
            }

            foreach (var playerToAdd in playersToAdd)
            {
                _playerRepository.AddPlayer(playerToAdd);
            }

            //foreach (var playerToAdd in playersToAdd)
            //{
            //    playerToAdd.Position = positionsSavedToDb.Where(p => p.RemoteId == playerToAdd.Position.RemoteId).FirstOrDefault();
            //    _playerRepository.AddPlayer(playerToAdd);
            //}
        }
    }
}
