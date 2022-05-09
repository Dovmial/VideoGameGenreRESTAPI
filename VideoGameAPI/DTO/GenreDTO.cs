using System.ComponentModel.DataAnnotations;

namespace VideoGameAPI.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }

        private string? _name;
        [MaxLength(50)]
        [Required]
        public string? Name
        {
            get { return _name; }
            set { _name = value?.Trim().ToLower(); }
        }
    }
}
