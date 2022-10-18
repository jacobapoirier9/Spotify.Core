using Spotify.Core;
using Spotify.Core.Model;
using System.Runtime.CompilerServices;

namespace Spotify.ConsoleApp;

internal static class Program
{
    private static readonly string Token = File.ReadAllText("D:\\SpotifyToken.txt");

    private static void Main(string[] args)
    {
        var client = new SpotifyClient()
        {
            LogResponses = (request, response) =>
            {
                Console.WriteLine($"{request.Method} {response.StatusCode} {request?.RequestUri?.ToString()}");
            }
        };

        var playlists = client.InvokePagable(new GetCurrentUsersPlaylists(), response => response, Token);
    }

}


public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}