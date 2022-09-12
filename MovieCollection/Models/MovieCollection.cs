using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCollection.Models
{
    public class MoviesCollection
    {
        public MoviesCollection()
        {

        }
        public int ID { get; set; }
        public int MoviesCount { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public int DirectorsCount { get; set; }
    }
}
