using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        ICollection<Recipe> GetRecipes(string title);
        decimal GetRecipeRating(int recipeId);
        bool RecipeExists(int recipeId);

    }
}
