using MovieManagementApi.Core.Entities;

namespace MovieManagementApi.Core.Interfaces
{
    public interface IMovieRatingRepository
    {
        Task<List<MovieRating>> GetAllRatingsAsync();
        Task<MovieRating> GetRatingByIdAsync(int id);
        Task AddRatingAsync(MovieRating movieRating);
        Task UpdateRatingAsync(MovieRating movieRating);
        Task DeleteRatingAsync(int id);
    }
}
