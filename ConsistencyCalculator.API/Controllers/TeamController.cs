using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConsistencyCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public IActionResult GetAllTeams()
        {
            return Ok(_teamRepository.GetAllTeams());
        }

        [HttpGet("{id}")]
        public IActionResult GetTeamById(int id)
        {
            return Ok(_teamRepository.GetTeamById(id));
        }

        [HttpPost]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            if (team == null)
                return BadRequest();

            if (team.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTeam = _teamRepository.AddTeam(team);

            return Created("team", createdTeam);
        }

        [HttpPut]
        public IActionResult UpdateTeam([FromBody] Team team)
        {
            if (team == null)
                return BadRequest();

            if (team.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teamToUpdate = _teamRepository.GetTeamById(team.Id);

            if (teamToUpdate == null)
                return NotFound();

            _teamRepository.UpdateTeam(team);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            if (id == 0)
                return BadRequest();

            var teamToDelete = _teamRepository.GetTeamById(id);
            if (teamToDelete == null)
                return NotFound();

            _teamRepository.DeleteTeam(id);

            return NoContent();//success
        }
    }
}
