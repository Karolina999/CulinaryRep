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

        public bool ShoppingListExists(int shoppingListId)
        {
            return _context.ShoppingLists.Any(x => x.Id == shoppingListId);
        }
    }
}
