using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace culinaryApp.Data
{
    public class CulinaryDbContext : DbContext
    {
        public CulinaryDbContext(DbContextOptions<CulinaryDbContext> options) :base(options)
        { }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
