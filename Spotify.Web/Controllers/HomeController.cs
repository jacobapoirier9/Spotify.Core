using Microsoft.AspNetCore.Mvc;
using Spotify.Core;
using Spotify.Core.Model;
using Spotify.Web.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Spotify.Web.Controllers;
public class HomeController : Controller
{
    private string _bearerToken => User.Claims.First(c => c.Type == "AccessToken").Value;
    private readonly SpotifyClient _spotifyClient;
    public HomeController(SpotifyClient spotifyClient)
    {
        _spotifyClient = spotifyClient;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Playlists(string playlistId)
    {
        if (playlistId is null)
        {
            var playlists = _spotifyClient.InvokePagable(new GetSavedPlaylists(), response => response, _bearerToken);
            var json = JsonSerializer.Serialize(playlists);
            return View("MultiplePlaylists", playlists);
        }
        else
        {
            var playlist = _spotifyClient.Invoke(new GetPlaylist { PlaylistId = playlistId }, _bearerToken);
            return View("SinglePlaylist", playlist);
        }
    }
}