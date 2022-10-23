using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly CulinaryDbContext _context;

        public ShoppingListRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public bool CreateShoppingList(ShoppingList shoppingList)
        {
            _context.Add(shoppingList);
            return Save();
        }

        public bool DeleteShoppingList(ShoppingList shoppingList)
        {
            _context.Remove(shoppingList);
            return Save();
        }

        public bool DeleteShoppingLists(ICollection<ShoppingList> shoppingLists)
        {
            _context.RemoveRange(shoppingLists);
            return Save();
        }

        public ICollection<ProductFromList> GetProductsFromList(int shoppingListId)
        {
            return _context.ProductFromLists.Where(x => x.ShoppingList.Id == shoppingListId).ToList();
        }

        public ICollection<ProductFromList> GetProductsFromLists(ICollection<ShoppingList> shoppingLists)
        {
            var products = new List<ProductFromList>();
            foreach (ShoppingList shoppingList in shoppingLists)
            {
                products.AddRange(_context.ProductFromLists.Where(x => x.ShoppingList.Id == shoppingList.Id).ToList());
            }
            return products;
        }

        public ShoppingList GetShoppingList(int id)
        {
            return _context.ShoppingLists.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<ShoppingList> GetShoppingLists()
        {
            return _context.ShoppingLists.OrderBy(x => x.Id).ToList();
        }

        public ICollection<ShoppingList> GetShoppingLists(string title)
        {
            return _context.ShoppingLists.OrderBy(x => x.Id).Where(x => x.Title == title).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ShoppingListExists(int shoppingListId)
        {
            return _context.ShoppingLists.Any(x => x.Id == shoppingListId);
        }

        public bool UpdateShoppingList(ShoppingList shoppingList)
        {
            _context.Update(shoppingList);
            return Save();
        }
    }
}
