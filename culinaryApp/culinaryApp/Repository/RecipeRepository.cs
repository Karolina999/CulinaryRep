using culinaryApp.Data;
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

        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.FirstOrDefault(x => x.Id == id);
        }

        public decimal GetRecipeRating(int recipeId)
        {
            var review = _context.UserComments.Where(x => x.Recipe.Id == recipeId);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(x => x.Rating) / review.Count());
        }

        public ICollection<Recipe> GetRecipes()
        {
            return _context.Recipes.OrderBy(x => x.Id).ToList();
        }

        public ICollection<Recipe> GetRecipes(string title)
        {
            return _context.Recipes.OrderBy(x => x.Id).Where(x => x.Title == title).ToList();
        }

        public ICollection<Step> GetRecipeSteps(int recipeId)
        {
            return _context.Steps.Where(x => x.Recipe.Id == recipeId).OrderBy(x => x.StepNumber).ToList();
        }

        public bool RecipeExists(int recipeId)
        {
            return _context.Recipes.Any(x => x.Id == recipeId);
        }
    }
}
