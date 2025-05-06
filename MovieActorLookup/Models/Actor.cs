using System.Text.Json.Serialization;

namespace MovieActorLookup.Models
{
    public class Actor(string name)
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = name;

        [JsonPropertyName("movie_cast")]
        public List<MovieCharacter> MovieCharacters { get; set; } = new();

        [JsonPropertyName("tv_cast")]
        public List<TvCharacter> TvCharacters { get; set; } = new();
    }

    public class MovieCharacter
    {
        [JsonPropertyName("title")]
        public string MovieTitle { get; set; }

        [JsonPropertyName("character")]
        public string CharacterName { get; set; }

        [JsonPropertyName("poster_path")]
        public string MoviePosterPath { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        public string Franchise { get; set; }
    }

    public class TvCharacter
    {
        [JsonPropertyName("name")]
        public string ShowTitle { get; set; }

        [JsonPropertyName("character")]
        public string CharacterName { get; set; }

        [JsonPropertyName("poster_path")]
        public string ShowPosterPath { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }

        public string Franchise { get; set; }
    }
}
