using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Song_Service.Entities;

namespace Song_Service.Database
{
    public interface ISongRepository : IRepository<Song>
    {
        public void AddRange(ICollection<Song> songs);
    }

    public class SongRepository : ISongRepository
    {
        private readonly DatabaseContext _context;

        public SongRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Song> GetAll()
        {
            return _context.Songs.ToList();
        }

        public Song Get(long id)
        {
            return _context.Songs.Find(id);
        }

        public void Add(Song entity)
        {
            _context.Songs.Add(entity);
            Save();
        }

        public void Update(Song entity)
        {
            _context.Songs.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Delete(Song entity)
        {
            Song existing = _context.Songs.Find(entity);
            _context.Songs.Remove(existing);
            Save();
        }

        public void AddRange(ICollection<Song> songs)
        {
            _context.Songs.AddRange(songs);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
