using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Token = "BQAOaG6-AlXkxLK8DSAAU0vrOD9skEeUhKNWJZf5R1fz1ErjxSVzAI1iS54hRziqfJIg8mt6GVGC7L1ClKyzEZBK0sL7Ld_dr4aDyR5YMh6fYvvXX6mlnVYF7BBqIDNFxhIOUEg0eWkIXQ-IFeKv9OhTXpvIkzmBUdfjIilEr44X2SSHI1edRB_Mkj9J6NeazyAgxIvTDRVKtjlOMDFga1FvmD8AbQeYfw7LeRU_fpvkQC6MXc8y0rRAokfqcIX-BcPzfGR4JyeBUaRaMPW5FlfDCJirPIW60_Bji2c3PHekUsgrXE9hGvALXn619sLg";

    static void Main(string[] args)
    {
        // Jocko podcast show - 7irxBvxNqGYnUdFo1c2gMc
        var client = new SpotifyClient(Configuration.ApiUri);

        var playlists = client.Put(new FollowArtistsOrUsers() { Type = ItemType.Artist }, Token);
    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}