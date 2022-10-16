using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        ICollection<Recipe> GetRecipes(string title);
        ICollection<Step> GetRecipeSteps(int recipeId);
        ICollection<UserComment> GetRecipeComments(int recipeId);
        decimal GetRecipeRating(int recipeId);
        bool RecipeExists(int recipeId);
        bool CreateRecipe(Recipe recipe);
        bool UpdateRecipe(Recipe recipe);
        bool Save();

    }
}
