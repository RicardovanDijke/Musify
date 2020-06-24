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

        public void Add(Artist entity)
        {
            _table.Add(entity);
            Save();
        }

        public void Update(Artist entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Delete(Artist entity)
        {
            var existing = _table.Find(entity);
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
