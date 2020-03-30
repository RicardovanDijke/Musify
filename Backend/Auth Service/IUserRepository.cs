using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Model;

namespace Auth_Service
{
    public interface IUserRepository : IDataRepository<User>
    {
        User Login(string username, string password);
    }
}
