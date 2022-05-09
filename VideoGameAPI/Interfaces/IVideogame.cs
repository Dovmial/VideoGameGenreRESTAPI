namespace VideoGameAPI.Interfaces
{
    public interface IVideogame
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Studio { get; set; }
    }
}
