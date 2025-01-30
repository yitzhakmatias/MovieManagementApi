using Microsoft.EntityFrameworkCore;
using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Interfaces;
using MovieManagementApi.Data;

namespace MovieManagementApi.Infrastructure.Repositories;

public class MovieRatingRepository : IMovieRatingRepository
{
    private readonly AppDbContext _context;

    public MovieRatingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MovieRating>> GetAllRatingsAsync()
    {
        return await _context.MovieRatings
            .Include(mr => mr.Movie)   // Include related Movie
            .ToListAsync();
    }

    public async Task<MovieRating> GetRatingByIdAsync(int id)
    {
        return await _context.MovieRatings
            .Include(mr => mr.Movie)  // Include related Movie
            .FirstOrDefaultAsync(mr => mr.Id == id);
    }

    public async Task AddRatingAsync(MovieRating movieRating)
    {
        await _context.MovieRatings.AddAsync(movieRating);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRatingAsync(MovieRating movieRating)
    {
        _context.MovieRatings.Update(movieRating);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRatingAsync(int id)
    {
        var rating = await _context.MovieRatings.FindAsync(id);
        if (rating != null)
        {
            _context.MovieRatings.Remove(rating);
            await _context.SaveChangesAsync();
        }
    }
}