using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    public class Repository<T> : IRepository<T> where T : class
    {
        private DatabaseContext context;
        private DbSet<T> table;

        public Repository(DatabaseContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T Get(long id)
        {
            return table.Find(id);
        }

        public void Add(T obj)
        {
            table.Add(obj);
            Save();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Save();
        }

        public void Delete(T obj)
        {
            T existing = table.Find(obj);
            table.Remove(existing);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
