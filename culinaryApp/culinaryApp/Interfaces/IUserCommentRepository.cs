using culinaryApp.Models;

namespace culinaryApp.Interfaces
{
    public interface IUserCommentRepository
    {
        ICollection<UserComment> GetUserComments();
        UserComment GetUserComment(int id);
        bool UserCommentExists(int userCommentId);
        bool UserCommentRecipeExists(int userId, int recipeId);
        bool CreateUserComment(UserComment userComment);
        bool UpdateUserComment(UserComment userComment);
        bool DeleteUserComment(UserComment userComment);
        bool DeleteUserComments(ICollection<UserComment> userComments);
        bool Save();
    }
}
