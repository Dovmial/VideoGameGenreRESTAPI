using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameAPI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<VideogamesGenres> VideogamesGenres { get; set; } = new List<VideogamesGenres>();
        
    }
}
