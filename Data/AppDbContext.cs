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

            // Optional: Fluent API for configuring relationships or constraints
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().HasMany(m => m.Actors).WithMany(a => a.Movies);
            modelBuilder.Entity<Movie>().HasMany(m => m.Ratings).WithOne(r => r.Movie);
            modelBuilder.Entity<MovieRating>().HasOne(r => r.Movie).WithMany(m => m.Ratings).HasForeignKey(r => r.MovieId);

            // Seed Data
            modelBuilder.Entity<Movie>().HasData(SeedData.GetMovies());
            modelBuilder.Entity<Actor>().HasData(SeedData.GetActors());
            modelBuilder.Entity<MovieRating>().HasData(SeedData.GetRatings());
        }
    }

}
