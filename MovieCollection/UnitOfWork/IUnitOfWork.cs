using MovieCollection.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCollection.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IMovieManager MovieManager { get; }
        public IMovieCollectionManager MovieCollectionManager { get; }
        public IDirectorManager DirectorManager { get; }
        public void SaveChanges();
        public void Dispose();
    }
}
