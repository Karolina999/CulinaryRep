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

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<UserComment> GetUserComments(int userId)
        {
            return _context.UserComments.Where(x => x.User.Id == userId).ToList();
        }

        public ICollection<Recipe> GetUserRecipes(int userId)
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(x => x.Id == userId);
        }
    }
}
