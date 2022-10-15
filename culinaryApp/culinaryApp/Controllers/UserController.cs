﻿using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        /*[HttpGet("{userId/Recipe}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetUserRecipe(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var recipe = _mapper.Map<RecipeDto>(_userRepository.GetUserRecipe(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipe);
        }*/

        /*[HttpGet("{userId/ShoppingList}")]
        [ProducesResponseType(200, Type = typeof(ShoppingList))]
        [ProducesResponseType(400)]
        public IActionResult GetUserShoppingList(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var shoppingList = _mapper.Map<ShoppingListDto>(_userRepository.GetUserShoppingList(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shoppingList);
        }*/

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
    }
}