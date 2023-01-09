using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IShoppingListRepository
    {
        ICollection<ShoppingList> GetShoppingLists();
        ShoppingList GetShoppingList(int id);
        ICollection<ShoppingList> GetShoppingLists(string title);
        ICollection<ProductFromList> GetProductsFromList(int shoppingListId);
        ICollection<ProductFromList> GetProductsFromLists(ICollection<ShoppingList> shoppingLists);
        bool ShoppingListExists(int shoppingListId);
        bool CreateShoppingList(ShoppingList shoppingList);
        bool UpdateShoppingList(ShoppingList shoppingList);
        bool DeleteShoppingList(ShoppingList shoppingList);
        bool DeleteShoppingLists(ICollection<ShoppingList> shoppingLists);
        bool Save();
    }
}
