using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IPlannerRepository
    {
        ICollection<Planner> GetPlanners();
        ICollection<Planner> GetUserPlanners(int userId);
        Planner GetUserPlanner(int userId, DateTime date);
        Planner GetPlanner(int id);
        ICollection<ProductFromPlanner> GetPlannerProducts(int plannerId);
        ICollection<ProductFromPlanner> GetPlannersProducts(ICollection<Planner> planners);
      //  ICollection<PlannerRecipe> GetPlannerRecipes(int plannerId);
      //  ICollection<PlannerRecipe> GetPlannersRecipes(ICollection<Planner> planners);
        bool PlannerExists(int plannerId);
        bool CreatePlanner(Planner planner);
        bool UpdatePlanner(Planner planner);
        bool DeletePlanner(Planner planner);
        bool DeletePlanners(ICollection<Planner> planners);
        bool Save();
    }
}
