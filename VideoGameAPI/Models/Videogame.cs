using VideoGameAPI.Interfaces;

namespace VideoGameAPI.Models
{
    public class Videogame: IVideogame
    {
        public int Id { get;set; }
        public string? Name { get;set; }
        public string? Studio { get; set; }
        public ICollection<VideogamesGenres> VideogamesGenres { get; set; } = new List<VideogamesGenres>();
    }
}
