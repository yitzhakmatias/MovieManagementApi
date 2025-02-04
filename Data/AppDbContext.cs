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
                .UsingEntity<Dictionary<string, object>>(
                    "ActorMovie", // Name of the join table
                    j => j.HasOne<Actor>().WithMany().HasForeignKey("ActorId"), // Foreign key to Actor
                    j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId") // Foreign key to Movie
                ); // EF Core will create a join table named "MovieActor"

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
            
            
            modelBuilder.Entity("ActorMovie").HasData(
                new { ActorId = 1, MovieId = 1 }, // Leonardo DiCaprio in Inception
                new { ActorId = 1, MovieId = 2 }, // Leonardo DiCaprio in Interstellar
                new { ActorId = 2, MovieId = 2 }, // Christian Bale in Interstellar
                new { ActorId = 3, MovieId = 3 }, // Keanu Reeves in The Dark Knight
                new { ActorId = 4, MovieId = 4 }, // Brad Pitt in The Matrix
                new { ActorId = 5, MovieId = 5 }, // Samuel L. Jackson in Fight Club
                new { ActorId = 6, MovieId = 6 }, // Russell Crowe in Pulp Fiction
                new { ActorId = 7, MovieId = 7 }, // Kate Winslet in Gladiator
                new { ActorId = 8, MovieId = 8 }, // Al Pacino in Titanic
                new { ActorId = 9, MovieId = 9 }, // Robert Downey Jr. in The Godfather
                new { ActorId = 10, MovieId = 10 } // Chris Evans in Avengers: Endgame
            );
        }
    }

}
