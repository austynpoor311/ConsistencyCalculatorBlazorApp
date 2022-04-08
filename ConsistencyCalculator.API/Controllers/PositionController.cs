using ConsistencyCalculator.Data.Repositories.Interfaces;
using ConsistencyCalculator.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConsistencyCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : Controller
    {
        private readonly IPositionRepository _positionRepository;

        public PositionController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        [HttpGet]
        public IActionResult GetAllPositions()
        {
            return Ok(_positionRepository.GetAllPositions());
        }

        [HttpGet("{id}")]
        public IActionResult GetPositionById(int id)
        {
            return Ok(_positionRepository.GetPositionById(id));
        }

        [HttpPost]
        public IActionResult CreatePosition([FromBody] Position position)
        {
            if (position == null)
                return BadRequest();

            if (position.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPosition = _positionRepository.AddPosition(position);

            return Created("position", createdPosition);
        }

        [HttpPut]
        public IActionResult UpdatePosition([FromBody] Position position)
        {
            if (position == null)
                return BadRequest();

            if (position.Name  == string.Empty)
            {
                ModelState.AddModelError("Name", "The name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var positionToUpdate = _positionRepository.GetPositionById(position.Id);

            if (positionToUpdate == null)
                return NotFound();

            _positionRepository.UpdatePosition(position);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePosition(int id)
        {
            if (id == 0)
                return BadRequest();

            var positionToDelete = _positionRepository.GetPositionById(id);
            if (positionToDelete == null)
                return NotFound();

            _positionRepository.DeletePosition(id);

            return NoContent();//success
        }
    }
}
