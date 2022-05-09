using System.ComponentModel.DataAnnotations;
using VideoGameAPI.Interfaces;

namespace VideoGameAPI.DTO
{
    public class VideogameDTO: IVideogame
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Studio { get; set; }
    }
}
