using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Models;

namespace VideoGameAPI.Data
{
    public class VideoGameDBContextAPI: DbContext
    {
        public DbSet<Videogame>         Videogames       { get; set; } = null!;
        public DbSet<Genre>             Genres           { get; set; } = null!;
        public DbSet<VideogamesGenres>  VideogamesGenres { get; set; } = null!;

        public VideoGameDBContextAPI(DbContextOptions<VideoGameDBContextAPI> options): base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideogamesGenres>()
                .HasKey(vg => new { vg.VideogameId, vg.GenreId });

            modelBuilder.Entity<VideogamesGenres>()
                .HasOne(vg => vg.Videogame)
                .WithMany(vg => vg.VideogamesGenres)
                .HasForeignKey(vg => vg.VideogameId);

            modelBuilder.Entity<VideogamesGenres>()
                .HasOne(vg => vg.Genre)
                .WithMany(vg => vg.VideogamesGenres)
                .HasForeignKey(vg => vg.GenreId);
        }
    }
}
