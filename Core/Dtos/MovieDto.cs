namespace MovieManagementApi.Core.Dtos;

public class MovieDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<RatingDto> Ratings { get; set; } = new List<RatingDto>();
    public List<ActorDto> Actors { get; set; } = new List<ActorDto>();
}