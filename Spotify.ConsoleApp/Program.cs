using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";
    private const string Token = "BQDZk5ErFXSuPG6IxSPK9gTkqYLGhTdCJN99dIRCGPT4iyb_csS2aVM_FeMBjd-koFmu6T8WDVD1gyQKkEz6ombOeqb-ZYqqx4FuRFNGxgl7H9C2DVg4ruMla8KFgbJJHdGVJRhMNOvkbZLTh7qi9TMgzemRHfp_Xt90wkhi0vMtunvyLs0LS_BoEW6r0vabcyS8uGmHX4G6GfGbTygosG9HKMySoFeLlPUVrhw6lsrY9QAhjsu7MRZNNISTHxe27OpIqls1qpszNYYdGcIXxt8OH1r5fKEuzxyorvEZaduX9ZuDghQzUXaahJD7lSdO";

    static void Main(string[] args)
    {
        // Jocko podcast show - 7irxBvxNqGYnUdFo1c2gMc
        var client = new SpotifyClient(Url);

        var get = client.Get(new GetChapter { Id = "38bS44xjbVVZ3No3ByF1dJ" }, Token);

        //var te = client.Get(new GetSeveralAudiobooks { Ids = "38bS44xjbVVZ3No3ByF1dJ".AsList() }, Token);

        //var delete = client.Delete(new RemoveSavedEpisodes { IdsBody = new List<string> { "5a6AizhZX1CqzvRUOcCALU" } }, Token);

        //var put = client.Put(new SaveEpisodes { Ids = new List<string> { "5a6AizhZX1CqzvRUOcCALU" } }, Token);

    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}