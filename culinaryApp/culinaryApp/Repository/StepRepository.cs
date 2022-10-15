using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class StepRepository : IStepRepository
    {
        private readonly CulinaryDbContext _context;

        public StepRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public Step GetStep(int id)
        {
            return _context.Steps.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Step> GetSteps()
        {
            return _context.Steps.OrderBy(x => x.Id).ToList();
        }

        public bool StepExists(int stepId)
        {
            return _context.Steps.Any(x => x.Id == stepId);
        }
    }
}
