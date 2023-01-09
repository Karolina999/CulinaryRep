using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IPlannerRecipeRepository
    {
        ICollection<PlannerRecipe> GetPlannerRecipes();
        PlannerRecipe GetPlannerRecipe(int plannerRecipeId);
        ICollection<PlannerRecipe> GetPlannerRecipes(int plannerId);
        ICollection<Recipe> GetRecipes(int plannerId);
        bool PlannerRecipeExists(int plannerRecipeId);
        bool CreatePlannerRecipe(int recipeId, int plannerId, MealTypes mealType);
        bool DeletePlannerRecipe(PlannerRecipe plannerRecipe);
        bool DeletePlannerRecipes(ICollection<PlannerRecipe> plannersRecipes);
        bool Save();
    }
}
