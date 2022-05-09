using VideoGameAPI.Models;

namespace VideoGameAPI.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetAll();
        Genre GetById(int id);
        Task CreateAsync(Genre genre);
        void Update(Genre genre);
        void Delete(Genre genre);
        bool IsExists(int id);
    }
}
