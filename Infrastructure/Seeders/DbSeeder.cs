using MovieManagementApi.Core.Entities;

namespace MovieManagementApi.Infrastructure.Seeders
{
    public static class SeedData
    {
        public static List<Movie> GetMovies(List<Actor> actors)
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Inception", Description = "Sci-Fi Thriller", ReleaseDate = new DateTime(2010, 7, 16) },
                new Movie { Id = 2, Title = "Interstellar", Description = "Space Adventure", ReleaseDate = new DateTime(2014, 11, 7) },
                new Movie { Id = 3, Title = "The Dark Knight", Description = "Action Thriller", ReleaseDate = new DateTime(2008, 7, 18) },
                new Movie { Id = 4, Title = "The Matrix", Description = "Cyberpunk Sci-Fi", ReleaseDate = new DateTime(1999, 3, 31) },
                new Movie { Id = 5, Title = "Fight Club", Description = "Psychological Drama", ReleaseDate = new DateTime(1999, 10, 15) },
                new Movie { Id = 6, Title = "Pulp Fiction", Description = "Crime Drama", ReleaseDate = new DateTime(1994, 10, 14) },
                new Movie { Id = 7, Title = "Gladiator", Description = "Historical Drama", ReleaseDate = new DateTime(2000, 5, 5) },
                new Movie { Id = 8, Title = "Titanic", Description = "Romance/Drama", ReleaseDate = new DateTime(1997, 12, 19) },
                new Movie { Id = 9, Title = "The Godfather", Description = "Mafia Drama", ReleaseDate = new DateTime(1972, 3, 24) },
                new Movie { Id = 10, Title = "Avengers: Endgame", Description = "Superhero Action", ReleaseDate = new DateTime(2019, 4, 26) }
            };

            // Manually assign actors to movies
            /*movies[0].Actors.Add(actors[0]); // Inception - Leonardo DiCaprio
            movies[1].Actors.Add(actors[0]); // Interstellar - Leonardo DiCaprio
            movies[2].Actors.Add(actors[1]); // The Dark Knight - Christian Bale
            movies[3].Actors.Add(actors[2]); // The Matrix - Keanu Reeves
            movies[4].Actors.Add(actors[3]); // Fight Club - Brad Pitt
            movies[5].Actors.Add(actors[3]); // Pulp Fiction - Brad Pitt
            movies[6].Actors.Add(actors[5]); // Gladiator - Russell Crowe
            movies[7].Actors.Add(actors[6]); // Titanic - Kate Winslet
            movies[8].Actors.Add(actors[7]); // The Godfather - Al Pacino
            movies[9].Actors.Add(actors[8]); // Avengers: Endgame - Robert Downey Jr.

            // Adding more actors to movies (example)
            movies[0].Actors.Add(actors[1]); // Inception - Christian Bale
            movies[4].Actors.Add(actors[9]); // Fight Club - Chris Evans
            movies[6].Actors.Add(actors[2]); // Gladiator - Keanu Reeves*/

            return movies;
        }

        public static List<Actor> GetActors()
        {
            return new List<Actor>
            {
                new Actor { Id = 1, Name = "Leonardo DiCaprio", DateOfBirth = new DateTime(1974, 11, 11) },
                new Actor { Id = 2, Name = "Christian Bale", DateOfBirth = new DateTime(1974, 1, 30) },
                new Actor { Id = 3, Name = "Keanu Reeves", DateOfBirth = new DateTime(1964, 9, 2) },
                new Actor { Id = 4, Name = "Brad Pitt", DateOfBirth = new DateTime(1963, 12, 18) },
                new Actor { Id = 5, Name = "Samuel L. Jackson", DateOfBirth = new DateTime(1948, 12, 21) },
                new Actor { Id = 6, Name = "Russell Crowe", DateOfBirth = new DateTime(1964, 4, 7) },
                new Actor { Id = 7, Name = "Kate Winslet", DateOfBirth = new DateTime(1975, 10, 5) },
                new Actor { Id = 8, Name = "Al Pacino", DateOfBirth = new DateTime(1940, 4, 25) },
                new Actor { Id = 9, Name = "Robert Downey Jr.", DateOfBirth = new DateTime(1965, 4, 4) },
                new Actor { Id = 10, Name = "Chris Evans", DateOfBirth = new DateTime(1981, 6, 13) }
            };
        }

        public static List<MovieRating> GetRatings()
        {
            return new List<MovieRating>
            {
                new MovieRating { Id = 1, MovieId = 1, Score = 9, Comment = "Amazing!" },
                new MovieRating { Id = 2, MovieId = 2, Score = 10, Comment = "Masterpiece!" },
                new MovieRating { Id = 3, MovieId = 3, Score = 9, Comment = "Great action!" },
                new MovieRating { Id = 4, MovieId = 4, Score = 8, Comment = "Mind-bending!" },
                new MovieRating { Id = 5, MovieId = 5, Score = 8, Comment = "Unique storytelling!" },
                new MovieRating { Id = 6, MovieId = 6, Score = 9, Comment = "Iconic!" },
                new MovieRating { Id = 7, MovieId = 7, Score = 10, Comment = "Epic!" },
                new MovieRating { Id = 8, MovieId = 8, Score = 7, Comment = "Classic love story!" },
                new MovieRating { Id = 9, MovieId = 9, Score = 10, Comment = "Legendary!" },
                new MovieRating { Id = 10, MovieId = 10, Score = 9, Comment = "Fantastic ending!" }
            };
        }
    }
}
