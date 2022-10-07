using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Url = "https://api.spotify.com/v1";
    private const string Token = "BQB1p4MjmtdXhJropiaC1Oet8PX2kS6h5H8NHgmeKrwAyx1FP-FJefQIK77ERFW4JSijCwdZ7uJUn3R8iYnt1iaGPCBPOtdqSo7bNhO21aTEkschvNS2kQa3qQ69cpyev0LAcpH0IgUecfGDje_uAVlw0PcT8OE0GMSlUXNyhdcSaxhv2qEIVJSsiCtL2_8t-Su3apHpcpuOgHN6l2Y06wsRhb21q6zRwe-VVQtGO4pi-Y0dxnlL8PBBCnHmtRPe5AjIJ5rJ0nP3wH-Pp-nXQPvgvLk0geTHo-aMxlHdiV89W3b_6Xws7h-CV7UbZYWF";

    static void Main(string[] args)
    {
        // Jocko podcast show - 7irxBvxNqGYnUdFo1c2gMc
        var client = new SpotifyClient(Url);

        var get = client.Get(new GetSavedEpisodes(), Token);

        var delete = client.Delete(new RemoveSavedEpisodes { IdsBody = new List<string> { "5a6AizhZX1CqzvRUOcCALU" } }, Token);

        //var put = client.Put(new SaveEpisodes { Ids = new List<string> { "5a6AizhZX1CqzvRUOcCALU" } }, Token);

    }
}