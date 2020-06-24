using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using User_Service.Entities;

namespace User_Service.Database
{
    public interface IUserRepository : IRepository<User>
    {
        public User Authenticate(string username, string password);
        List<User> GetFollowersByUser(long userId);
        List<User> GetFollowingByUser(long userId);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Get(long id)
        {
            return _context.Users.Find(id);
        }

        public User Add(User entity)
        {
            var u =_context.Users.Add(entity);
            Save();
            return u.Entity;
        }

        public void Update(User entity)
        {
            _context.Users.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Delete(User entity)
        {
            User existing = _context.Users.Find(entity.UserId);
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

        public List<User> GetFollowersByUser(long userId)
        {
            var followers = Get(userId).Followers;

            var usersFollowing = followers.Select(f => f.Follower).ToList();
            return usersFollowing;
        }

        public List<User> GetFollowingByUser(long userId)
        {
            var following = Get(userId).Following;

            var usersFollowing = following.Select(f => f.Followee).ToList();
            return usersFollowing;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
