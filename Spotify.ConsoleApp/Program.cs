using Spotify.Core;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
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

        client.CallApi(new GetCurrentUsersPlaylists(), Token);
    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}