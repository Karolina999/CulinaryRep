using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;
using culinaryApp.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IProductFromRecipeRepository _productFromRecipeRepository;
        private readonly IStepRepository _stepRepository;
        private readonly IUserCommentRepository _commentRepository;
        private readonly IPlannerRepository _plannerRepository;
        private readonly IProductFromPlannerRepository _productFromPlannerRepository;
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IProductFromListRepository _productFromListRepository;
        private readonly IWatchedRecipeRepository _watchedRecipeRepository;
        private readonly IPlannerRecipeRepository _plannerRecipeRepository;
        private readonly IMapper _mapper;
        private readonly IAuth _auth;

        public UserController(
            IUserRepository userRepository,
            IRecipeRepository recipeRepository,
            IProductFromRecipeRepository productFromRecipeRepository,
            IStepRepository stepRepository,
            IUserCommentRepository commentRepository,
            IPlannerRepository plannerRepository,
            IProductFromPlannerRepository productFromPlannerRepository,
            IShoppingListRepository shoppingListRepository,
            IProductFromListRepository productFromListRepository,
            IWatchedRecipeRepository watchedRecipeRepository,
            IPlannerRecipeRepository plannerRecipeRepository,
            IAuth auth,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
            _productFromRecipeRepository = productFromRecipeRepository;
            _stepRepository = stepRepository;
            _commentRepository = commentRepository;
            _plannerRepository = plannerRepository;
            _productFromPlannerRepository = productFromPlannerRepository;
            _shoppingListRepository = shoppingListRepository;
            _productFromListRepository = productFromListRepository;
            _watchedRecipeRepository = watchedRecipeRepository;
            _plannerRecipeRepository = plannerRecipeRepository;
            _mapper = mapper;
            _auth = auth;
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public ActionResult Login(UserLoginDto userLoginDto)
        {
            var user = _userRepository.LoginUser(userLoginDto.Login, userLoginDto.Password);
            if (user == null)
                return Problem("Wrong password or login");

            var token = _auth.Authentication(user);
            HttpContext.Response.Headers.Add("jwt", token);

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
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

        [Authorize]
        [HttpGet("user")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("{userId}/comments")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserComment>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserComments(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var comments = _mapper.Map<List<UserCommentDto>>(_userRepository.GetUserComments(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);
        }

        [HttpGet("{userId}/recipes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserRecipes(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var recipes = _mapper.Map<List<RecipeDto>>(_userRepository.GetUserRecipes(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipes);
        }

        [Authorize]
        [HttpGet("shoppingLists")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoppingList>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserShoppingLists()
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            var shoppingLists = _mapper.Map<List<ShoppingListDto>>(_userRepository.GetUserShoppingList(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(shoppingLists);
        }

        [HttpGet("{userId}/planners")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoppingList>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserPlanners(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var planners = _mapper.Map<List<Planner>>(_userRepository.GetUserPlanners(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(planners);
        }

        [HttpGet("{userId}/watchedRecipes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public IActionResult GetWatchedRecipes(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var watchedRecipes = _mapper.Map<List<RecipeDto>>(_userRepository.GetUserWatchedRecipes(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(watchedRecipes);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userWithEmail = _userRepository.GetUsers()
                .Where(x => x.Email == userCreate.Email)
                .FirstOrDefault();

            if (userWithEmail is not null)
            {
                return Problem("There is a user with this e-mail");
            }

            var userMap = _mapper.Map<User>(userCreate);
            userMap.Password = BCrypt.Net.BCrypt.HashPassword(userCreate.Password);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [Authorize]
        [HttpPut()]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser([FromBody] UserDto updateUser)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            if (updateUser == null)
                return BadRequest(ModelState);

            if (userId != updateUser.Id)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var user = _userRepository.GetUser(userId);

            var userWithEmail = _userRepository.GetUserByEmail(updateUser.Email);

            if (userWithEmail is not null && userWithEmail?.Id != user.Id)
            {
                return Problem("There is a user with this e-mail");
            }

            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.Email = updateUser.Email;
            user.Photo = updateUser.ImageUrl;

            var userMap = _mapper.Map<User>(user);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [Authorize]
        [HttpPut("password")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserPassword([FromQuery] string newPassword, [FromQuery] string oldPassword)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var user = _userRepository.GetUser(userId);

            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
            {
                return Problem("Bad old password");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            var userMap = _mapper.Map<User>(user);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var userToDelete = _userRepository.GetUser(userId);

            var recipesToDelete = _userRepository.GetUserRecipes(userId);
            var stepsFromRecipesToDelete = _recipeRepository.GetRecipesSteps(recipesToDelete);
            var commentsFromRecipeToDelete = _recipeRepository.GetRecipesComments(recipesToDelete);
            var productsFromRecipeToDelete = _recipeRepository.GetRecipesProducts(recipesToDelete);
            var plannerRecipeFromRecipeToDelete = _recipeRepository.GetPlannerRecipeList(recipesToDelete);

            var watchedRecipesToDelete = _userRepository.GetUserWatched(userId);

            if (watchedRecipesToDelete.Count() > 0 && !_watchedRecipeRepository.DeleteWatchedRecipes(watchedRecipesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (plannerRecipeFromRecipeToDelete.Count() > 0 && !_plannerRecipeRepository.DeletePlannerRecipes(plannerRecipeFromRecipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (productsFromRecipeToDelete.Count() > 0 && !_productFromRecipeRepository.DeleteProductsFromRecipe(productsFromRecipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (commentsFromRecipeToDelete.Count() > 0 && !_commentRepository.DeleteUserComments(commentsFromRecipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (stepsFromRecipesToDelete.Count() > 0 && !_stepRepository.DeleteSteps(stepsFromRecipesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (recipesToDelete.Count() > 0 && !_recipeRepository.DeleteRecipes(recipesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            var commentsToDelete = _userRepository.GetUserComments(userId);

            if (commentsToDelete.Count() > 0 && !_commentRepository.DeleteUserComments(commentsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            var plannersToDelete = _userRepository.GetUserPlanners(userId);
            var productFromPlannersToDelete = _plannerRepository.GetPlannersProducts(plannersToDelete);
            var plannerRecipesToDelete = _plannerRepository.GetPlannersRecipes(plannersToDelete);

            if (productFromPlannersToDelete.Count() > 0 && !_productFromPlannerRepository.DeleteProductsFromPlanner(productFromPlannersToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (plannerRecipesToDelete.Count() > 0 && !_plannerRecipeRepository.DeletePlannerRecipes(plannerRecipesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (plannersToDelete.Count() > 0 && !_plannerRepository.DeletePlanners(plannersToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            var shoppingListToDelete = _userRepository.GetUserShoppingList(userId);
            var productFromListToDelete = _shoppingListRepository.GetProductsFromLists(shoppingListToDelete);

            if (shoppingListToDelete.Count() > 0 && !_shoppingListRepository.DeleteShoppingLists(shoppingListToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (productFromListToDelete.Count() > 0 && !_productFromListRepository.DeleteProductsFromList(productFromListToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
