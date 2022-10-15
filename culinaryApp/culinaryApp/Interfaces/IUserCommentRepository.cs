using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IUserCommentRepository
    {
        ICollection<UserComment> GetUserComments();
        UserComment GetUserComment(int id);
        bool UserCommentExists(int userCommentId);
        bool CreateUserComment(UserComment userComment);
        bool Save();
    }
}
