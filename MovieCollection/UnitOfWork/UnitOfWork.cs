using MovieCollection.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCollection.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext context;

        public UnitOfWork(MyContext context)
        {
            this.context = context;
            MovieManager = new MovieManager(context);
            MovieCollectionManager = new MovieCollectionManager(context);
            DirectorManager = new DirectorManager(context);
        }

        public IMovieManager MovieManager { get; }
        public IMovieCollectionManager MovieCollectionManager { get; }
        public IDirectorManager DirectorManager { get; }

        public void Dispose()
        {
            context.Dispose();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
