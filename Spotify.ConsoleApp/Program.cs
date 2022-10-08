using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";
    private const string Token = "BQAXffKyl6il09fH58lAQPvNv8JNlrE9RHRTb3Y8d2EPHD8f9PgpdORAhzB-VvBdD6xu1Hri8sXoORCpjlfuKDlLC0wmHvKrrBu9gadQLgnqWcOVn77wMMrjtywUz3709eQMpWDxiG4Ov6car44QjculkRNVaUG8eRsQExsWZC3tNgmxBVzjz4HYuwjK3Noa-F8wzzHnT525j8L2bM2OAj8sMZM3J3N2NMmhqA-jElD3oWIPTLMOyXRvQ42UOky6l8szEcfxGrFk9pefuvHGZd_3Y-Mvfkio2wuL_Ahwud7WkrdZPMYX-rtkYSLm1JDA";

    static void Main(string[] args)
    {
        // Jocko podcast show - 7irxBvxNqGYnUdFo1c2gMc
        var client = new SpotifyClient(Url);


        var search = client.Get(new Search { Q = "remaster%20track:Doxy%20artist:Miles%20Davis", Type = new List<ItemType> { ItemType.Track, ItemType.Artist} }, Token);

        //var te = client.Get(new GetSeveralAudiobooks { Ids = "38bS44xjbVVZ3No3ByF1dJ".AsList() }, Token);

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