using Microsoft.AspNetCore.Mvc;
using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Services;

namespace MovieManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        // Get all movies
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        // Get a movie by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        // Add a new movie
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest("Movie is null");
            }

            await _movieService.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // Update an existing movie
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest("Movie ID mismatch");
            }

            var existingMovie = await _movieService.GetMovieByIdAsync(id);
            if (existingMovie == null)
            {
                return NotFound();
            }

            await _movieService.UpdateMovieAsync(movie);
            return NoContent(); // 204 No Content
        }

        // Delete a movie by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            await _movieService.DeleteMovieAsync(id);
            return NoContent(); // 204 No Content
        }
    }
}
