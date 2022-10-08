using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";
    private const string Token = "BQD8TTH1UY41TAPUBWdDAXP00dvGUh-pW3NIPt5mYSiiKhv8JQ9_DoaTay8QOIFEq0ZQKkQbU1y8jrLlbtplcN2HllfZM2z20sbbrKFmsBwPSn1c4cpSnS7NLJvBWCpmye09JyrYRiy1xoygGiO35rw8hdNThy5D8fi0mFhRHPO0wyREQRo4bFWrQfX-pZgJgqLeJeTZgcBxyAEojjm0fQgR62o5UUNXxkwfv1gshXKxoXur5VAp9a8eAFsTNvzvDRUi4xi8iG0bWhK9ecPRYAW6bryHqJzSsKcxwbahn0PjfgJjAUdXXdowy43RYi73";

    static void Main(string[] args)
    {
        // Jocko podcast show - 7irxBvxNqGYnUdFo1c2gMc
        var client = new SpotifyClient(Url);

        var get = client.Get(new GetAvailableGenreSeeds(), Token);
    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}