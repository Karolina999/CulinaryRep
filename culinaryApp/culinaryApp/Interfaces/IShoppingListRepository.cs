using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IShoppingListRepository
    {
        ICollection<ShoppingList> GetShoppingLists();
        ShoppingList GetShoppingList(int id);
        ICollection<ShoppingList> GetShoppingLists(string title);
        bool ShoppingListExists(int shoppingListId);
    }
}
