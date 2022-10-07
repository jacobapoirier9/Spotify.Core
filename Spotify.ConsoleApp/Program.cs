using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";
    private const string Token = "BQDjnR21tTPaUnbJUWt-PeEB7Vdgx1J8EhOk_GSGq6nqZyp2xa8DYVjkByJnSLCJmBoUkJGh3xtBCz4W3ZoJGOVR6eb0u8NUrCsFzuVz3SIxmJaT3fV9zz2eu99zBXwOndZACkT4X8rKMpdiJNVJ7cPs1gkzz8YPp6o5YfrPtlQxPvEDTxBZtSZYE0myGqqvJGB00n7YerYzBBWjvxlljC73-Ik9BVW3H2JlDr7KkEWZf6INKGZB5kEYvXNoByg_f1dZAOpMP9ASmWynmL2ONvVRxnmz-x2jYsEWdYUZz7DGN0GClk3gdFSRaN5E_OAy";

    static void Main(string[] args)
    {
        // Jocko podcast show - 7irxBvxNqGYnUdFo1c2gMc
        var client = new SpotifyClient(Url);

        //var get = client.Get(new GetUserSavedShows(), Token);



        //var delete = client.Delete(new RemoveSavedShows { Ids = new List<string> { "7irxBvxNqGYnUdFo1c2gMc" } }, Token);

        int x = 3;

        //var put = client.Put(new SaveShows { Ids = new List<string> { "7irxBvxNqGYnUdFo1c2gMc" } }, Token);

    }
}