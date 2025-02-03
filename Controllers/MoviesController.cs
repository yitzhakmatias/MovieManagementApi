using Microsoft.AspNetCore.Mvc;
using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Services;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace MovieManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;
        private readonly ActorService _actorService;
        private const string API_SECRET_KEY = "YourHardCodedApiSecret";  // Hardcoded secret API key

        public MoviesController(MovieService movieService, ActorService actorService)
        {
            _movieService = movieService;
            _actorService = actorService;
        }

        // Get all movies
        [HttpGet]
        [SwaggerOperation(Summary = "Get all movies", Description = "Retrieve a list of all movies including actors and ratings.")]
        [SwaggerResponse(200, "List of all movies", typeof(List<Movie>))]
        [SwaggerResponse(404, "No movies found")]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return movies.Count == 0 ? NotFound("No movies found") : Ok(movies);
        }

        // Get a movie by ID
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a movie by ID", Description = "Retrieve a single movie by its unique ID.")]
        [SwaggerResponse(200, "Movie details", typeof(Movie))]
        [SwaggerResponse(404, "Movie not found")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound("Movie not found");
            return Ok(movie);
        }

        // Create a new movie
        [HttpPost]
        [SwaggerOperation(Summary = "Add a new movie", Description = "Create a new movie with details including actors and ratings.")]
        [SwaggerResponse(201, "Movie created", typeof(Movie))]
        [SwaggerResponse(400, "Invalid movie data")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> CreateMovie([FromBody] Movie movie, [FromHeader] string apiKey)
        {
            if (apiKey != API_SECRET_KEY) return Unauthorized("Invalid API Key");

            if (movie == null) return BadRequest("Movie is null");

            await _movieService.AddMovieAsync(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // Update a movie
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a movie", Description = "Update the details of an existing movie.")]
        [SwaggerResponse(204, "Movie updated")]
        [SwaggerResponse(400, "Invalid data")]
        [SwaggerResponse(404, "Movie not found")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie, [FromHeader] string apiKey)
        {
            if (apiKey != API_SECRET_KEY) return Unauthorized("Invalid API Key");

            if (id != movie.Id) return BadRequest("Movie ID mismatch");

            var existingMovie = await _movieService.GetMovieByIdAsync(id);
            if (existingMovie == null) return NotFound("Movie not found");

            await _movieService.UpdateMovieAsync(movie);
            return NoContent();
        }

        // Delete a movie
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a movie", Description = "Remove a movie from the database by ID.")]
        [SwaggerResponse(204, "Movie deleted")]
        [SwaggerResponse(404, "Movie not found")]
        [SwaggerResponse(401, "Unauthorized")]
        public async Task<IActionResult> DeleteMovie(int id, [FromHeader] string apiKey)
        {
            if (apiKey != API_SECRET_KEY) return Unauthorized("Invalid API Key");

            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound("Movie not found");

            await _movieService.DeleteMovieAsync(id);
            return NoContent();
        }

        // Search movies by name
        [HttpGet("search/movies")]
        [SwaggerOperation(Summary = "Search movies by name", Description = "Search for movies by title.")]
        [SwaggerResponse(200, "List of movies", typeof(List<Movie>))]
        [SwaggerResponse(400, "Search term cannot be empty")]
        public async Task<IActionResult> SearchMovies([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Search term cannot be empty");

            var movies = await _movieService.SearchMoviesByNameAsync(name);
            return Ok(movies);
        }

        // Search actors by name
        [HttpGet("search/actors")]
        [SwaggerOperation(Summary = "Search actors by name", Description = "Search for actors by name.")]
        [SwaggerResponse(200, "List of actors", typeof(List<Actor>))]
        [SwaggerResponse(400, "Search term cannot be empty")]
        public async Task<IActionResult> SearchActors([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Search term cannot be empty");

            var actors = await _actorService.SearchActorsByNameAsync(name);
            return Ok(actors);
        }

        // Get all movies an actor has been in
        [HttpGet("{actorId}/movies")]
        [SwaggerOperation(Summary = "Get all movies an actor has been in", Description = "Retrieve all movies an actor has appeared in.")]
        [SwaggerResponse(200, "List of movies", typeof(List<Movie>))]
        [SwaggerResponse(404, "Actor not found")]
        public async Task<IActionResult> GetMoviesByActor(int actorId)
        {
            var actor = await _actorService.GetActorByIdAsync(actorId);
            if (actor == null) return NotFound("Actor not found");

            return Ok(actor.Movies); // Movies the actor has appeared in
        }

        // Get all actors in a movie
        [HttpGet("{movieId}/actors")]
        [SwaggerOperation(Summary = "Get all actors in a movie", Description = "Retrieve all actors that appeared in a given movie.")]
        [SwaggerResponse(200, "List of actors", typeof(List<Actor>))]
        [SwaggerResponse(404, "Movie not found")]
        public async Task<IActionResult> GetActorsByMovie(int movieId)
        {
            var movie = await _movieService.GetMovieByIdAsync(movieId);
            if (movie == null) return NotFound("Movie not found");

            return Ok(movie.Actors); // Actors in the movie
        }
    }
}
