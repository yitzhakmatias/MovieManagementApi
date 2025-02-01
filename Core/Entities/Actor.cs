using System.Text.Json.Serialization;

namespace MovieManagementApi.Core.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Avoid circular reference during JSON serialization
        [JsonIgnore]
        public List<Movie> Movies { get; set; } = new List<Movie>();
    }

}
