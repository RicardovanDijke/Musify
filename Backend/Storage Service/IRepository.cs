using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Song_Service
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    public interface ISongRepository : IRepository<Song>
    {
        public void AddRange(ICollection<Song> songs);
    }

    public class SongRepository : ISongRepository
    {
        private DatabaseContext context;
        private DbSet<Song> table;

        public SongRepository(DatabaseContext context)
        {
            this.context = context;
            table = context.Set<Song>();
        }

        public IEnumerable<Song> GetAll()
        {
            return context.Songs.ToList();
        }

        public Song Get(long id)
        {
            return table.Find(id);
        }

        public void Add(Song obj)
        {
            table.Add(obj);
            Save();
        }

        public void Update(Song obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(Song obj)
        {
            Song existing = table.Find(obj);
            table.Remove(existing);
            Save();
        }

        public void AddRange(ICollection<Song> songs)
        {
            table.AddRange(songs);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
