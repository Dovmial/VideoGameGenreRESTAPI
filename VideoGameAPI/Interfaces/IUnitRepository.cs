namespace VideoGameAPI.Interfaces
{
    public interface IUnitRepository
    {
        IGenreRepository Genre { get; }
        IVideogameRepository Videogame { get; }
        Task<bool> SaveAsync();
    }
}
