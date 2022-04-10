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
    public class InjuryController : Controller
    {
        private readonly IInjuryRepository _injuryRepository;
        private readonly IPlayerRepository _playerRepository;

        public InjuryController(IInjuryRepository injuryRepository, IPlayerRepository playerRepository)
        {
            _injuryRepository = injuryRepository;
            _playerRepository = playerRepository;
        }

        [HttpGet]
        public IActionResult GetAllInjuries()
        {
            return Ok(_injuryRepository.GetAllInjuries());
        }

        [HttpGet("{id}")]
        public IActionResult GetInjuryById(int id)
        {
            return Ok(_injuryRepository.GetInjuryById(id));
        }

        [HttpPost]
        public IActionResult CreateInjury([FromBody] Injury injury)
        {
            if (injury == null)
                return BadRequest();

            if (injury.Detail == string.Empty)
            {
                ModelState.AddModelError("Detail", "The detail shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdInjury = _injuryRepository.AddInjury(injury);

            return Created("injury", createdInjury);
        }

        [HttpPut]
        public IActionResult UpdateInjury([FromBody] Injury injury)
        {
            if (injury == null)
                return BadRequest();

            if (injury.Detail == string.Empty)
            {
                ModelState.AddModelError("Detail", "The detail shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var injuryToUpdate = _injuryRepository.GetInjuryById(injury.Id);

            if (injuryToUpdate == null)
                return NotFound();

            _injuryRepository.UpdateInjury(injury);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInjury(int id)
        {
            if (id == 0)
                return BadRequest();

            var injuryToDelete = _injuryRepository.GetInjuryById(id);
            if (injuryToDelete == null)
                return NotFound();

            _injuryRepository.DeleteInjury(id);

            return NoContent();//success
        }

        [HttpGet("nba/refresh")]
        public async Task RefreshNbaPlayerInjuries()
        {
            var players = _playerRepository.GetAllPlayers();
            var injuriesToAdd = new List<Injury>();
            var playerResponse = new PlayerResponse();
            var handler = new HttpClientHandler();

            foreach (var player in players)
            {
                var playerEndpoint = $"http://sports.core.api.espn.com/v2/sports/basketball/leagues/nba/seasons/2022/athletes/{player.RemoteId}?lang=en&region=us";

                // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
                handler.AutomaticDecompression = ~DecompressionMethods.None;

  
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), playerEndpoint))
                    {
                        var response = await httpClient.SendAsync(request);
                        string responseBody = await response.Content.ReadAsStringAsync();
                        playerResponse = JsonSerializer.Deserialize<PlayerResponse>(responseBody);
                    }
                }

                foreach(var injury in playerResponse.Injuries)
                {
                    var injuryToAdd = new Injury
                    {
                        RemoteId = injury.Id,
                        Detail = injury.Details.Detail,
                        DateString = DateTime.Parse(injury.Date),
                        Side = injury.Details.Side,
                        LongComment = injury.LongComment,
                        ShortComment = injury.ShortComment,
                        Location = injury.Details.Location,
                        Type = injury.Details.Type,
                        Status = injury.Status,
                        Player = player
                    };

                    injuriesToAdd.Add(injuryToAdd);
                }
            }

            //remove all existing entries and add new
            try
            {
                _injuryRepository.DeleteAllInjuries();

                foreach (var injuryToAdd in injuriesToAdd)
                {
                    _injuryRepository.AddInjury(injuryToAdd);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
