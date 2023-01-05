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
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (userCommentCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isOwner = _userRepository.GetUserRecipes(userId)
                .Where(x => x.Id == recipeId)
                .FirstOrDefault();

            if (isOwner != null)
            {
                return Problem("User cannot comment his own recipe");
            }

            var alreadyCommented = _userCommentRepository.UserCommentRecipeExists(userId, recipeId);

            if (alreadyCommented)
            {
                return Problem("A user has already commented on this recipe");
            }

            var userCommentMap = _mapper.Map<UserComment>(userCommentCreate);

            userCommentMap.User = _userRepository.GetUser(userId);
            userCommentMap.Recipe = _recipeRepository.GetRecipe(recipeId);

            if (!_userCommentRepository.CreateUserComment(userCommentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var recipe = _recipeRepository.GetRecipe(recipeId);
            var recipeRating = _recipeRepository.GetRecipeRating(recipeId);

            recipe.Rating = Decimal.ToDouble(recipeRating.Rating);

            if (!_recipeRepository.UpdateRecipe(recipe))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{userCommentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserComment(int userCommentId, [FromBody] UserCommentDto updateUserComment)
        {
            if (updateUserComment == null)
                return BadRequest(ModelState);

            if (userCommentId != updateUserComment.Id)
                return BadRequest(ModelState);

            if (!_userCommentRepository.UserCommentExists(userCommentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userCommentMap = _mapper.Map<UserComment>(updateUserComment);

            if (!_userCommentRepository.UpdateUserComment(userCommentMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userCommentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserComment(int userCommentId)
        {
            if (!_userCommentRepository.UserCommentExists(userCommentId))
                return NotFound();

            var userCommentToDelete = _userCommentRepository.GetUserComment(userCommentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userCommentRepository.DeleteUserComment(userCommentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
