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
        private readonly IMapper _mapper;

        public ShoppingListController(IShoppingListRepository shoppingListRepository, IMapper mapper)
        {
            _shoppingListRepository = shoppingListRepository;
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
    }
}
