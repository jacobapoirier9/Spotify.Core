using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Token = "BQBz9fq24wob2DlDbl8iHyBif9iPYSbCoQrETPuoTVIp4M9_8Q1iVzq5GjE4PV_C5QmPFhczuMVk0yPLr4ejezvJ0DOXDmIeUv-H642_lMPpFRQRRx6_A6-L7IHAOwmBbiLjZ7k2nfF9KbwF83EpivFk8RCfiNUSHkOPhyc3sUfv9gmak1vUlfzETeux7oPoeT7UaG036Iw5D19LLf8AgvIHXRWezTVm7IP_41wFPffqWM-Is2VZZa_woZQ3Z8XtoVd1rfnlizNxufKHriShg-Qsk-kxUsEv8Yt_kFCcg2WdSzP3VKLkZPQ4l0pk8m5s";

    static void Main(string[] args)
    {
        // Jocko podcast show - 7irxBvxNqGYnUdFo1c2gMc
        var client = new SpotifyClient();

        var playlists = client.Get(new GetPlaybackQueue() {}, Token);
    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}