﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IProductFromPlannerRepository _productFromPlannerRepository;
        private readonly IMapper _mapper;

        public PlannerController(IPlannerRepository plannerRepository, IUserRepository userRepository, IProductFromPlannerRepository productFromPlannerRepository, IMapper mapper)
        {
            _plannerRepository = plannerRepository;
            _userRepository = userRepository;
            _productFromPlannerRepository = productFromPlannerRepository;
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
            if (!_plannerRepository.PlannerExists(plannerId))
                return NotFound();

            var planner = _mapper.Map<PlannerDto>(_plannerRepository.GetPlanner(plannerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(planner);
        }

        [HttpGet("{plannerId}/products")]
        [ProducesResponseType(200, Type = typeof(Planner))]
        [ProducesResponseType(400)]
        public IActionResult GetPlannerProducts(int plannerId)
        {
            if (!_plannerRepository.PlannerExists(plannerId))
                return NotFound();

            var products = _mapper.Map<List<ProductFromPlannerDto>>(_plannerRepository.GetPlannerProducts(plannerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
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

        [HttpPut("{plannerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePlanner(int plannerId, [FromBody] PlannerDto updatePlanner)
        {
            if (updatePlanner == null)
                return BadRequest(ModelState);

            if (plannerId != updatePlanner.Id)
                return BadRequest(ModelState);

            if (!_plannerRepository.PlannerExists(plannerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var plannerMap = _mapper.Map<Planner>(updatePlanner);

            if (!_plannerRepository.UpdatePlanner(plannerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{plannerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePlanner(int plannerId)
        {
            if (!_plannerRepository.PlannerExists(plannerId))
                return NotFound();

            var plannerToDelete = _plannerRepository.GetPlanner(plannerId);
            var productsToDelete = _plannerRepository.GetPlannerProducts(plannerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (productsToDelete.Count() > 0 && !_productFromPlannerRepository.DeleteProductsFromPlanner(productsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (!_plannerRepository.DeletePlanner(plannerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        
        }
    }
}
