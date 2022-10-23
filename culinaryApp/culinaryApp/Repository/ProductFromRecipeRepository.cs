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

        public bool CreateProductFromRecipe(ProductFromRecipe productFromRecipe)
        {
            _context.Add(productFromRecipe);
            return Save();
        }

        public bool DeleteProductFromRecipe(ProductFromRecipe productFromRecipe)
        {
            _context.Remove(productFromRecipe);
            return Save();
        }

        public bool DeleteProductsFromRecipe(ICollection<ProductFromRecipe> productsFromRecipe)
        {
            _context.RemoveRange(productsFromRecipe);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProductFromRecipe(ProductFromRecipe productFromRecipe)
        {
            _context.Update(productFromRecipe);
            return Save();
        }
    }
}
