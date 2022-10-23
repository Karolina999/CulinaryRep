using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IPlannerRepository
    {
        ICollection<Planner> GetPlanners();
        ICollection<ProductFromPlanner> GetPlannerProducts(int plannerId);
        ICollection<ProductFromPlanner> GetPlannersProducts(ICollection<Planner> planners);
        Planner GetPlanner(int id);
        bool PlannerExists(int plannerId);
        bool CreatePlanner(Planner planner);
        bool UpdatePlanner(Planner planner);
        bool DeletePlanner(Planner planner);
        bool DeletePlanners(ICollection<Planner> planners);
        bool Save();
    }
}
