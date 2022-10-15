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

        public Planner GetPlanner(int id)
        {
            return _context.Planners.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Planner> GetPlanners()
        {
            return _context.Planners.OrderBy(x => x.Id).ToList();
        }

        /*public ICollection<Planner> GetPlannersOfOwner(int ownerId)
        {
            return _context.Planners.Where(x => x.User.Id == ownerId).ToList();
        }*/

        public bool PlanerExists(int plannerId)
        {
            return _context.Planners.Any(x => x.Id == plannerId);
        }
    }
}
