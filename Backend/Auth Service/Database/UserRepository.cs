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
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Get(long id)
        {
            return _context.Users.Find(id);
        }

        public void Add(User obj)
        {
            _context.Users.Add(obj);
            Save();
        }

        public void Update(User obj)
        {
            _context.Users.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(User obj)
        {
            User existing = _context.Users.Find(obj);
            _context.Users.Remove(existing);
            Save();
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users
                .Include(x=>x.Following)
                .Include(x=>x.Followers)
                .SingleOrDefault(x => x.UserName == username && x.Password == password);

            return user;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
