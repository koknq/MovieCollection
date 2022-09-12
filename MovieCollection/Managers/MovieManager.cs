using MovieCollection.ContextManager;
using MovieCollection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCollection.Managers
{
    public class MovieManager : ContextManager<Movie>, IMovieManager
    {
        public MovieManager(MyContext context)
            : base(context)
        {

        }
    }
}
