using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Playlist_Service.Database
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        public Playlist FindByName(string name);
    }

    public class PlaylistRepository : IPlaylistRepository
    {
        private DatabaseContext context;
        private DbSet<Playlist> table;

        public PlaylistRepository(DatabaseContext context)
        {
            this.context = context;
            table = context.Set<Playlist>();
        }

        public IEnumerable<Playlist> GetAll()
        {
            return table.ToList();
        }

        public Playlist Get(long id)
        {
            return table.Find(id);
        }

        public void Add(Playlist obj)
        {
            table.Add(obj);
            Save();
        }

        public void Update(Playlist obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(Playlist obj)
        {
            var existing = table.Find(obj);
            table.Remove(existing);
            Save();
        }

        public Playlist FindByName(string name)
        {
            return table.FirstOrDefault(a => a.Name == name);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
