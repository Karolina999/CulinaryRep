using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannerController : ControllerBase
    {
        private readonly IPlannerRepository _plannerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PlannerController(IPlannerRepository plannerRepository, IUserRepository userRepository, IMapper mapper)
        {
            _plannerRepository = plannerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Planner>))]
        public IActionResult GetPlanners()
        {
            var planners = _mapper.Map<List<PlannerDto>>(_plannerRepository.GetPlanners());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(planners);
        }

        [HttpGet("{plannerId}")]
        [ProducesResponseType(200, Type = typeof(Planner))]
        [ProducesResponseType(400)]
        public IActionResult GetPlaner(int plannerId)
        {
            if (!_plannerRepository.PlanerExists(plannerId))
                return NotFound();

            var planner = _mapper.Map<PlannerDto>(_plannerRepository.GetPlanner(plannerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(planner);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePlanner([FromQuery] int userId, [FromBody] PlannerDto plannerCreate)
        {
            if (plannerCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var plannerMap = _mapper.Map<Planner>(plannerCreate);

            plannerMap.User = _userRepository.GetUser(userId);

            if (!_plannerRepository.CreatePlanner(plannerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
    }
}
