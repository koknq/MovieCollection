using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieCollection.Models
{
    public class Movie
    {
        public Movie()
        {

        }

        public Movie(string movieName, int year, Director director, string status, string genre, int collectionId)
        {
            MovieName = movieName;
            Year = year;
            Director = director;
            Status = status;
            Genre = genre;
            MovieCollectionId = collectionId;
        }
        [Key]
        public int ID { get; set; }
        public string MovieName { get; set; }
        public int Year { get; set; }
        public Director Director { get; set; }
        public string Genre { get; set; }
        public string Status { get; set; }
        public int MovieCollectionId { get; set; }
    }
}
