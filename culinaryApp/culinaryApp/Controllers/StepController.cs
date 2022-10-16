using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IStepRepository _stepRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public StepController(IStepRepository stepRepository, IRecipeRepository recipeRepository, IMapper mapper)
        {
            _stepRepository = stepRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Step>))]
        public IActionResult GetSteps()
        {
            var steps = _mapper.Map<List<StepDto>>(_stepRepository.GetSteps());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(steps);
        }

        [HttpGet("{stepId}")]
        [ProducesResponseType(200, Type = typeof(Step))]
        [ProducesResponseType(400)]
        public IActionResult GetStep(int stepId)
        {
            if (!_stepRepository.StepExists(stepId))
                return NotFound();

            var step = _mapper.Map<StepDto>(_stepRepository.GetStep(stepId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(step);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStep([FromQuery] int recipeId, [FromBody] StepDto stepCreate)
        {
            if (stepCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stepMap = _mapper.Map<Step>(stepCreate);

            stepMap.Recipe = _recipeRepository.GetRecipe(recipeId); 

            if (!_stepRepository.CreateStep(stepMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{stepId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStep(int stepId, [FromBody] StepDto updateStep)
        {
            if (updateStep == null)
                return BadRequest(ModelState);

            if (stepId != updateStep.Id)
                return BadRequest(ModelState);

            if (!_stepRepository.StepExists(stepId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var stepMap = _mapper.Map<Step>(updateStep);

            if (!_stepRepository.UpdateStep(stepMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
