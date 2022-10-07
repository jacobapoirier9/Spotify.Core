using Spotify.Core;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var request = new Test
        {
            Name = "Jake"
        };

        var httpRequest = HttpRequestMessageBuilder.BuildRequestMessage(Url, request);

        Console.WriteLine(request);
    }
}

[Route("/test")]
internal class Test
{
    public string? Name { get; set; }
}
