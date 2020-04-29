using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Auth_Service.Database
{
    public interface IUserRepository : IRepository<User>
    {
        public User Authenticate(string username, string password);
    }

    public class UserRepository : IUserRepository
    {
        private DatabaseContext context;

        public UserRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User Get(long id)
        {
            return context.Users.Find(id);
        }

        public void Add(User obj)
        {
            context.Users.Add(obj);
            Save();
        }

        public void Update(User obj)
        {
            context.Users.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(User obj)
        {
            User existing = context.Users.Find(obj);
            context.Users.Remove(existing);
            Save();
        }

        public User Authenticate(string username, string password)
        {
            var user = context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);

            return user;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
