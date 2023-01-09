using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace culinaryApp.Data
{
    public class CulinaryDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Planner> Planners { get; set; }
        public DbSet<ProductFromList> ProductFromLists { get; set; }
        public DbSet<ProductFromPlanner> ProductFromPlanners { get; set; }
        public DbSet<ProductFromRecipe> ProductFromRecipes { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<WatchedRecipe> WatchedRecipes { get; set; }
        public DbSet<PlannerRecipe> PlannerRecipes { get; set; }
        public CulinaryDbContext(DbContextOptions<CulinaryDbContext> options) :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
