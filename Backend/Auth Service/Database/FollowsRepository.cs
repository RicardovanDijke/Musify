using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using User_Service.Entities;

namespace User_Service.Database
{
    public interface IFollowsRepository : IRepository<UserFollow>
    {
        void RemoveByIds(long followeeId, long followerId);
    }

    public class FollowsRepository : IFollowsRepository
    {
        private readonly DatabaseContext _context;

        public FollowsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<UserFollow> GetAll()
        {
            return _context.UserFollows.ToList();
        }

        public UserFollow Get(long id)
        {
            return _context.UserFollows.Find(id);
        }

        public void Add(UserFollow obj)
        {
            _context.UserFollows.Add(obj);
            Save();
        }

        public void Update(UserFollow obj)
        {
            _context.UserFollows.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(UserFollow obj)
        {
            UserFollow existing = Get(obj.UserFollowId);
            _context.UserFollows.Remove(existing);
            Save();
        }

        public void RemoveByIds(long followeeId, long followerId)
        {
            var follows = GetAll();

            var userFollow = _context.UserFollows.FirstOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);
            if (userFollow != null)
            {
                Delete(userFollow);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
