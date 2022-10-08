using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";
    private const string Token = "BQABu33fIZO7AVBG6jCqPto1NDEMjbky6v57p78KFKHgW-TbTCCSVWAo5rbNmuthQHEgCepT_bxXbyzxbftejVoaufZmfw6vevEW3BIl0DqraT6JytUZs0mPNOksKHcEEUnEwuegErI6PO85ZwJPdokbVSRtInm6kOMgB42u_U_xOAi77CqEil9zJOTOibrLBO3WBO-J8_oF0c-0F5XYKIF2RqoixABVFcc8aTshTnO9SGccZSAvqJAfoGHAaK0pXr0gKSsXxoi1fcU5oBfg5WFcGtvS4W3l8ljKy_M65AAv8bq20ZEPjWUc2gv5ivYd";

    static void Main(string[] args)
    {
        // Jocko podcast show - 7irxBvxNqGYnUdFo1c2gMc
        var client = new SpotifyClient(Url);

        var get = client.Get(new GetPlaylistCoverImage { PlaylistId = "3cEYpjA9oz9GiPac4AsH4n" }, Token);
    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}