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
}
