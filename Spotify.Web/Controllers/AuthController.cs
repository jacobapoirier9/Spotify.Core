using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Spotify.Core;
using Spotify.Core.Model;
using Microsoft.AspNetCore.Authorization;

namespace Spotify.Web.Controllers;

public class AuthController : Controller
{
    private readonly SpotifyClient _spotifyClient;
    private readonly IConfiguration _configuration;
    public AuthController(SpotifyClient spotifyClient, IConfiguration configuration)
    {
        _spotifyClient = spotifyClient;
        _configuration = configuration;
    }

    private string BuildUri(string uri, object queryParams)
    {
        if (!uri.Contains('?'))
            uri += '?';

        foreach (var queryParam in queryParams.GetType().GetProperties())
        {
            var value = queryParam.GetValue(queryParams);
            if (value is not null)
            {
                if (uri.Contains(queryParam.Name + '='))
                {
                    uri = Regex.Replace(uri, queryParam.Name + "=\\w*", queryParam.Name + '=' + value);
                }
                else
                {
                    uri += '&' + queryParam.Name + '=' + value;
                }
            }
        }

        return uri;
    }

    [AllowAnonymous]
    public IActionResult Login(string? code)
    {
        if (code is null)
        {
            // This will eventually be moved into Spotify.Core in a cleaner way
            var response = BuildUri("https://accounts.spotify.com/authorize", new
            {
                response_type = "code",
                redirect_uri = _configuration.GetValue<string>("Spotify:RedirectUri"),
                client_id = _configuration.GetValue<string>("Spotify:ClientId"),
                scope = string.Join("+", new string[]
                    {
                        "playlist-read-private", "user-read-playback-position", "user-read-email", "user-library-modify",
                        "playlist-read-collaborative", "playlist-modify-private", "user-follow-read", "user-read-playback-state",
                        "user-read-currently-playing", "user-read-recently-played", "user-modify-playback-state", "ugc-image-upload",
                        "playlist-modify-public", "user-top-read", "user-library-read", "user-read-private", "user-follow-modify"
                    })
            });

            return Redirect(response);
        }
        else
        {
            var token = _spotifyClient.CodeForAccessToken(code);
            var user = _spotifyClient.Invoke(new GetCurrentUserProfile(), token?.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("Username", user?.DisplayName ?? string.Empty));
            claims.Add(new Claim("AccessToken", token?.AccessToken ?? string.Empty));

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            HttpContext.SignInAsync(claimsPrincipal);

            System.IO.File.WriteAllText("D:\\SpotifyToken.txt", token?.AccessToken);

            //_logger.Debug("Spotify user signed in is {Username}", user.DisplayName);
            return RedirectToAction("Index", "Home");
        }
    }
}
