namespace Spotify.Core;

public class RouteAttribute : Attribute
{
    public string Path { get; private set; }
    public string? Verb { get; private set; }

    public RouteAttribute(string path, string? verb = null)
    {
        Path = path;
        Verb = verb;
    }
}