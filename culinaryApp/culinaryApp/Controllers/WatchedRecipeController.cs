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
    public class WatchedRecipeController : ControllerBase
    {
        private readonly IWatchedRecipeRepository _watchedRecipeRepository;
        private readonly IMapper _mapper;

        public WatchedRecipeController(IWatchedRecipeRepository watchedRecipeRepository, IMapper mapper)
        {
            _watchedRecipeRepository = watchedRecipeRepository;
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

        [Authorize]
        [HttpGet("isWatched")]
        public IActionResult RecipeIsWatched([FromQuery] int recipeId)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var watchedRecipe = _watchedRecipeRepository.GetWatchedRecipes()
                 .Where(x => x.UserId == userId)
                 .Where(x => x.RecipeId == recipeId)
                 .FirstOrDefault();

            if (watchedRecipe != null)
            {
                return Problem("Watched Recipe already exists");
            }

            return Ok("Watched Recipe do not exists");
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWatchedRecipes([FromQuery] int recipeId)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var watchedRecipe = _watchedRecipeRepository.GetWatchedRecipes()
                .Where(x => x.UserId == userId)
                .Where(x => x.RecipeId == recipeId)
                .FirstOrDefault();

            if (watchedRecipe != null)
            {
                return Problem("Watched Recipe already exists");
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

        [Authorize]
        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWatchedRecipe([FromQuery] int recipeId)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

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
