using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCollection.ContextManager
{
    public interface IContextManager<T> where T : class
    {
        void Add(T entity);
        void Remove(int Id);
        void RemoveByName(string name);
        void Update(T entity);
        int Count();
        T Find(int Id);
        T FindByName(string name);
        T GetInfo(T entity);
        IEnumerable<T> GetAll();
    }
}
