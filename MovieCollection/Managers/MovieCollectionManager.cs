using MovieCollection.ContextManager;
using MovieCollection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCollection.Managers
{
    public class MovieCollectionManager : ContextManager<MoviesCollection>, IMovieCollectionManager
    {
        public MovieCollectionManager(MyContext context)
            : base(context)
        {

        }
    }
}
