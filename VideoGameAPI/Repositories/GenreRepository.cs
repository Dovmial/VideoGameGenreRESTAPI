using VideoGameAPI.Interfaces;
using VideoGameAPI.Models;
using VideoGameAPI.Data;

namespace VideoGameAPI.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly VideoGameDBContextAPI _dbContext;

        public GenreRepository(VideoGameDBContextAPI dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Genre genre)
        {
            await _dbContext.AddAsync(genre);
        }

        public void Delete(Genre genre)
        {
            _dbContext.Remove(genre);
        }

        public ICollection<Genre> GetAll()
        {
            return _dbContext.Genres.ToList();
        }

        public Genre GetById(int id)
        {
            return _dbContext.Genres.FirstOrDefault(x => x.Id == id);
        }

        public bool IsExists(int id)
        {
            return _dbContext.Genres.Any(g => g.Id == id);
        }
        public void Update(Genre genre)
        {
            _dbContext.Update(genre);
        }
    }
}
