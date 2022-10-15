using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        ICollection<Recipe> GetUserRecipe(int userId);
        ICollection<ShoppingList> GetUserShoppingList(int userId);
        bool UserExists(int userId);
    }
}
