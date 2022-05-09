using AutoMapper;
using VideoGameAPI.DTO;
using VideoGameAPI.Models;

namespace VideoGameAPI.Helper
{
    public class MappingProfiler: Profile
    {
        public MappingProfiler()
        {
            CreateMap<Videogame, VideogameDTO>().ReverseMap();
            CreateMap<Genre, GenreDTO>().ReverseMap();
        }
    }
}
