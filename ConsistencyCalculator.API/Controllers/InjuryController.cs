using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConsistencyCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InjuryController : Controller
    {
        private readonly IInjuryRepository _injuryRepository;

        public InjuryController(IInjuryRepository injuryRepository)
        {
            _injuryRepository = injuryRepository;
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
    }
}
