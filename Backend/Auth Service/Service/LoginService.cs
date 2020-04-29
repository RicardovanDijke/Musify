using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth_Service.Helpers;
using Core.Model;

namespace Auth_Service.Service
{
    public interface ILoginService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
    }

    public class LoginService : ILoginService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { UserID = 1, DisplayName = "Test",  UserName = "test", Password = "test" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.UserName == username && x.Password == password));

            // return null if user not found

            // authentication successful so return user details without password
            return user?.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}
