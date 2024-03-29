﻿using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientController(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ingredient>))]
        public IActionResult GetIngredients()
        {
            var ingredients = _mapper.Map<List<IngredientDto>>(_ingredientRepository.GetIngredients());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ingredients);
        }


        [HttpGet("{ingredientId}")]
        [ProducesResponseType(200, Type = typeof(Ingredient))]
        [ProducesResponseType(400)]
        public IActionResult GetIngredient(int ingredientId)
        {
            if (!_ingredientRepository.IngredientExists(ingredientId))
                return NotFound();

            var ingredient = _mapper.Map<IngredientDto>(_ingredientRepository.GetIngredient(ingredientId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ingredient);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateIngredient([FromBody] IngredientDto ingredientCreate)
        {
            if (ingredientCreate == null)
                return BadRequest(ModelState);

            var ingredient = _ingredientRepository.GetIngredients()
                .Where(x => x.Name.Trim().ToUpper() == ingredientCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (ingredient != null)
            {
                ModelState.AddModelError("", "Ingredient already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredientMap = _mapper.Map<Ingredient>(ingredientCreate);

            if (!_ingredientRepository.CreateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{ingredientId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateIngredient(int ingredientId, [FromBody] IngredientDto updateIngredient)
        {
            if (updateIngredient == null)
                return BadRequest(ModelState);

            if (ingredientId != updateIngredient.Id)
                return BadRequest(ModelState);

            if (!_ingredientRepository.IngredientExists(ingredientId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ingredientMap = _mapper.Map<Ingredient>(updateIngredient);

            if (!_ingredientRepository.UpdateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{ingredientId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteIngredient(int ingredientId)
        {
            if (!_ingredientRepository.IngredientExists(ingredientId))
                return NotFound();

            var ingredientToDelete = _ingredientRepository.GetIngredient(ingredientId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!_ingredientRepository.DeleteIngredient(ingredientToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
