namespace Spotify.Core.Attributes;

public class RouteAttribute : Attribute
{
    public string Uri { get; private set; }
    public string? Verb { get; private set; }

    public RouteAttribute(string uri, string? verb = null)
    {
        Uri = uri;
        Verb = verb;
    }
}