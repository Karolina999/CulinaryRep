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

        public bool CreatePlannerRecipe(int recipeId, int plannerId, MealTypes mealType)
        {
            var recipe = _context.Recipes.Where(x => x.Id == recipeId).FirstOrDefault();
            var planner = _context.Planners.Where(x => x.Id == plannerId).FirstOrDefault();

            var plannerRecipe = new PlannerRecipe()
            {
                Recipe = recipe,
                Planner = planner,
                MealType = mealType
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

        public PlannerRecipe GetPlannerRecipe(int plannerRecipeId)
        {
            return _context.PlannerRecipes.Where(x => x.Id == plannerRecipeId).FirstOrDefault();
        }

        public ICollection<PlannerRecipe> GetPlannerRecipes()
        {
            return _context.PlannerRecipes.ToList();
        }

        public ICollection<PlannerRecipe> GetPlannerRecipes(int plannerId)
        {
            return _context.PlannerRecipes.Where(x => x.PlannerId == plannerId).ToList();
        }

        public ICollection<Recipe> GetRecipes(int plannerId)
        {
            var plannerRecipe = _context.PlannerRecipes.Where(x => x.PlannerId == plannerId);
            var recipes = new List<Recipe>();
            foreach(var pr in plannerRecipe)
            {
                recipes.AddRange(_context.Recipes.Where(x => x.Id == pr.RecipeId).ToList());
            }

            return recipes;
        }

        public bool PlannerRecipeExists(int plannerRecipeId)
        {
            return _context.PlannerRecipes.Any(x => x.Id == plannerRecipeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
