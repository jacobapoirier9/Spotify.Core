﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using NLog;
using ServiceStack;
using ServiceStack.Text;
using Spotify.Core;
using Spotify.Core.Model;
using Spotify.Web.Models;
using Spotify.Web.Services;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace Spotify.Web.Controllers;
public class HomeController : Controller
{
    private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    private string _bearerToken => User.Claims.First(c => c.Type == "AccessToken").Value;
    private string _username => User.Claims.First(c => c.Type == "Username").Value;

    private readonly SpotifyClient _spotifyClient;
    private readonly IDataService _dataService;

    public HomeController(SpotifyClient spotifyClient, IDataService dataService)
    {
        _spotifyClient = spotifyClient;
        _dataService = dataService;
    }

    public IActionResult Index()
    {
        return View();
    }


    public void Test()
    {
        var tracks = _spotifyClient.InvokePagable(new GetRecentlyPlayedTracks
        {
            After = DateTimeOffset.Now.AddDays(-5).ToUnixTimeMilliseconds(),
            Limit = 50,
        }, response => response, _bearerToken);

        var builder = new StringBuilder();

        builder.AppendLine();
        foreach (var track in tracks)
        {
            builder.AppendLine($"{track.PlayedAt} {track.Track.Name} - {track.Track.Artists.First().Name}");
        }

        _logger.Trace(builder.ToString());
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

            var playbackState = _spotifyClient.Invoke(new GetCurrentlyPlayingTrack(), _bearerToken);

            _logger.Debug("Found {TrackName}", track?.Name);

            return View("SingleTrack", new HomeSingleTrack
            {
                Track = track,
                TrackIntervals = _dataService.GetTrackIntervals(_username, trackId),

                PlaybackState = track?.Id == playbackState?.Item?.Id ? playbackState : null
            });
        }
    }

    public IActionResult SeekTrackPosition(string trackId, int startMs)
    {
        var uri = "spotify:track:" + trackId;
        var added = _spotifyClient.Invoke(new AddItemToPlaybackQueue
        {
            Uri = uri
        }, _bearerToken);

        var skipped = _spotifyClient.Invoke(new SkipToNext(), _bearerToken);

        var result = _spotifyClient.Invoke(new SetPlaybackPosition
        {
            PositionMs = startMs
        }, _bearerToken);

        return Ok();
    }

    public IActionResult PlayTrack(string trackId)
    {
        var uri = "spotify:track:" + trackId;
        var added = _spotifyClient.Invoke(new AddItemToPlaybackQueue
        {
            Uri = uri
        }, _bearerToken);

        var skipped = _spotifyClient.Invoke(new SkipToNext(), _bearerToken);

        return Ok();
    }
}