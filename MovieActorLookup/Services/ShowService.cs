using MovieActorLookup.Models;
using MovieActorLookup.Models.TvShows;
using RestSharp;
using System.Text.Json;

public class ShowService
{
    private readonly string _baseUrl;
    private readonly string _apiKey;
    private readonly string _bearerToken;

    public ShowService(string baseUrl, string apiKey, string bearerToken)
    {
        _baseUrl = baseUrl;
        _apiKey = apiKey;
        _bearerToken = bearerToken;
    }

    public async Task<List<TvShow>> GetShowsByTitle(string title)
    {
        var client = new RestClient("https://api.themoviedb.org/3/");
        var request = new RestRequest("search/tv")
            .AddQueryParameter("query", title)
            .AddQueryParameter("include_adult", "false")
            .AddQueryParameter("language", "en-US")
            .AddQueryParameter("page", "1");

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await client.GetAsync(request);
        var shows = new List<TvShow>();

        if (response.IsSuccessful)
        {
            var showResponse = JsonSerializer.Deserialize<TvResponse>(response.Content);
            if (showResponse != null && showResponse.Results.Any())
            {
                foreach (var show in showResponse.Results)
                {
                    shows.Add(new TvShow
                    {
                        Id = show.Id,
                        Name = show.Name,
                        Overview = show.Overview,
                        PosterPath = show.PosterPath != null ? $"https://image.tmdb.org/t/p/w500{show.PosterPath}" : null,
                        BackdropPath = show.BackdropPath != null ? $"https://image.tmdb.org/t/p/w500{show.BackdropPath}" : null,
                        FirstAirDate = show.FirstAirDate,
                        Popularity = show.Popularity,
                        VoteAverage = show.VoteAverage
                    });
                }
            }
        }

        return shows;
    }

    public async Task<Credits> GetShowCreditsById(int showId)
    {
        var client = new RestClient(_baseUrl);
        var request = new RestRequest($"tv/{showId}/credits")
            .AddQueryParameter("language", "en-US");

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await client.GetAsync(request);

        if (response.IsSuccessful)
        {
            return JsonSerializer.Deserialize<Credits>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }

    public async Task<MovieActorLookup.Models.TvShow.TvShow.ActorTvFilmography> GetActorTvCreditsById(int personId)
    {
        var client = new RestClient(_baseUrl);
        var request = new RestRequest($"person/{personId}/tv_credits")
            .AddQueryParameter("language", "en-US");

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await client.GetAsync(request);

        if (response.IsSuccessful)
        {
            return JsonSerializer.Deserialize<MovieActorLookup.Models.TvShow.TvShow.ActorTvFilmography>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }

    public async Task<TvShow> GetShowDetailsById(int showId)
    {
        var client = new RestClient(_baseUrl);
        var request = new RestRequest($"tv/{showId}")
            .AddQueryParameter("language", "en-US");

        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_bearerToken}");

        var response = await client.GetAsync(request);

        if (response.IsSuccessful)
        {
            return JsonSerializer.Deserialize<TvShow>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }
}
