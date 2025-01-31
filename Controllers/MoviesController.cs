using Microsoft.AspNetCore.Mvc;
using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Services;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [SwaggerOperation(Summary = "Get all movies", Description = "Retrieve a list of all movies including actors and ratings.")]
        [SwaggerResponse(200, "List of all movies", typeof(List<Movie>))]
        [SwaggerResponse(404, "No movies found")]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return movies.Count == 0 ? NotFound() : Ok(movies);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a movie by ID", Description = "Retrieve a single movie by its unique ID.")]
        [SwaggerResponse(200, "Movie details", typeof(Movie))]
        [SwaggerResponse(404, "Movie not found")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add a new movie", Description = "Create a new movie with details including actors and ratings.")]
        [SwaggerResponse(201, "Movie created", typeof(Movie))]
        [SwaggerResponse(400, "Invalid movie data")]
        public async Task<IActionResult> CreateMovie([FromBody] Movie movie)
        {
            if (movie == null) return BadRequest("Movie is null");

            await _movieService.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a movie", Description = "Update the details of an existing movie.")]
        [SwaggerResponse(204, "Movie updated")]
        [SwaggerResponse(400, "Invalid data")]
        [SwaggerResponse(404, "Movie not found")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (id != movie.Id) return BadRequest("Movie ID mismatch");

            var existingMovie = await _movieService.GetMovieByIdAsync(id);
            if (existingMovie == null) return NotFound();

            await _movieService.UpdateMovieAsync(movie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a movie", Description = "Remove a movie from the database by ID.")]
        [SwaggerResponse(204, "Movie deleted")]
        [SwaggerResponse(404, "Movie not found")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound();

            await _movieService.DeleteMovieAsync(id);
            return NoContent();
        }
    }
}
