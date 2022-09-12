using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieCollection.Models
{
    public class Director
    {
        public Director()
        {

        }

        public Director(string name)
        {
            Name = name;
        }
        [Key]
        public string Name { get; set; }
    }
}
