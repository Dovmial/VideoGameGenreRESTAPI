using VideoGameAPI.Models;
using VideoGameAPI.ViewModels;

namespace VideoGameAPI.Interfaces
{
    public interface IVideogameRepository
    {
        ICollection<Videogame> GetAll();
        Videogame GetbyId(int id);
        ICollection<Videogame> GetByGenre(Genre genre);
        Task CreateAsync(Videogame videogame, IEnumerable<Genre> genres);
        void Update(Videogame videogame, IEnumerable<Genre> genres);
        void Delete(Videogame videogame);
        bool IsExists(int id);
        bool IsExists(IVideogame videogame);
        ICollection<Genre> GetGenresByVideogame(Videogame videogame);
    }
}
