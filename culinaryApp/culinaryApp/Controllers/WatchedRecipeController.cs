using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchedRecipeController : ControllerBase
    {
        private readonly IWatchedRecipeRepository _watchedRecipeRepository;
       /* private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;*/
        private readonly IMapper _mapper;

        public WatchedRecipeController(
            IWatchedRecipeRepository watchedRecipeRepository,
            /*IRecipeRepository recipeRepository,
            IUserRepository userRepository,*/
            IMapper mapper
            )
        {
            _watchedRecipeRepository = watchedRecipeRepository;
            /*_recipeRepository = recipeRepository;
            _userRepository = userRepository;*/
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WatchedRecipe>))]
        public IActionResult GetWatchedRecipes()
        {
            var watchedRecipes = _mapper.Map<List<WatchedRecipeDto>>(_watchedRecipeRepository.GetWatchedRecipes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(watchedRecipes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWatchedRecipes([FromQuery] int recipeId, [FromQuery] int userId)
        {

            var watchedRecipe = _watchedRecipeRepository.GetWatchedRecipes()
                .Where(x => x.UserId == userId)
                .Where(x => x.RecipeId == recipeId)
                .FirstOrDefault();

            if (watchedRecipe != null)
            {
                ModelState.AddModelError("", "Watched Recipe already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_watchedRecipeRepository.CreateWatchedRecipe(recipeId, userId))
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
        public IActionResult DeleteWatchedRecipe(int recipeId, int userId)
        {
            if (!_watchedRecipeRepository.WatchedRecipeExists(recipeId, userId))
                return NotFound();

            var watchedRecipeToDelete = _watchedRecipeRepository.GetWatchedRecipe(recipeId, userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_watchedRecipeRepository.DeleteWatchedRecipe(watchedRecipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
