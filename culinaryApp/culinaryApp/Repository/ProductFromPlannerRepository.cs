using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class ProductFromPlannerRepository : IProductFromPlannerRepository
    {
        private readonly CulinaryDbContext _context;

        public ProductFromPlannerRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public ProductFromPlanner GetProduct(int id)
        {
            return _context.ProductFromPlanners.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<ProductFromPlanner> GetProducts()
        {
            return _context.ProductFromPlanners.OrderBy(x => x.Id).ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.ProductFromPlanners.Any(x => x.Id == productId);
        }
    }
}
