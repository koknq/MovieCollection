using MovieCollection.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MovieCollection.ContextManager
{
    public class ContextManager<T> : IContextManager<T> where T : class
    {
        public MyContext Context { get; set; }
        public ContextManager(MyContext context)
        {
            this.Context = context;
        }
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public int Count()
        {
            return Context.Set<T>().Count();
        }
        public T Find(int Id)
        {
            return Context.Set<T>().Find(Id);
        }
        public T FindByName(string name)
        {
            return Context.Set<T>().Find(name);
        }
        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public T GetInfo(T entity)
        {
            return Context.Set<T>().Find(entity);
        }
        public void Remove(int Id)
        {
            Movie movie = Context.Movies.Where(x => x.ID == Id).FirstOrDefault();
            Context.Movies.Remove(movie);
        }
        public void RemoveByName(string name)
        {
            Director director = Context.Directors.Where(x => x.Name == name).FirstOrDefault();
            Context.Directors.Remove(director);
        }
        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
    }
}
