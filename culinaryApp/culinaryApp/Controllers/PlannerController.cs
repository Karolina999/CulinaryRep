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
        private readonly IMapper _mapper;

        public PlannerController(IPlannerRepository plannerRepository, IMapper mapper)
        {
            _plannerRepository = plannerRepository;
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

        /*[HttpGet("{ownerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Planner>))]
        [ProducesResponseType(400)]
        public IActionResult GetPlannersOfOwner(int ownerId)
        {
            if (!_plannerRepository.PlanerExists(plannerId))
                return NotFound();

            var planners = _mapper.Map<List<PlannerDto>>(_plannerRepository.GetPlannersOfOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(planners);
        }*/

    }
}
