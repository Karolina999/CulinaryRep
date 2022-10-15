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
    }
}
