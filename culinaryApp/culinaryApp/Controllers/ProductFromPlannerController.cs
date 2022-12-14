using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFromPlannerController : ControllerBase
    {
        private readonly IProductFromPlannerRepository _productRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IPlannerRepository _plannerRepository;
        private readonly IMapper _mapper;

        public ProductFromPlannerController(IProductFromPlannerRepository productRepository, IIngredientRepository ingredientRepository, IPlannerRepository plannerRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _ingredientRepository = ingredientRepository;
            _plannerRepository = plannerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductFromPlanner>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductFromPlannerDto>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(ProductFromPlanner))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var product = _mapper.Map<ProductFromPlannerDto>(_productRepository.GetProduct(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductFromPlanner([FromQuery] int ingredientId, [FromQuery] int plannerId, [FromBody] ProductFromPlannerDto productFromPlannerCreate)
        {
            if (productFromPlannerCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productFromPlannerMap = _mapper.Map<ProductFromPlanner>(productFromPlannerCreate);

            productFromPlannerMap.Ingredient = _ingredientRepository.GetIngredient(ingredientId);
            productFromPlannerMap.Planner = _plannerRepository.GetPlanner(plannerId);

            if (!_productRepository.CreateProductFromPlanner(productFromPlannerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{productFromPlannerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProductFromPlanner(int productFromPlannerId,[FromQuery] int ingredientId, [FromBody] ProductFromPlannerDto updateProductFromPlanner)
        {
            if (updateProductFromPlanner == null)
                return BadRequest(ModelState);

            if (productFromPlannerId != updateProductFromPlanner.Id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(productFromPlannerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var productFromPlannerMap = _mapper.Map<ProductFromPlanner>(updateProductFromPlanner);

            productFromPlannerMap.Ingredient = _ingredientRepository.GetIngredient(ingredientId);

            if (!_productRepository.UpdateProductFromPlanner(productFromPlannerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productFromPlannerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProductFromPlanner(int productFromPlannerId)
        {
            if (!_productRepository.ProductExists(productFromPlannerId))
                return NotFound();

            var productToDelete = _productRepository.GetProduct(productFromPlannerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProductFromPlanner(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
