using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using culinaryApp.Data;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()
        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetRecipes());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipes);
        }

        [HttpGet("{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(recipeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipe);
        }

        [HttpGet("{recipeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeRating(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var rating = _recipeRepository.GetRecipeRating(recipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }

    }
}

/* private readonly CulinaryDbContext _context;
         public RecipeController(CulinaryDbContext context) => _context = context;

         [HttpGet]
         public async Task<IEnumerable<Recipe>> Get() => await _context.Recipes.ToListAsync();

         [HttpGet("{id}")]
         [ProducesResponseType(typeof(Recipe), StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status404NotFound)]
         public async Task<IActionResult> GetById(int id)
         {
             var recipe = await _context.Recipes.FindAsync(id);
             return recipe == null ? NotFound() : Ok(recipe);
         }

         [HttpPost]
         [ProducesResponseType(StatusCodes.Status201Created)]
         public async Task<IActionResult> Create(Recipe recipe)
         {
             await _context.Recipes.AddAsync(recipe);
             await _context.SaveChangesAsync();

             return CreatedAtAction(nameof(GetById), new { id = recipe.Id }, recipe);
         }

         [HttpPut("{id}")]
         [ProducesResponseType(StatusCodes.Status204NoContent)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         public async Task<IActionResult> Update(int id, Recipe recipe)
         {
             if (id != recipe.Id) return BadRequest();

             _context.Entry(recipe).State = EntityState.Modified;
             await _context.SaveChangesAsync();

             return NoContent();
         }

         [HttpDelete("{id}")]
         [ProducesResponseType(StatusCodes.Status204NoContent)]
         [ProducesResponseType(StatusCodes.Status404NotFound)]
         public async Task<IActionResult> Delete(int id)
         {
             var recipeToDelete = await _context.Recipes.FindAsync(id);
             if (recipeToDelete == null) return NotFound();

             _context.Recipes.Remove(recipeToDelete);
             await _context.SaveChangesAsync();

             return NoContent();
         }*/