using Core;
using Core.Model;

namespace Auth_Service
{
    public interface IUserRepository : IRepository<User>
    {
        User Login(string username, string password);
    }
}
