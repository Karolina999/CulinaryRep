﻿using culinaryApp.Data;
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

        public bool CreateStep(Step step)
        {
            _context.Add(step);
            return Save();
        }

        public bool CreateSteps(ICollection<Step> steps)
        {
            _context.AddRange(steps);
            return Save();
        }

        public bool DeleteStep(Step step)
        {
            _context.Remove(step);
            return Save();
        }

        public bool DeleteSteps(ICollection<Step> steps)
        {
            _context.RemoveRange(steps);
            return Save();
        }

        public Step GetStep(int id)
        {
            return _context.Steps.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Step> GetSteps()
        {
            return _context.Steps.OrderBy(x => x.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool StepExists(int stepId)
        {
            return _context.Steps.Any(x => x.Id == stepId);
        }

        public bool UpdateStep(Step step)
        {
            _context.Update(step);
            return Save();
        }
    }
}
