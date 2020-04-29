using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth_Service.Database;
using Auth_Service.Helpers;
using Core.Model;

namespace Auth_Service.Service
{
    public interface ILoginService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }

    public class LoginService : ILoginService
    {
        private IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            var user = _userRepository.Authenticate(username, password);

            // return null if user not found

            // authentication successful so return user details without password
            return user?.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll().WithoutPasswords();
        }
    }
}
