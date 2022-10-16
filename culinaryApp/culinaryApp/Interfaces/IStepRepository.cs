using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IStepRepository
    {
        ICollection<Step> GetSteps();
        Step GetStep(int id);
        bool StepExists(int stepId);
        bool CreateStep(Step step);
        bool UpdateStep(Step step);
        bool Save();
    }
}
