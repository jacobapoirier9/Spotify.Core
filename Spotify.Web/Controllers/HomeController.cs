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
        var filePath = @"D:\Playlists.json";
        if (playlistId is null)
        {
            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                var playlists = JsonSerializer.Deserialize<List<Playlist>>(json);
                return View(playlists);
            }
            else
            {
                //var playlists = _spotifyClient.InvokePagable(new GetSavedPlaylists(), response => response, _bearerToken);
                var playlists = _spotifyClient.InvokePagable(new GetSavedPlaylists(), response => response, _bearerToken);
                var json = JsonSerializer.Serialize(playlists);
                System.IO.File.WriteAllText(filePath, json);
                return View(playlists);
            }
        }
        else
        {
            var json = System.IO.File.ReadAllText(filePath);
            var playlists = JsonSerializer.Deserialize<List<Playlist>>(json);
            var playlist = playlists?.First(p => p.Id == playlistId);
            return Json(playlist);
        }
    }
}