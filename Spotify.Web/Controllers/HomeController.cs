using Microsoft.AspNetCore.Mvc;
using Spotify.Core;
using Spotify.Core.Model;
using Spotify.Web.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

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
    }
}
