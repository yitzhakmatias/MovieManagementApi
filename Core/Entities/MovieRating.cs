namespace MovieManagementApi.Core.Entities
{
    public class MovieRating
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public double Rating { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }

}
