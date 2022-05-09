using VideoGameAPI.Interfaces;
using VideoGameAPI.Models;
using VideoGameAPI.Data;
using VideoGameAPI.ViewModels;
using VideoGameAPI.Helper;

namespace VideoGameAPI.Repositories
{
    public class VideogameRepository : IVideogameRepository
    {
        private readonly VideoGameDBContextAPI _dbContext;

        public VideogameRepository(VideoGameDBContextAPI dbContext)
        {
            _dbContext = dbContext;
        }

        //create many-to-many
        public async Task CreateAsync(Videogame videogame, IEnumerable<Genre> genres)
        {
            List<VideogamesGenres> videogameGenres = new List<VideogamesGenres>();

            await _dbContext.AddAsync(videogame);

            foreach (var genre in genres)
            {
                /*
                var genreExists = _dbContext.Genres.FirstOrDefault(g => g.Name == genre.Name);
                if (genreExists is not null)
                    genre.Id = genreExists.Id;*/
                videogameGenres.Add(new VideogamesGenres
                {
                    Videogame = videogame,
                    Genre = genre
                });
            }

            await _dbContext.AddRangeAsync(videogameGenres);
        }

        //delete
        public void Delete(Videogame videogame)
        {
            _dbContext.Remove(videogame);
        }

        public ICollection<Videogame> GetAll()
        {
            return _dbContext.Videogames.OrderBy(v => v.Name).ToList();
        }

        public ICollection<Videogame> GetByGenre(Genre genre)
        {
            return _dbContext.VideogamesGenres
                .Where(vg => vg.Genre.Equals(genre))
                .Select(vg => vg.Videogame)
                .ToList();
        }

        public Videogame GetbyId(int id)
        {
            return _dbContext.Videogames.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Videogame videogame, IEnumerable<Genre> genres)
        {
            _dbContext.Update(videogame);
            if (genres is null)
                return;

            var genresByVideogame = GetGenresByVideogame(videogame);

            //delete genres from game
            var vgToDelete = genresByVideogame.Except(genres);
            foreach (var genre in vgToDelete)
            {
                var videogameGenre = _dbContext.VideogamesGenres.FirstOrDefault(x => x.Genre == genre);
                if (videogameGenre is not null)
                    _dbContext.VideogamesGenres.Remove(videogameGenre);
            }

            foreach (var genre in vgToDelete)
                genresByVideogame.Remove(genre);

            //new genres?
            foreach (var genre in genres)
            {
                if (_dbContext.Genres.Contains(genre, new GenreEqualComparer()) == false)
                    _dbContext.Genres.Add(genre);
            }

            //Add genres to game
            var genresToAdd = genres.Except(genresByVideogame);
            foreach (Genre genre in genresToAdd)
            {
                _dbContext.Add(
                    new VideogamesGenres
                    {
                        Videogame = videogame,
                        Genre = genre
                    });
            }
        }

        public bool IsExists(int id)
        {
            return _dbContext.Videogames.Any(v => v.Id == id);
        }

        public bool IsExists(IVideogame videogame)
        {
            return _dbContext.Videogames.Any(v => 
                v.Name   == videogame.Name &&
                v.Studio == videogame.Studio
                );
        }
        public ICollection<Genre> GetGenresByVideogame(Videogame videogame)
        {
            return _dbContext.VideogamesGenres
                .Where(vg => vg.Videogame == videogame)
                .Select(vg=>vg.Genre)
                .ToList();
        }
    }
}
