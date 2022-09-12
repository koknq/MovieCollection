using MovieCollection.ContextManager;
using MovieCollection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCollection.Managers
{
    public interface IMovieCollectionManager : IContextManager<MoviesCollection>
    {
    }
}
