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

        public bool CreateProductFromPlanner(ProductFromPlanner productFromPlanner)
        {
            _context.Add(productFromPlanner);
            return Save();
        }

        public bool DeleteProductFromPlanner(ProductFromPlanner productFromPlanner)
        {
            _context.Remove(productFromPlanner);
            return Save();
        }

        public bool DeleteProductsFromPlanner(ICollection<ProductFromPlanner> productsFromPlanner)
        {
            _context.RemoveRange(productsFromPlanner);
            return Save();
        }

        public Ingredient GetIngredientFromProduct(int ingrednientId)
        {
            return _context.Ingredients.Where(x => x.Id == ingrednientId).FirstOrDefault();
        }

        public ICollection<ProductFromPlanner> GetPlannerProducts(int plannerId)
        {
            return _context.ProductFromPlanners.Where(x => x.Planner.Id == plannerId).ToList();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProductFromPlanner(ProductFromPlanner productFromPlanner)
        {
            _context.Update(productFromPlanner);
            return Save();
        }
    }
}
