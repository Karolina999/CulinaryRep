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
        private readonly IMapper _mapper;

        public StepController(IStepRepository stepRepository, IMapper mapper)
        {
            _stepRepository = stepRepository;
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
        public IActionResult Getstep(int stepId)
        {
            if (!_stepRepository.StepExists(stepId))
                return NotFound();

            var step = _mapper.Map<StepDto>(_stepRepository.GetStep(stepId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(step);
        }
    }
}
