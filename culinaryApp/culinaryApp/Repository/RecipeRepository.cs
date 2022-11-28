using culinaryApp.Data;
using culinaryApp.Dto;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CulinaryDbContext _context;

        public RecipeRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public bool CreateRecipe(Recipe recipe)
        {
            _context.Add(recipe);
            return Save();
        }

        public bool DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
            return Save();
        }

        public bool DeleteRecipes(ICollection<Recipe> recipes)
        {
            _context.RemoveRange(recipes);
            return Save();
        }

        public ICollection<PlannerRecipe> GetPlannerRecipeList(ICollection<Recipe> recipes)
        {
            var plannerRecipes = new List<PlannerRecipe>();
            foreach (var recipe in recipes)
            {
                plannerRecipes.AddRange(_context.PlannerRecipes.Where(x => x.Recipe.Id == recipe.Id).ToList());
            }
            return plannerRecipes;
        }

        public ICollection<PlannerRecipe> GetPlannerRecipes(int recipeId)
        {
            return _context.PlannerRecipes.Where(x => x.Recipe.Id == recipeId).ToList();
        }

        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.FirstOrDefault(x => x.Id == id);
        }

        public User GetRecipeAuthor(int userId)
        {
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            return user;
        }

        public ICollection<UserComment> GetRecipeComments(int recipeId)
        {
            return _context.UserComments.Where(x => x.Recipe.Id == recipeId).ToList();
        }

        public ICollection<ProductFromRecipe> GetRecipeProducts(int recipeId)
        {
            return _context.ProductFromRecipes.Where(x => x.Recipe.Id == recipeId).ToList();
        }

        public RatingDto GetRecipeRating(int recipeId)
        {
            var review = _context.UserComments.Where(x => x.Recipe.Id == recipeId);
            
            RatingDto rating = new RatingDto()
            {
                Rating = 0,
                NumberOfReviews = 0,
            };

            if (review.Count() <= 0)
                return rating;

            rating.Rating = (decimal)review.Sum(x => x.Rating) / review.Count();
            rating.NumberOfReviews = review.Count();

            return rating;
        }

        public ICollection<Recipe> GetRecipes()
        {
            return _context.Recipes.OrderBy(x => x.Id).ToList();
        }

        public ICollection<Recipe> GetRecipes(string title)
        {
            return _context.Recipes.OrderBy(x => x.Id).Where(x => x.Title == title).ToList();
        }

        public ICollection<UserComment> GetRecipesComments(ICollection<Recipe> recipes)
        {
            var comments = new List<UserComment>();
            foreach (var recipe in recipes)
            {
                comments.AddRange(_context.UserComments.Where(x => x.Recipe.Id == recipe.Id).ToList());
            }
            return comments;
        }

        public ICollection<ProductFromRecipe> GetRecipesProducts(ICollection<Recipe> recipes)
        {
            var products = new List<ProductFromRecipe>();
            foreach (var recipe in recipes)
            {
                products.AddRange(_context.ProductFromRecipes.Where(x => x.Recipe.Id == recipe.Id).ToList());
            }
            return products;
        }

        public ICollection<Step> GetRecipesSteps(ICollection<Recipe> recipes)
        {
            var steps = new List<Step>();
            foreach (var recipe in recipes)
            {
                steps.AddRange(_context.Steps.Where(x => x.Recipe.Id == recipe.Id).ToList());
            }
            return steps;
        }

        public ICollection<Step> GetRecipeSteps(int recipeId)
        {
            return _context.Steps.Where(x => x.Recipe.Id == recipeId).OrderBy(x => x.StepNumber).ToList();
        }

        public ICollection<WatchedRecipe> GetWatchedRecipes(int recipeId)
        {
            return _context.WatchedRecipes.Where(x => x.Recipe.Id == recipeId).ToList();
        }

        public ICollection<WatchedRecipe> GetWatchedRecipesList(ICollection<Recipe> recipes)
        {
            var watchedRecipes = new List<WatchedRecipe>();
            foreach (var recipe in recipes)
            {
                watchedRecipes.AddRange(_context.WatchedRecipes.Where(x => x.Recipe.Id == recipe.Id).ToList());
            }
            return watchedRecipes;
        }

        public bool RecipeExists(int recipeId)
        {
            return _context.Recipes.Any(x => x.Id == recipeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRecipe(Recipe recipe)
        {
            _context.Update(recipe);
            return Save();
        }
    }
}
