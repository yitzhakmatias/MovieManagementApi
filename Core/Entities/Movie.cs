namespace MovieManagementApi.Core.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public ICollection<MovieRating> Ratings { get; set; } = new List<MovieRating>();
        public DateTime ReleaseDate { get; set; }
    }

}
