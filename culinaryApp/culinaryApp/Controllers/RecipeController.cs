using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using culinaryApp.Data;
using culinaryApp.Models;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly CulinaryDbContext _context;
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
        }
    }
}
