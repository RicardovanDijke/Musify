using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Song_Service.Database
{
    public interface IArtistRepository : IRepository<Artist>
    {
        public Artist FindByName(string name);
    }

    public class ArtistRepository : IArtistRepository
    {
        private DatabaseContext context;
        private DbSet<Artist> table;

        public ArtistRepository(DatabaseContext context)
        {
            this.context = context;
            table = context.Set<Artist>();
        }

        public IEnumerable<Artist> GetAll()
        {
            return table.ToList();
        }

        public Artist Get(long id)
        {
            return table.Find(id);
        }

        public void Add(Artist obj)
        {
            table.Add(obj);
            Save();
        }

        public void Update(Artist obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(Artist obj)
        {
            var existing = table.Find(obj);
            table.Remove(existing);
            Save();
        }

        public Artist FindByName(string name)
        {
            return table.FirstOrDefault(a => a.Name == name);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
