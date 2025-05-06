using System.Text.Json.Serialization;

namespace MovieActorLookup.Models.TvShow.TvShow
{
    public class ActorTvFilmography
    {
        [JsonPropertyName("cast")]
        public List<TvRole> Cast { get; set; }
    }

    public class TvRole
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string ShowTitle { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }
    }
}
