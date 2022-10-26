using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class WatchedRecipeRepository : IWatchedRecipeRepository
    {
        private readonly CulinaryDbContext _context;
        public WatchedRecipeRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public bool CreateWatchedRecipe(int recipeId, int userId)
        {
            var recipe = _context.Recipes.Where(x => x.Id == recipeId).FirstOrDefault();
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();

            var watchedRecipe = new WatchedRecipe()
            {
                Recipe = recipe,
                User = user,
            };

            _context.Add(watchedRecipe);
            return Save();
        }

        public bool DeleteWatchedRecipe(WatchedRecipe watchedRecipe)
        {
            _context.Remove(watchedRecipe);
            return Save();
        }

        public bool DeleteWatchedRecipes(ICollection<WatchedRecipe> watchedRecipes)
        {
            _context.RemoveRange(watchedRecipes);
            return Save();
        }

        public WatchedRecipe GetWatchedRecipe(int recipeId, int userId)
        {
            return _context.WatchedRecipes.Where(x => x.RecipeId == recipeId && x.UserId == userId).FirstOrDefault();
        }

        public ICollection<WatchedRecipe> GetWatchedRecipes()
        {
            return _context.WatchedRecipes.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool WatchedRecipeExists(int recipeId, int userId)
        {
            return _context.WatchedRecipes.Any(x => x.RecipeId == recipeId && x.UserId == userId);
        }
    }
}
