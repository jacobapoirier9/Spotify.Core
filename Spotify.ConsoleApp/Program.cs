using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Token = "BQDR3sSX-Wa3ukD2Om7DxGw85LbI-06PMGpOxaydG_DP7WyWZzuOpKsLN5vrlqewc0BAt25D2jRhihtuTIw03EcdkJskRgGpm_9ec5jW1JrCXW5788cZQX6rIWuqPU94IFNL3tXIQOXRwr81LpfTBKqplaSTR8vv2qWUr1HWsUk3GyRi5u7WpIp6WRpLIMsDEc4SCI_7fb3MP2ws7YAO0sWQSStkkQ11OEXr0dcYUCjfmD4fB9LatzqrDD2G3Z4Rn7O8t6fI2OWXijUGssU4as6xMYC5TkA175fKzlF3GyW-4MTAqJNPAC3UnVyQJX09";

    static void Main(string[] args)
    {
        var client = new SpotifyClient();

        var playlists = client.Request(new GetCurrentUsersPlaylists(), Token);
        playlists.Items.ForEach(p => Console.WriteLine(p.Name + " " + p.Id));

        var response = client.Request(new ChangePlaylistDetails
        {
            PlaylistId = "3rXJqhT8GezYfEUpKXBOjd",
            Description = "TEST"
        }, Token);
    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}