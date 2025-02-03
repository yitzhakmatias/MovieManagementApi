using Microsoft.EntityFrameworkCore;
using MovieManagementApi.Core.Entities;
using MovieManagementApi.Infrastructure.Seeders;

namespace MovieManagementApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Movie?> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Many-to-Many relationship between Movies and Actors
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity(j => j.ToTable("ActorMovie"));  // EF Core will create a join table named "MovieActor"

            // Configure One-to-Many relationship between Movie and MovieRatings
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Ratings)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId);

            modelBuilder.Entity<MovieRating>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.MovieId);

            // Seed data for Movies, Actors, and MovieRatings
            var actors = SeedData.GetActors();
            var movies = SeedData.GetMovies(actors); // Pass actors to seed movies
            var ratings = SeedData.GetRatings();

            modelBuilder.Entity<Movie>().HasData(movies);
            modelBuilder.Entity<Actor>().HasData(actors);
            modelBuilder.Entity<MovieRating>().HasData(ratings);
        }
    }

}
