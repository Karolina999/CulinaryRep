using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;

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

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User? GetUserByEmail(string email)
        {
            //return _context.Users.AsNoTrackingWithIdentityResolution().FirstOrDefault(x => x.Email == email);
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public ICollection<UserComment> GetUserComments(int userId)
        {
            return _context.UserComments.Where(x => x.User.Id == userId).ToList();
        }

        public ICollection<Planner> GetUserPlanners(int userId)
        {
            return _context.Planners.Where(x => x.User.Id == userId).ToList();
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

        public ICollection<Recipe> GetUserWatchedRecipes(int userId)
        {
            var watchedRecipes = _context.WatchedRecipes.Where(x => x.UserId == userId).ToList();
            var recipes = new List<Recipe>();
            foreach (var watchedRecipe in watchedRecipes)
            {
                recipes.Add(_context.Recipes.Where(x => x.Id == watchedRecipe.RecipeId).FirstOrDefault());
            }

            return recipes;
        }

        public ICollection<WatchedRecipe> GetUserWatched(int userId)
        {
            return _context.WatchedRecipes.Where(x => x.UserId == userId).ToList();
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

        public User? LoginUser(string login, string password)
        {
            var result = _context.Users.FirstOrDefault(x => x.Email == login);
            if (result is null)
                return result;
            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, result.Password);
            if (isValidPassword)
            {
                return result;
            } 
            return null;
        }
    }
}
