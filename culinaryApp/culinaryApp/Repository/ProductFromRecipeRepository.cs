using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class ProductFromRecipeRepository : IProductFromRecipeRepository
    {
        private readonly CulinaryDbContext _context;

        public ProductFromRecipeRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public ProductFromRecipe GetProduct(int id)
        {
            return _context.ProductFromRecipes.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<ProductFromRecipe> GetProducts()
        {
            return _context.ProductFromRecipes.OrderBy(x => x.Id).ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.ProductFromRecipes.Any(x => x.Id == productId);
        }
    }
}
