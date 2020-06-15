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

        public void Add(Song obj)
        {
            _context.Songs.Add(obj);
            Save();
        }

        public void Update(Song obj)
        {
            _context.Songs.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(Song obj)
        {
            Song existing = _context.Songs.Find(obj);
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
