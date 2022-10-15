using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IPlannerRepository
    {
        ICollection<Planner> GetPlanners();
        Planner GetPlanner(int id);
        bool PlanerExists(int plannerId);
        bool CreatePlanner(Planner planner);
        bool Save();
    }
}
