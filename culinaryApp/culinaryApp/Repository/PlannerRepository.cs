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

        public Planner GetPlanner(int id)
        {
            return _context.Planners.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Planner> GetPlanners()
        {
            return _context.Planners.OrderBy(x => x.Id).ToList();
        }

        public bool PlanerExists(int plannerId)
        {
            return _context.Planners.Any(x => x.Id == plannerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
