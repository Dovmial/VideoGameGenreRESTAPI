namespace VideoGameAPI.Models
{
    public class VideogamesGenres
    {
        public int VideogameId { get; set; }
        public int GenreId { get; set; }
        public Videogame Videogame { get; set; }
        public Genre Genre { get; set; }
    }
}
