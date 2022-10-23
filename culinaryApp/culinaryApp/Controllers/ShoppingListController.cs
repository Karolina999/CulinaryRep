using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductFromListRepository _productRepository;
        private readonly IMapper _mapper;

        public ShoppingListController(IShoppingListRepository shoppingListRepository, IUserRepository userRepository, IProductFromListRepository productRepository, IMapper mapper)
        {
            _shoppingListRepository = shoppingListRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoppingList>))]
        public IActionResult GetShoppingLists()
        {
            var shoppingList = _mapper.Map<List<ShoppingListDto>>(_shoppingListRepository.GetShoppingLists());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shoppingList);
        }

        [HttpGet("{shoppingListId}")]
        [ProducesResponseType(200, Type = typeof(ShoppingList))]
        [ProducesResponseType(400)]
        public IActionResult GetShoppingList(int shoppingListId)
        {
            if (!_shoppingListRepository.ShoppingListExists(shoppingListId))
                return NotFound();

            var shoppingList = _mapper.Map<ShoppingListDto>(_shoppingListRepository.GetShoppingList(shoppingListId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shoppingList);
        }

        [HttpGet("{shoppingListId}/products")]
        [ProducesResponseType(200, Type = typeof(ProductFromList))]
        [ProducesResponseType(400)]
        public IActionResult GetProductFromList(int shoppingListId)
        {
            if (!_shoppingListRepository.ShoppingListExists(shoppingListId))
                return NotFound();

            var products = _mapper.Map<ICollection<ProductFromListDto>>(_shoppingListRepository.GetProductsFromList(shoppingListId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateShoppingList([FromQuery] int userId, [FromBody] ShoppingListDto shoppingList)
        {
            if (shoppingList == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shoppingListMap = _mapper.Map<ShoppingList>(shoppingList);

            shoppingListMap.User = _userRepository.GetUser(userId);

            if (!_shoppingListRepository.CreateShoppingList(shoppingListMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{shoppingListId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateShoppingList(int shoppingListId, [FromBody] ShoppingListDto updateShoppingList)
        {
            if (updateShoppingList == null)
                return BadRequest(ModelState);

            if (shoppingListId != updateShoppingList.Id)
                return BadRequest(ModelState);

            if (!_shoppingListRepository.ShoppingListExists(shoppingListId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var shoppingListMap = _mapper.Map<ShoppingList>(updateShoppingList);

            if (!_shoppingListRepository.UpdateShoppingList(shoppingListMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{shoppingListId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteShoppingList(int shoppingListId)
        {
            if (!_shoppingListRepository.ShoppingListExists(shoppingListId))
                return NotFound();

            var productToDelete = _shoppingListRepository.GetProductsFromList(shoppingListId);
            var shoppingListToDelete = _shoppingListRepository.GetShoppingList(shoppingListId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (productToDelete.Count() > 0 && !_productRepository.DeleteProductsFromList(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (!_shoppingListRepository.DeleteShoppingList(shoppingListToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
