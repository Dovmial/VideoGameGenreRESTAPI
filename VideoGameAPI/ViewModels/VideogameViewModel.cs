using System.Text.Json.Serialization;
using VideoGameAPI.DTO;
using VideoGameAPI.Interfaces;

namespace VideoGameAPI.ViewModels
{
    public class VideogameViewModel : IVideogame
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Studio { get; set; }

        public ICollection<GenreDTO> Genres { get; set; } = new List<GenreDTO>();

        [JsonConstructor]
        public VideogameViewModel()
        {}
        public VideogameViewModel(IVideogame videogame, ICollection<GenreDTO> genres)
        {
            Genres = genres;
            Id = videogame.Id;
            Name = videogame.Name;
            Studio = videogame.Studio;
        }

        public VideogameDTO GetVideogameDTO() => new VideogameDTO() { Id = Id, Name=Name, Studio = Studio};
    }
}
