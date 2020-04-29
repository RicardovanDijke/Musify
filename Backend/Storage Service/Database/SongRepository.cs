using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Song_Service.Database
{
    public interface ISongRepository : IRepository<Song>
    {
        public void AddRange(ICollection<Song> songs);
    }

    public class SongRepository : ISongRepository
    {
        private DatabaseContext context;

        public SongRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public IEnumerable<Song> GetAll()
        {
            return context.Songs.ToList();
        }

        public Song Get(long id)
        {
            return context.Songs.Find(id);
        }

        public void Add(Song obj)
        {
            context.Songs.Add(obj);
            Save();
        }

        public void Update(Song obj)
        {
            context.Songs.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(Song obj)
        {
            Song existing = context.Songs.Find(obj);
            context.Songs.Remove(existing);
            Save();
        }

        public void AddRange(ICollection<Song> songs)
        {
            context.Songs.AddRange(songs);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
