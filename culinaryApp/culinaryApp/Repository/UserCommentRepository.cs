using culinaryApp.Data;
using culinaryApp.Interfaces;
using culinaryApp.Models;

namespace culinaryApp.Repository
{
    public class UserCommentRepository : IUserCommentRepository
    {
        private readonly CulinaryDbContext _context;

        public UserCommentRepository(CulinaryDbContext context)
        {
            _context = context;
        }

        public bool CreateUserComment(UserComment userComment)
        {
            _context.Add(userComment);
            return Save();
        }

        public bool DeleteUserComment(UserComment userComment)
        {
            _context.Remove(userComment);
            return Save();
        }

        public bool DeleteUserComments(ICollection<UserComment> userComments)
        {
            _context.RemoveRange(userComments);
            return Save();
        }

        public UserComment GetUserComment(int id)
        {
            return _context.UserComments.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<UserComment> GetUserComments()
        {
            return _context.UserComments.OrderBy(x => x.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUserComment(UserComment userComment)
        {
            _context.Update(userComment);
            return Save();
        }

        public bool UserCommentExists(int userCommentId)
        {
            return _context.UserComments.Any(x => x.Id == userCommentId);
        }

        public bool UserCommentRecipeExists(int userId, int recipeId)
        {
            return _context.UserComments
                .Where(x => x.Recipe.Id == recipeId)
                .Any(x => x.User.Id == userId);
        }
    }
}
