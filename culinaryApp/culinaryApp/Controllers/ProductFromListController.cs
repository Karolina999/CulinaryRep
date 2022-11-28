using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFromListController : ControllerBase
    {
        private readonly IProductFromListRepository _productRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IMapper _mapper;

        public ProductFromListController(IProductFromListRepository productRepository, IIngredientRepository ingredientRepository, IShoppingListRepository shoppingListRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _ingredientRepository = ingredientRepository;
            _shoppingListRepository = shoppingListRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductFromList>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductFromListDto>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(ProductFromList))]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
                return NotFound();

            var product = _mapper.Map<ProductFromListDto>(_productRepository.GetProduct(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductFromList([FromQuery] int ingredientId, [FromQuery] int shoppingListId, [FromBody] ProductFromListDto productFromListCreate)
        {
            if (productFromListCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productFromLisMap = _mapper.Map<ProductFromList>(productFromListCreate);

            productFromLisMap.Ingredient = _ingredientRepository.GetIngredient(ingredientId);

            productFromLisMap.ShoppingList = _shoppingListRepository.GetShoppingList(shoppingListId);

            if (!_productRepository.CreateProductFromList(productFromLisMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{productFromListId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProductFromList(int productFromListId, [FromBody] ProductFromListDto updateProductFromList)
        {
            if (updateProductFromList == null)
                return BadRequest(ModelState);

            if (productFromListId != updateProductFromList.Id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(productFromListId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var productFromListMap = _mapper.Map<ProductFromList>(updateProductFromList);

            if (!_productRepository.UpdateProductFromList(productFromListMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{productFromListId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProductFromList(int productFromListId)
        {
            if (!_productRepository.ProductExists(productFromListId))
                return NotFound();

            var productToDelete = _productRepository.GetProduct(productFromListId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProductFromList(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("productsFromList")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProductsFromList(int[] productsFromListId)
        {
            var productsFromListToDelete = new List<ProductFromList>();

            foreach (var productFromListId in productsFromListId)
            {
                if (!_productRepository.ProductExists(productFromListId))
                    return NotFound();

                var productToDelete = _productRepository.GetProduct(productFromListId);
                productsFromListToDelete.Add(productToDelete);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProductsFromList(productsFromListToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
