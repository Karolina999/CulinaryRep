using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class PlannerRepository : IPlannerRepository
    {
        private readonly CulinaryDbContext _context;

        public PlannerRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public bool CreatePlanner(Planner planner)
        {
            _context.Add(planner);
            return Save();
        }

        public bool DeletePlanner(Planner planner)
        {
            _context.Remove(planner);
            return Save();
        }

        public bool DeletePlanners(ICollection<Planner> planners)
        {
            _context.RemoveRange(planners);
            return Save();
        }

        public Planner GetPlanner(int id)
        {
            return _context.Planners.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<ProductFromPlanner> GetPlannerProducts(int plannerId)
        {
            return _context.ProductFromPlanners.Where(x => x.Planner.Id == plannerId).ToList();
        }

        public ICollection<PlannerRecipe> GetPlannerRecipes(int plannerId)
        {
            return _context.PlannerRecipes.Where(x => x.PlannerId == plannerId).ToList();
        }

        public ICollection<Planner> GetPlanners()
        {
            return _context.Planners.OrderBy(x => x.Id).ToList();
        }

        public ICollection<ProductFromPlanner> GetPlannersProducts(ICollection<Planner> planners)
        {
            var products = new List<ProductFromPlanner>();
            foreach(var planner in planners)
            {
                products.AddRange(_context.ProductFromPlanners.Where(x => x.Planner.Id == planner.Id).ToList());
            }
            return products;
        }

        public ICollection<PlannerRecipe> GetPlannersRecipes(ICollection<Planner> planners)
        {
            var plannerRecipes = new List<PlannerRecipe>();
            foreach (var planner in planners)
            {
                plannerRecipes.AddRange(_context.PlannerRecipes.Where(x => x.PlannerId == planner.Id).ToList());
            }
            return plannerRecipes;
        }

        public bool PlannerExists(int plannerId)
        {
            return _context.Planners.Any(x => x.Id == plannerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePlanner(Planner planner)
        {
            _context.Update(planner);
            return Save();
        }
    }
}
