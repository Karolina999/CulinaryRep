using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class PlannerRecipeRepository : IPlannerRecipeRepository
    {
        private readonly CulinaryDbContext _context;
        public PlannerRecipeRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public bool CreatePlannerRecipe(int recipeId, int plannerId)
        {
            var recipe = _context.Recipes.Where(x => x.Id == recipeId).FirstOrDefault();
            var planner = _context.Planners.Where(x => x.Id == plannerId).FirstOrDefault();

            var plannerRecipe = new PlannerRecipe()
            {
                Recipe = recipe,
                Planner = planner,
            };

            _context.Add(plannerRecipe);
            return Save();
        }

        public bool DeletePlannerRecipe(PlannerRecipe plannerRecipe)
        {
            _context.Remove(plannerRecipe);
            return Save();
        }

        public bool DeletePlannerRecipes(ICollection<PlannerRecipe> plannersRecipes)
        {
            _context.RemoveRange(plannersRecipes);
            return Save();
        }

        public PlannerRecipe GetPlannerRecipe(int recipeId, int plannerId)
        {
            return _context.PlannerRecipes.Where(x => x.RecipeId == recipeId && x.PlannerId == plannerId).FirstOrDefault();
        }

        public ICollection<PlannerRecipe> GetPlannerRecipes()
        {
            return _context.PlannerRecipes.ToList();
        }

        public bool PlannerRecipeExists(int recipeId, int plannerId)
        {
            return _context.PlannerRecipes.Any(x => x.RecipeId == recipeId && x.PlannerId == plannerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
