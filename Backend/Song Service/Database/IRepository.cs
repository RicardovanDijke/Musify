﻿using System.Collections.Generic;

namespace Song_Service.Database
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
