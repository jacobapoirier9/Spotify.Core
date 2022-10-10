using Spotify.Core.Attributes;

namespace Spotify.Core.Model;

/// <summary>
/// Retrieve a list of available genres seed parameter values for recommendations.
/// </summary>
[Route($"{Configuration.ApiUri}/recommendations/available-genre-seeds", Verb.Get)]
public class GetAvailableGenreSeeds : IReturn<AvailableGenreSeeds>
{

}

public class AvailableGenreSeeds
{
    /// <summary>
    /// A set of genres
    /// </summary>
    public List<string>? Genres { get; set; }
}
