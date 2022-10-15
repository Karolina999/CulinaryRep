using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IPlannerRepository
    {
        ICollection<Planner> GetPlanners();
        /*ICollection<Planner> GetPlannersOfOwner(int ownerId);*/
        Planner GetPlanner(int id);
        bool PlanerExists(int plannerId);
    }
}
