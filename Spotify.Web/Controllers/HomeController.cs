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

            if (playlist?.Tracks?.Next is not null)
            {
                var tracks = _spotifyClient.InvokePagable(new GetPlaylist { PlaylistId = playlistId }, response => response?.Tracks!, _bearerToken);
                playlist.Tracks.Items = tracks;
            }

            _logger.Debug("Found {PlaylistName}", playlist?.Name);

            return View("SinglePlaylist", playlist);
        }
    }

    public IActionResult Tracks(string trackId)
    {
        if (trackId is null)
        {
            throw new NotImplementedException();
        }
        else
        {
            _logger.Debug("Need to get track {TrackId}", trackId);

            var track = _spotifyClient.Invoke(new GetTrack { Id = trackId }, _bearerToken);

            _logger.Debug("Found {TrackName}", track?.Name);

            return View("SingleTrack", new HomeSingleTrack
            {
                Track = track
            });
        }
    }
}