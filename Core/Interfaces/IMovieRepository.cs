using MovieManagementApi.Core.Entities;

namespace MovieManagementApi.Core.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie?>> GetAllMoviesAsync();
        Task<Movie?> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie? movie);
        Task UpdateMovieAsync(Movie? movie);
        Task DeleteMovieAsync(int id);
    }
}
