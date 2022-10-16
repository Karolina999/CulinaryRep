using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IPlannerRepository
    {
        ICollection<Planner> GetPlanners();
        Planner GetPlanner(int id);
        bool PlannerExists(int plannerId);
        bool CreatePlanner(Planner planner);
        bool UpdatePlanner(Planner planner);
        bool Save();
    }
}
