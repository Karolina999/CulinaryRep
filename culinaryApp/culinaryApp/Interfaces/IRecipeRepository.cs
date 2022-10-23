using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        ICollection<Recipe> GetRecipes(string title);
        ICollection<Step> GetRecipeSteps(int recipeId);
        ICollection<Step> GetRecipesSteps(ICollection<Recipe> recipes);
        ICollection<ProductFromRecipe> GetRecipeProducts(int recipeId);
        ICollection<ProductFromRecipe> GetRecipesProducts(ICollection<Recipe> recipes);
        ICollection<UserComment> GetRecipeComments(int recipeId);
        ICollection<UserComment> GetRecipesComments(ICollection<Recipe> recipes);
        decimal GetRecipeRating(int recipeId);
        bool RecipeExists(int recipeId);
        bool CreateRecipe(Recipe recipe);
        bool UpdateRecipe(Recipe recipe);
        bool DeleteRecipe(Recipe recipe);
        bool DeleteRecipes(ICollection<Recipe> recipes);
        bool Save();

    }
}
