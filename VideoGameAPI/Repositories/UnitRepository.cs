using VideoGameAPI.Interfaces;
using VideoGameAPI.Data;

namespace VideoGameAPI.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly VideoGameDBContextAPI  _dbContext;

        private IVideogameRepository   _videogameRepository;
        private IGenreRepository       _genreRepository;

         public UnitRepository(VideoGameDBContextAPI dbContext)
        {
            _dbContext           = dbContext;
            _videogameRepository = new VideogameRepository(dbContext);
            _genreRepository     = new GenreRepository(dbContext);
        }
        public IGenreRepository Genre         { get { return _genreRepository; } }
        public IVideogameRepository Videogame { get { return _videogameRepository; } }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return (await _dbContext.SaveChangesAsync()) > 0;
            }
            catch (Exception) { return false; }
        }
    }
}
