namespace Spotify.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RouteAttribute : Attribute
{
    /// <summary>
    /// The full uri string for the endpoint
    /// </summary>
    public string Uri { get; private set; }

    /// <summary>
    /// The HTTP method for the endpoint
    /// </summary>
    public string Verb { get; private set; }

    public RouteAttribute(string uri, string verb)
    {
        Uri = uri;
        Verb = verb;
    }
}