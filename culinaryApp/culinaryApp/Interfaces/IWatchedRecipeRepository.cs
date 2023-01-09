using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IWatchedRecipeRepository
    {
        ICollection<WatchedRecipe> GetWatchedRecipes();
        WatchedRecipe GetWatchedRecipe(int recipeId, int userId);
        bool WatchedRecipeExists(int recipeId, int userId);
        bool CreateWatchedRecipe(int recipeId, int userId);
        bool DeleteWatchedRecipe(WatchedRecipe watchedRecipe);
        bool DeleteWatchedRecipes(ICollection<WatchedRecipe> watchedRecipes);
        bool Save();
    }
}
