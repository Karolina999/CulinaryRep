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

        public UserComment GetUserComment(int id)
        {
            return _context.UserComments.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<UserComment> GetUserComments()
        {
            return _context.UserComments.OrderBy(x => x.Id).ToList();
        }

        public bool UserCommentExists(int userCommentId)
        {
            return _context.UserComments.Any(x => x.Id == userCommentId);
        }
    }
}
