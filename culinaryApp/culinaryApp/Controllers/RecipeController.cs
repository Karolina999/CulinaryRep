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
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStepRepository _stepRepository;
        private readonly IProductFromRecipeRepository _productRepository;
        private readonly IUserCommentRepository _commentRepository;
        private readonly IWatchedRecipeRepository _watchedRecipeRepository;
        private readonly IPlannerRecipeRepository _plannerRecipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository,
            IUserRepository userRepository,
            IStepRepository stepRepository,
            IProductFromRecipeRepository productRepository,
            IUserCommentRepository commentRepository,
            IWatchedRecipeRepository watchedRecipeRepository,
            IPlannerRecipeRepository plannerRecipeRepository,
            IIngredientRepository ingredientRepository,
            IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
            _stepRepository = stepRepository;
            _productRepository = productRepository;
            _commentRepository = commentRepository;
            _watchedRecipeRepository = watchedRecipeRepository;
            _plannerRecipeRepository = plannerRecipeRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()
        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetRecipes());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipes);
        }

        [HttpGet("top12")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetTopRecipes()
        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetTopRecipes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipes);
        }

        [HttpGet("includes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipesIncludes([FromQuery] string title )
        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetRecipesIncludes(title));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipes);
        }

        [HttpGet("{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(recipeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipe);
        }


        [HttpGet("{recipeId}/rating")]
        [ProducesResponseType(200, Type = typeof(RatingDto))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeRating(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var rating = _recipeRepository.GetRecipeRating(recipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(rating);
        }

        [HttpGet("{recipeId}/steps")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Step>))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeSteps(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var steps = _mapper.Map<List<StepDto>>(_recipeRepository.GetRecipeSteps(recipeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(steps);
        }

        [HttpGet("{recipeId}/comments")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserComment>))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeComments(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            /*var comments = _mapper.Map<List<UserCommentDto>>(_recipeRepository.GetRecipeComments(recipeId));*/

            var comments = _recipeRepository.GetRecipeComments(recipeId);

            foreach (var comment in comments)
            {
                comment.User = _userRepository.GetUser(comment.UserId);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);
        }

        [HttpGet("{recipeId}/products")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductFromRecipe>))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeIngredients(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var products = _mapper.Map<List<ProductFromRecipeGetDto>>(_recipeRepository.GetRecipeProducts(recipeId));

            foreach (var product in products)
            {
                product.Ingredient = _productRepository.GetIngredientFromProduct(product.IngredientId);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("{recipeId}/author")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeAuthor(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(recipeId));

            var author = _mapper.Map<UserDto>(_recipeRepository.GetRecipeAuthor(recipe.OwnerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(author);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipe([FromBody] RecipeProductsStepsDto recipeCreate)
        {
            var userId = int.Parse(User.Claims.First(x => x.Type == "id").Value);

            if (recipeCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeMap = _mapper.Map<Recipe>(recipeCreate);
            
            recipeMap.Owner = _userRepository.GetUser(userId);

            if (!_recipeRepository.CreateRecipe(recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            var productsFromRecipeToCreate = new List<ProductFromRecipe>();
            var productsCreate = recipeCreate.Products;

            if (productsCreate.Count() > 0)
            {
                foreach (var productCreate in productsCreate)
                {
                    var productMap = _mapper.Map<ProductFromRecipe>(productCreate);
                    productMap.Ingredient = _ingredientRepository.GetIngredient(productCreate.IngredientId);
                    productMap.Recipe = _recipeRepository.GetRecipe(recipeMap.Id);
                    productsFromRecipeToCreate.Add(productMap);
                }

                if (!_productRepository.CreateProductsFromRecipe(productsFromRecipeToCreate))
                {
                    ModelState.AddModelError("", "Something went wrong while saving products");
                    return StatusCode(500, ModelState);
                }
            }

            var stepsToCreate = new List<Step>();
            var stepsCreate = recipeCreate.Steps;

            if (stepsCreate.Count() > 0)
            {
                foreach(var stepCreate in stepsCreate)
                {
                    var stepMap = _mapper.Map<Step>(stepCreate);
                    stepMap.Recipe = _recipeRepository.GetRecipe(recipeMap.Id);
                    stepsToCreate.Add(stepMap);
                }

                if (!_stepRepository.CreateSteps(stepsToCreate))
                {
                    ModelState.AddModelError("", "Something went wrong while saving steps");
                    return StatusCode(500, ModelState);
                }
            }

            return Ok("Successfully created");
        }

        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRecipe(int recipeId, [FromBody] RecipeDto updateRecipe)
        {
            if (updateRecipe == null)
                return BadRequest(ModelState);

            if (recipeId != updateRecipe.Id)
                return BadRequest(ModelState);

            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var RecipeMap = _mapper.Map<Recipe>(updateRecipe);

            if (!_recipeRepository.UpdateRecipe(RecipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var recipeToDelete = _recipeRepository.GetRecipe(recipeId);
            var stepsToDelete = _recipeRepository.GetRecipeSteps(recipeId);
            var productsToDelete = _recipeRepository.GetRecipeProducts(recipeId);
            var commentsToDelete = _recipeRepository.GetRecipeComments(recipeId);
            var watchedRecipesToDelete = _recipeRepository.GetWatchedRecipes(recipeId);
            var plannerRecipesToDelete = _recipeRepository.GetPlannerRecipes(recipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (plannerRecipesToDelete.Count() > 0 && !_plannerRecipeRepository.DeletePlannerRecipes(plannerRecipesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (watchedRecipesToDelete.Count() > 0 && !_watchedRecipeRepository.DeleteWatchedRecipes(watchedRecipesToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (stepsToDelete.Count() > 0 && !_stepRepository.DeleteSteps(stepsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (productsToDelete.Count() > 0 && !_productRepository.DeleteProductsFromRecipe(productsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (commentsToDelete.Count() > 0 && !_commentRepository.DeleteUserComments(commentsToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            if (!_recipeRepository.DeleteRecipe(recipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}