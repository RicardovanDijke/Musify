using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Song_Service.Database
{
    public interface IAlbumRepository : IRepository<Album>
    {
        public Album FindByName(string name);
        public Album GetBySong(Song song);
    }

    public class AlbumRepository : IAlbumRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Album> _table;

        public AlbumRepository(DatabaseContext context)
        {
            this._context = context;
            _table = context.Set<Album>();
        }

        public IEnumerable<Album> GetAll()
        {
            return _table.ToList();
        }

        public Album Get(long id)
        {
            return _table.Find(id);
        }

        public void Add(Album obj)
        {
            _table.Add(obj);
            Save();
        }

        public void Update(Album obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(Album obj)
        {
            var existing = _table.Find(obj);
            _table.Remove(existing);
            Save();
        }

        public Album FindByName(string name)
        {
            return _table.FirstOrDefault(a => a.Name == name);
        }

        public Album GetBySong(Song song)
        {
            return _table.FirstOrDefault(a => a.Songs.Contains(song));
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
