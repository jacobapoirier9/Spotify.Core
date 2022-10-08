using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";
    private const string Token = "BQCwDjf-8qc8tnPkhYPe5niQReuBJkYNFpDT_wvN3aregL5gHwSGAP2so_UraFuCED4cKkxZuYgLan7hOqTEf_-MmSsFBm2pA2iz4CShZIWkoM-xsG_jcO2BBA3Sr-Cd0THrKYwcRqZkAqd0BYhy1pnvTQdGZ9pYz2-kdyHrGmSPGtXHXlZ9MdcmOSN6xHIGR3jFNx89HbwtfRsRyHoSh8njuyfEzI_NmtjfHndQtY9eGosRIP_bQWl7B2Y7B7eXweiDp5oZ90ox8f3tasL4LIiy4pN49zPAP4J1c89UTLWm13tp8_4bbUWXAd3eqAQC";

    static void Main(string[] args)
    {
        // Jocko podcast show - 7irxBvxNqGYnUdFo1c2gMc
        var client = new SpotifyClient(Url);

        var get = client.Put(new UnfollowArtistsOrUsers() { Type = ItemType.Artist, IdsBody = "2CIMQHirSU0MQqyYHq0eOx".AsList() }, Token);

        //var delete = client.Delete(new RemoveSavedEpisodes { IdsBody = new List<string> { "5a6AizhZX1CqzvRUOcCALU" } }, Token);

        //var put = client.Put(new SaveEpisodes { Ids = new List<string> { "5a6AizhZX1CqzvRUOcCALU" } }, Token);

    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}