using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Song_Service.Database
{
    public interface IAlbumRepository : IRepository<Album>
    {
        public Album FindByName(string name);
    }

    public class AlbumRepository : IAlbumRepository
    {
        private DatabaseContext context;
        private DbSet<Album> table;

        public AlbumRepository(DatabaseContext context)
        {
            this.context = context;
            table = context.Set<Album>();
        }

        public IEnumerable<Album> GetAll()
        {
            return table.ToList();
        }

        public Album Get(long id)
        {
            return table.Find(id);
        }

        public void Add(Album obj)
        {
            table.Add(obj);
            Save();
        }

        public void Update(Album obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(Album obj)
        {
            var existing = table.Find(obj);
            table.Remove(existing);
            Save();
        }

        public Album FindByName(string name)
        {
            return table.FirstOrDefault(a => a.Name == name);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
