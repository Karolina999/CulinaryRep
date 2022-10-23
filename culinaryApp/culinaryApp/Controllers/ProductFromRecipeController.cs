using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFromRecipeController : ControllerBase
    {
        private readonly IProductFromRecipeRepository _productRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public ProductFromRecipeController(IProductFromRecipeRepository productRepository, IIngredientRepository ingredientRepository, IRecipeRepository recipeRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductFromRecipe>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductFromRecipeDto>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(ProductFromRecipe))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var product = _mapper.Map<ProductFromRecipeDto>(_productRepository.GetProduct(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductFromRecipe([FromQuery] int recipeId, [FromQuery] int ingredientId, [FromBody] ProductFromRecipeDto productFromRecipeCreate)
        {
            if (productFromRecipeCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productFromRecipeMap = _mapper.Map<ProductFromRecipe>(productFromRecipeCreate);

            productFromRecipeMap.Recipe = _recipeRepository.GetRecipe(recipeId);
            productFromRecipeMap.Ingredient = _ingredientRepository.GetIngredient(ingredientId);

            if(!_productRepository.CreateProductFromRecipe(productFromRecipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{productFromRecipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProductFromRecipe(int productFromRecipeId, [FromBody] ProductFromRecipeDto updateProductFromRecipe)
        {
            if (updateProductFromRecipe == null)
                return BadRequest(ModelState);

            if (productFromRecipeId != updateProductFromRecipe.Id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(productFromRecipeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var productFromRecipeMap = _mapper.Map<ProductFromRecipe>(updateProductFromRecipe);

            if (!_productRepository.UpdateProductFromRecipe(productFromRecipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productFromRecipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProductFromRecipe(int productFromRecipeId)
        {
            if (!_productRepository.ProductExists(productFromRecipeId))
                return NotFound();

            var productToDelete = _productRepository.GetProduct(productFromRecipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProductFromRecipe(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
