using Spotify.Core.Attributes;

namespace Spotify.Core.Model;

/// <summary>
/// Get the list of markets where Spotify is available.
/// </summary>
[Route($"{Configuration.ApiUri}/markets", Verb.Get)]
public class GetAvailableMarkets : IReturn<AvailableMarkets>
{
}

public class AvailableMarkets
{
    /// <summary>
    /// A markets object with an array of country codes
    /// </summary>
    public List<string>? Markets { get; set; }
}
