using Microsoft.AspNetCore.Mvc;

namespace Spotify.Web;

public static class UrlHelperExtensions
{
    public static string ApplicationUrl(this IUrlHelper urlHelper)
    {
        var request = urlHelper.ActionContext.HttpContext.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
        return baseUrl;
    }
}