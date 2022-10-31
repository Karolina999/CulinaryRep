using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IPlannerRecipeRepository
    {
        ICollection<PlannerRecipe> GetPlannerRecipes();
        PlannerRecipe GetPlannerRecipe(int recipeId, int plannerId);
        bool PlannerRecipeExists(int recipeId, int plannerId);
        bool CreatePlannerRecipe(int recipeId, int plannerId);
        bool DeletePlannerRecipe(PlannerRecipe plannerRecipe);
        bool DeletePlannerRecipes(ICollection<PlannerRecipe> plannersRecipes);
        bool Save();
    }
}
