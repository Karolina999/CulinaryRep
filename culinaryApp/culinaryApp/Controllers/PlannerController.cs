using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;
using Microsoft.AspNetCore.Authorization;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannerController : ControllerBase
    {
        private readonly IPlannerRepository _plannerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductFromPlannerRepository _productFromPlannerRepository;
        private readonly IPlannerRecipeRepository _plannerRecipeRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public PlannerController(IPlannerRepository plannerRepository, IUserRepository userRepository, IProductFromPlannerRepository productFromPlannerRepository, IPlannerRecipeRepository plannerRecipeRepository, IRecipeRepository recipeRepository, IMapper mapper)
        {
            _plannerRepository = plannerRepository;
            _userRepository = userRepository;
            _productFromPlannerRepository = productFromPlannerRepository;
            _plannerRecipeRepository = plannerRecipeRepository;
            _recipeRepository = recipeRepository;
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

        [Authorize]
        [HttpGet("fromUser")]
        [ProducesResponseType(200, Type = typeof(GetPlannerDto))]
        public IActionResult GetUserPlanner([FromQuery] DateTime date)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var planner = _mapper.Map<GetPlannerDto>(_plannerRepository.GetUserPlanner(userId, date));

            if(planner == null)
            {
                return Ok(null);
            }

            var plannerRecipes = _mapper.Map<List<GetPlannerRecipeDto>>(_plannerRecipeRepository.GetPlannerRecipes(planner.Id));
            foreach (var pr in plannerRecipes)
            {
                pr.Recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(pr.RecipeId));
            }
            planner.PlannerRecipes = plannerRecipes;

            var products = _mapper.Map<List<GetProductFromPlannerDto>>(_productFromPlannerRepository.GetPlannerProducts(planner.Id));
            foreach (var product in products)
            {
                product.Ingredient = _productFromPlannerRepository.GetIngredientFromProduct(product.IngredientId);
            }
            planner.Products = products;


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(planner);
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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePlanner([FromBody] PlannerDto plannerCreate)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

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

        [Authorize]
        [HttpDelete("{plannerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePlanner(int plannerId)
        {
            if (!_plannerRepository.PlannerExists(plannerId))
                return NotFound();

            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var plannerToDelete = _plannerRepository.GetPlanner(plannerId);

            if(plannerToDelete.UserId != userId)
                return Forbid();

            var productsToDelete = _plannerRepository.GetPlannerProducts(plannerId);
            var plannerRecipesToDelete = _plannerRepository.GetPlannerRecipes(plannerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (plannerRecipesToDelete.Count() > 0 && !_plannerRecipeRepository.DeletePlannerRecipes(plannerRecipesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

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
