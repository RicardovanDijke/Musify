using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Model;

namespace Auth_Service
{
    public class UserManager : IUserRepository
    {
        readonly UserContext context;

        public UserManager(UserContext context)
        {
            this.context = context;
        }

        public void Add(User entity)
        {
            context.Users.Add(entity);
            context.SaveChanges();
        }

        public void Delete(User entity)
        {
            context.Users.Remove(entity);
            context.SaveChanges();

        }

        public User Login(string username, string password)
        {
            return context.Users.FirstOrDefault(user => user.UserName == username && user.Password == password);
        }

        public User Get(long id)
        {
            return context.Users.First(user => user.ID == id);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
