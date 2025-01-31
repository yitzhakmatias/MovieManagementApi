using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Interfaces;

namespace MovieManagementApi.Core.Services
{
    public class MovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // Get all movies with actors and ratings
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }

        // Get a movie by ID with actors and ratings
        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _movieRepository.GetMovieByIdAsync(id);
        }

        // Add a new movie
        public async Task AddMovieAsync(Movie? movie)
        {
            await _movieRepository.AddMovieAsync(movie);
        }

        // Update an existing movie
        public async Task UpdateMovieAsync(Movie? movie)
        {
            await _movieRepository.UpdateMovieAsync(movie);
        }

        // Delete a movie by ID
        public async Task DeleteMovieAsync(int id)
        {
            await _movieRepository.DeleteMovieAsync(id);
        }
    }
}