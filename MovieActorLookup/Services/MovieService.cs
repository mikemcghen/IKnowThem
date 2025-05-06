using MovieActorLookup.Models;
using MovieActorLookup.Models.Movies;
using MovieActorLookup.Models.Movie.Movie;
using RestSharp;
using System.Text.Json;

public class MovieService
{
    private readonly string _baseUrl;
    private readonly string _apiKey;
    private readonly string _bearerToken;

    public MovieService(string baseUrl, string apiKey, string bearerToken)
    {
        _baseUrl = baseUrl;
        _apiKey = apiKey;
        _bearerToken = bearerToken;
    }

    public async Task<List<Movie>> GetMoviesByTitle(string title)
    {
        var client = new RestClient("https://api.themoviedb.org/3/");

        var request = new RestRequest("search/movie")
                          .AddQueryParameter("query", title)
                          .AddQueryParameter("include_adult", "false")
                          .AddQueryParameter("language", "en-US")
                          .AddQueryParameter("page", "1");

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await client.GetAsync(request);

        var movies = new List<Movie>();
        if (response.IsSuccessful)
        {
            var movieResponse = JsonSerializer.Deserialize<MovieResponse>(response.Content);
            if (movieResponse != null && movieResponse.Results.Any())
            {
                foreach (var apiMovie in movieResponse.Results)
                {
                    var movie = new Movie
                    {
                        Id = apiMovie.Id,
                        Title = apiMovie.Title,
                        OriginalTitle = apiMovie.OriginalTitle,
                        Overview = apiMovie.Overview,
                        Adult = apiMovie.Adult,
                        BackdropPath = apiMovie.BackdropPath != null ? $"https://image.tmdb.org/t/p/w500{apiMovie.BackdropPath}" : null,
                        GenreIds = apiMovie.GenreIds,
                        OriginalLanguage = apiMovie.OriginalLanguage,
                        Popularity = apiMovie.Popularity,
                        PosterPath = apiMovie.PosterPath != null ? $"https://image.tmdb.org/t/p/w500{apiMovie.PosterPath}" : null,
                        ReleaseDate = apiMovie.ReleaseDate,
                        Video = apiMovie.Video,
                        VoteAverage = apiMovie.VoteAverage,
                        VoteCount = apiMovie.VoteCount
                    };
                    movies.Add(movie);
                }
            }
        }
        return movies;
    }

    public async Task<Credits> GetMovieCreditsByMovieId(int movieId)
    {
        var client = new RestClient(_baseUrl);

        var request = new RestRequest($"movie/{movieId}/credits")
                          .AddQueryParameter("language", "en-US"); 

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await client.GetAsync(request);

        if (response.IsSuccessful)
        {
            var credits = JsonSerializer.Deserialize<Credits>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return credits;
        }
        else
        {
            Console.WriteLine("Error retrieving movie credits: " + response.ErrorMessage);
            return null;
        }
    }

    public async Task<ActorMovieFilmography> GetActorMovieCreditsById(int personId)
    {
        var client = new RestClient(_baseUrl);
        var request = new RestRequest($"person/{personId}/movie_credits")
                          .AddQueryParameter("language", "en-US");

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await client.GetAsync(request);

        if (response.IsSuccessful)
        {
            // Deserialize the response into the ActorFilmography class
            var actorFilmography = JsonSerializer.Deserialize<ActorMovieFilmography>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return actorFilmography;
        }
        else
        {
            Console.WriteLine("Error retrieving actor's movie credits: " + response.ErrorMessage);
            return null;
        }
    }

    public async Task<Movie> GetMovieDetailsById(int movieId)
    {
        var client = new RestClient(_baseUrl);

        var request = new RestRequest($"movie/{movieId}")
                          .AddQueryParameter("language", "en-US");

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await client.GetAsync(request);

        if (response.IsSuccessful)
        {
            var movie = JsonSerializer.Deserialize<Movie>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return movie;
        }
        else
        {
            Console.WriteLine("Error retrieving movie details: " + response.ErrorMessage);
            return null;
        }
    }
}
