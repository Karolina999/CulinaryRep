﻿using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly CulinaryDbContext _context;
        public IngredientRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public bool CreateIngredient(Ingredient ingredient)
        {
            _context.Add(ingredient);
            return Save();
        }

        public bool DeleteIngredient(Ingredient ingredient)
        {
            _context.Remove(ingredient);
            return Save();
        }

        public Ingredient GetIngredient(int id)
        {
            return _context.Ingredients.FirstOrDefault(x => x.Id == id);
        }

        public Ingredient GetIngredient(string name)
        {
            return _context.Ingredients.FirstOrDefault(x => x.Name == name);
        }

        public ICollection<Ingredient> GetIngredients()
        {
            return _context.Ingredients.OrderBy(x => x.Id).ToList();
        }

        public bool IngredientExists(int ingredientId)
        {
            return _context.Ingredients.Any(x => x.Id == ingredientId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateIngredient(Ingredient ingredient)
        {
            _context.Update(ingredient);
            return Save();
        }
    }
}
