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

        public bool UserCommentExists(int userCommentId)
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
