using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";
    private const string Token = "BQA6br_9EL6bR1YI5bbIfBbcSriUITj4VEm7aSkvux4kcY2JwL1JUiJv9fUdSdNMkP6Ikp01h5RaTLwxTEkU-2uY_idjDtuL-ZM7VysRcZpHf_h0XvjzdC6widBIiXiQRJhvffGkVREGKdPz5uS2XjFRuKDYfAnhMLXT6ITeRUS4FoK1kztzOyejg4hqBfVPVxutWwZT-yiUTYIOSxMjryAM4TmmG5_gARBmquFFX-mVpO-HWk8JbooxJTiDyjvELQiAsRnuUisSqhpsc6bCVR-IDypdeIFX9UAonxtekCy8hxY1IYZunodAzeruabmm";

    static void Main(string[] args)
    {
        var client = new SpotifyClient(Url);

        var get = client.Get(new GetNewReleases(), Token);

        //var put = client.Put(new SaveAlbums { Ids = new List<string> { "382ObEPsp2rxGrnsizN5TX" } }, Token);

        //var delete = client.Delete(new RemoveAlbums { Ids = new List<string> { "382ObEPsp2rxGrnsizN5TX" } }, Token);
    }
}