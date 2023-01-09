using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannerRecipeController : ControllerBase
    {
        private readonly IPlannerRecipeRepository _plannerRecipeRepository;
        private readonly IMapper _mapper;
       
        public PlannerRecipeController(IPlannerRecipeRepository plannerRecipeRepository, IMapper mapper)
        {
            _plannerRecipeRepository = plannerRecipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PlannerRecipe>))]
        public IActionResult GePlannersRecipes()
        {
            var plannersRecipes = _mapper.Map<List<PlannerRecipeDto>>(_plannerRecipeRepository.GetPlannerRecipes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(plannersRecipes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWatchedRecipes([FromQuery] int recipeId, [FromQuery] int plannerId, [FromQuery] MealTypes mealType)
        {

            var plannerRecipe = _plannerRecipeRepository.GetPlannerRecipes()
                .Where(x => x.PlannerId == plannerId)
                .Where(x => x.RecipeId == recipeId)
                .Where(x => x.MealType == mealType)
                .FirstOrDefault();

            if (plannerRecipe != null)
            {
                ModelState.AddModelError("", "Planner Recipe already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_plannerRecipeRepository.CreatePlannerRecipe(recipeId, plannerId, mealType))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePlannerRecipe(int plannerRecipeId)
        {
            if (!_plannerRecipeRepository.PlannerRecipeExists(plannerRecipeId))
                return NotFound();

            var plannerRecipeToDelete = _plannerRecipeRepository.GetPlannerRecipe(plannerRecipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_plannerRecipeRepository.DeletePlannerRecipe(plannerRecipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
