using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";
    private const string Token = "BQC7K2tOrGzRyeaLWAzvShSM_Id1k4OTfJiqCTqPhxXj5YuBihOf4zdnoy-RjSIHQfRMmbP6V8fxNQ2-4tljyUamc1nMwBlCdX0s5BXnssFi300Qj2pZLlk3wjWT3yocm6N-S3sygGvDioAJs7BeN39OwbXFspMaeVccZNY460KHhcf-8V5y";

    static void Main(string[] args)
    {
        var client = new SpotifyClient(Url);

        var album = client.Get(new GetAlbum { Id = "4aawyAB9vmqN3uQ7FjRGTy" }, Token);
    }
}