namespace MovieActorLookup.Models
{
    public class ActorViewModel
    {
        public string ActorName { get; set; }
        public string ActorProfilePath { get; set; }
        public List<MovieCharacterInfo> MovieCharacterInfos { get; set; } = new List<MovieCharacterInfo>();
    }

    public class MovieCharacterInfo
    {
        public string MovieTitle { get; set; }
        public string CharacterName { get; set; }
        public string MoviePosterPath { get; set; }
    }
}
