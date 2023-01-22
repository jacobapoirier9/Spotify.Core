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

        

        var chapters = client.Invoke(new GetAudiobookChapters { Id = "38bS44xjbVVZ3No3ByF1dJ" }, Token);
    }
}