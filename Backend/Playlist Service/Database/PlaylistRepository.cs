using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Playlist_Service.Entities;

namespace Playlist_Service.Database
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        public Playlist FindByName(string name);
        List<Playlist> GetFollowedPlaylistsByUserId(long id);
        List<Playlist> GetPublicCreatedPlaylistsByUserId(long id);
        List<Playlist> GetAllCreatedPlaylistsByUserId(long id);
        void AddRange(List<Playlist> playlists);
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
            var existing = table.Find(obj.PlaylistId);
            table.Remove(existing);
            Save();
        }

        public Playlist FindByName(string name)
        {
            return table.FirstOrDefault(a => a.Name == name);
        }

        public List<Playlist> GetFollowedPlaylistsByUserId(long id)
        {
            //todo add followed playlists instead of just created
            return table.Where(p => p.CreatorUserID == id).ToList();
        }

        public List<Playlist> GetPublicCreatedPlaylistsByUserId(long id)
        {
            return table.Where(p => p.CreatorUserID == id && p.Private == false).ToList();
        }

        public List<Playlist> GetAllCreatedPlaylistsByUserId(long id)
        {
            return table.Where(p => p.CreatorUserID == id).ToList();
        }

        public void AddRange(List<Playlist> playlists)
        {
            table.AddRange(playlists);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
