using culinaryApp.Models;

namespace culinaryApp.Authentication
{
    public interface IAuth
    {
        string Authentication(User user);
    }
}
