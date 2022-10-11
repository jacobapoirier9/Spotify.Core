using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Spotify.Web.Controllers;
public class HomeController : Controller
{
    public HomeController()
    {
    }

    public IActionResult Index() => View();
}
