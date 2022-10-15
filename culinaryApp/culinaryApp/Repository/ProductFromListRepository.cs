using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class ProductFromListRepository : IProductFromListRepository
    {
        private readonly CulinaryDbContext _context;

        public ProductFromListRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public bool CreateProductFromList(ProductFromList productFromList)
        {
            _context.Add(productFromList);
            return Save();
        }

        public ProductFromList GetProduct(int id)
        {
            return _context.ProductFromLists.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<ProductFromList> GetProducts()
        {
            return _context.ProductFromLists.OrderBy(x => x.Id).ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.ProductFromLists.Any(x => x.Id == productId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
