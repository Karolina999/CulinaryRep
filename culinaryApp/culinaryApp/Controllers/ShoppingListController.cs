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

        [Authorize]
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

            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            if (shoppingList.UserId != userId)
                return Forbid();

            return Ok(shoppingList);
        }

        [Authorize]
        [HttpGet("{shoppingListId}/products")]
        [ProducesResponseType(200, Type = typeof(ProductFromList))]
        [ProducesResponseType(400)]
        public IActionResult GetProductFromList(int shoppingListId)
        {
            if (!_shoppingListRepository.ShoppingListExists(shoppingListId))
                return NotFound();

            var shoppingList = _mapper.Map<ShoppingListDto>(_shoppingListRepository.GetShoppingList(shoppingListId));

            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            if (shoppingList.UserId != userId)
                return Forbid();

            var products = _mapper.Map<ICollection<ProductFromListGetDto>>(_shoppingListRepository.GetProductsFromList(shoppingListId));

            foreach (var product in products)
            {
                product.Ingredient = _productRepository.GetIngredientFromProduct(product.IngredientId);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateShoppingList([FromBody] ShoppingListDto shoppingList)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

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

            return Ok(shoppingListMap);

        }

        [Authorize]
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

            var shoppingList = _shoppingListRepository.GetShoppingList(shoppingListId);

            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            if (shoppingList.UserId != userId)
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest();

            shoppingList.Title = updateShoppingList.Title;

            if (!_shoppingListRepository.UpdateShoppingList(shoppingList))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
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

            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            if (shoppingListToDelete.UserId != userId)
                return Forbid();

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

        [Authorize]
        [HttpDelete("shoppingLists")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteShoppingLists(int[] shoppingListsId)
        {
            var shoppingListsToDelete = new List<ShoppingList>();
            var productsToDelete = new List<ProductFromList>();

            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            foreach (var shoppingListId in shoppingListsId)
            {
                if (!_shoppingListRepository.ShoppingListExists(shoppingListId))
                    return NotFound();

                var productToDelete = _shoppingListRepository.GetProductsFromList(shoppingListId);
                productsToDelete.AddRange(productToDelete);
                var shoppingListToDelete = _shoppingListRepository.GetShoppingList(shoppingListId);
                shoppingListsToDelete.Add(shoppingListToDelete);

                if (shoppingListToDelete.UserId != userId)
                    return Forbid();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (productsToDelete.Count() > 0 && !_productRepository.DeleteProductsFromList(productsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (!_shoppingListRepository.DeleteShoppingLists(shoppingListsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
