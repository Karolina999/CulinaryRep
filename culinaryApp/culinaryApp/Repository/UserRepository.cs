using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CulinaryDbContext _context;

        public UserRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Recipe> GetUserRecipe(int userId)
        {
            return _context.Recipes.Where(x => x.Owner.Id == userId).ToList();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(x => x.Id).ToList();
        }

        public ICollection<ShoppingList> GetUserShoppingList(int userId)
        {
            return _context.ShoppingLists.Where(x => x.User.Id == userId).ToList();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(x => x.Id == userId);
        }
    }
}
