using culinaryApp.Dto;
using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();
        ICollection<Recipe> GetRecipesIncludes(string title);
        Recipe GetRecipe(int id);
        ICollection<Recipe> GetRecipes(string title);
        ICollection<Step> GetRecipeSteps(int recipeId);
        ICollection<Step> GetRecipesSteps(ICollection<Recipe> recipes);
        ICollection<ProductFromRecipe> GetRecipeProducts(int recipeId);
        ICollection<ProductFromRecipe> GetRecipesProducts(ICollection<Recipe> recipes);
        User GetRecipeAuthor(int userId);
        ICollection<UserComment> GetRecipeComments(int recipeId);
        ICollection<UserComment> GetRecipesComments(ICollection<Recipe> recipes);
        ICollection<WatchedRecipe> GetWatchedRecipes(int recipeId);
        ICollection<WatchedRecipe> GetWatchedRecipesList(ICollection<Recipe> recipes);
        ICollection<PlannerRecipe> GetPlannerRecipes(int recipeId);
        ICollection<PlannerRecipe> GetPlannerRecipeList(ICollection<Recipe> recipes);
        RatingDto GetRecipeRating(int recipeId);
        bool RecipeExists(int recipeId);
        bool CreateRecipe(Recipe recipe);
        bool UpdateRecipe(Recipe recipe);
        bool DeleteRecipe(Recipe recipe);
        bool DeleteRecipes(ICollection<Recipe> recipes);
        bool Save();

    }
}
