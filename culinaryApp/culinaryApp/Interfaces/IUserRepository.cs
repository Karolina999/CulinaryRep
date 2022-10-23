using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUserByEmail(string email);
        ICollection<Recipe> GetUserRecipes(int userId);
        ICollection<Planner> GetUserPlanners(int userId);
        ICollection<ShoppingList> GetUserShoppingList(int userId);
        ICollection<UserComment> GetUserComments(int userId);
        bool UserExists(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
