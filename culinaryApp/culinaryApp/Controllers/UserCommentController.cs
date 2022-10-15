using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCommentController : ControllerBase
    {
        private readonly IUserCommentRepository _userCommentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public UserCommentController(IUserCommentRepository userCommentRepository, IUserRepository userRepository, IRecipeRepository recipeRepository, IMapper mapper)
        {
            _userCommentRepository = userCommentRepository;
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserComment>))]
        public IActionResult GetuserComments()
        {
            var userComments = _mapper.Map<List<UserCommentDto>>(_userCommentRepository.GetUserComments());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userComments);
        }

        [HttpGet("{userCommentId}")]
        [ProducesResponseType(200, Type = typeof(UserComment))]
        [ProducesResponseType(400)]
        public IActionResult GetuserComment(int userCommentId)
        {
            if (!_userCommentRepository.UserCommentExists(userCommentId))
                return NotFound();

            var userComment = _mapper.Map<UserCommentDto>(_userCommentRepository.GetUserComment(userCommentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userComment);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserComment([FromQuery] int userId, [FromQuery] int recipeId, [FromBody] UserCommentDto userCommentCreate)
        {
            if (userCommentCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userCommentMap = _mapper.Map<UserComment>(userCommentCreate);

            userCommentMap.User = _userRepository.GetUser(userId);
            userCommentMap.Recipe = _recipeRepository.GetRecipe(recipeId);

            if (!_userCommentRepository.CreateUserComment(userCommentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
    }
}
