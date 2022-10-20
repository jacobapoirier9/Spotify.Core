using Microsoft.AspNetCore.Mvc;
using NLog;
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
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

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
            _logger.Debug("Loading full list of playlists");

            var playlists = _spotifyClient.InvokePagable(new GetSavedPlaylists(), response => response, _bearerToken);

            _logger.Debug("Found {Count} playlists", playlists.Count);

            return View("MultiplePlaylists", playlists);
        }
        else
        {
            _logger.Debug("Need to get playlist {PlaylistId}", playlistId);

            var playlist = _spotifyClient.Invoke(new GetPlaylist { PlaylistId = playlistId }, _bearerToken);

            _logger.Debug("Found {PlaylistName}", playlist?.Name);

            return View("SinglePlaylist", playlist);
        }
    }
}