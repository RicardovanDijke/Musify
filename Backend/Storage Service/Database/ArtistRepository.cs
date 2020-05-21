using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Song_Service.Entities;

namespace Song_Service.Database
{
    public interface IArtistRepository : IRepository<Artist>
    {
        public Artist FindByName(string name);
    }

    public class ArtistRepository : IArtistRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Artist> _table;

        public ArtistRepository(DatabaseContext context)
        {
            _context = context;
            _table = context.Set<Artist>();
        }

        public IEnumerable<Artist> GetAll()
        {
            return _table.ToList();
        }

        public Artist Get(long id)
        {
            return _table.Find(id);
        }

        public void Add(Artist obj)
        {
            _table.Add(obj);
            Save();
        }

        public void Update(Artist obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(Artist obj)
        {
            var existing = _table.Find(obj);
            _table.Remove(existing);
            Save();
        }

        public Artist FindByName(string name)
        {
            return _table.FirstOrDefault(a => a.Name == name);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
