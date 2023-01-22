using Spotify.Core.Attributes;

namespace Spotify.Core.Model;

/// <summary>
/// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
/// </summary>
[Route($"{Configuration.ApiUri}/artists/{{{nameof(Id)}}}", Verb.Get)]
public class GetArtist : IReturn<Artist>
{
    /// <summary>
    /// The Spotify ID of the artist.
    /// </summary>
    public string? Id { get; set; }
}

/// <summary>
/// Get Spotify catalog information for several artists based on their Spotify IDs.
/// </summary>
[Route($"{Configuration.ApiUri}/artists", Verb.Get)]
public class GetSeveralArtists : IReturn<SeveralArtists>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the artists. Maximum: 50 IDs.
    /// </summary>
    public List<string>? Ids { get; set; }
}

/// <summary>
/// Get Spotify catalog information about an artist's albums.
/// </summary>
[Route($"{Configuration.ApiUri}/artists/{{{nameof(Id)}}}/albums", Verb.Get)]
public class GetArtistsAlbums : IReturnPagable<Pagable<Album>>
{
    /// <summary>
    /// A comma-separated list of keywords that will be used to filter the response. If not supplied, all album types will be returned.
    /// Valid values are: album, single, appears_on, compilation
    /// </summary>
    public List<string>? IncludeGroups { get; set; }

    /// <summary>
    /// The Spotify ID of the artist.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only content that is available in that market will be returned.
    /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
    /// Note: If neither market or user country are provided, the content is considered unavailable for the client.
    /// Users can view the country that is associated with their account in the account settings.
    /// </summary>
    public string? Market { get; set; }

    /// <summary>
    /// The index of the first item to return. Default: 0 (the first item). Use with limit to get the next set of items.
    /// </summary>
    public int? Offset { get; set; }
}

/// <summary>
/// Get Spotify catalog information about an artist's top tracks by country.
/// </summary>
[Route($"{Configuration.ApiUri}/artists/{{{nameof(Id)}}}/top-tracks", Verb.Get)]
public class GetArtistsTopTracks : IReturn<ArtistsTopTracks>
{
    /// <summary>
    /// The Spotify ID of the artist.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only content that is available in that market will be returned.
    /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
    /// Note: If neither market or user country are provided, the content is considered unavailable for the client.
    /// Users can view the country that is associated with their account in the account settings.
    /// </summary>
    public string? Market { get; set; }
}

/// <summary>
/// Get Spotify catalog information about artists similar to a given artist. Similarity is based on analysis of the Spotify community's listening history.
/// </summary>
[Route($"{Configuration.ApiUri}/artists/{{{nameof(Id)}}}/related-artists", Verb.Get)]
public class GetArtistsRelatedArtists : IReturn<ArtistsRelatedArtists>
{
    /// <summary>
    /// The Spotify ID of the artist.
    /// </summary>
    public string? Id { get; set; }
}

/// <summary>
/// A wrapper for the <see cref="GetSeveralArtists"/> request
/// </summary>
public class SeveralArtists
{
    /// <summary>
    /// A set of artists
    /// </summary>
    public List<Artist>? Artists { get; set; }
}

/// <summary>
/// A wrapper for the <see cref="GetArtistsTopTracks"/> request
/// </summary>
public class ArtistsTopTracks
{
    /// <summary>
    /// A set of tracks
    /// </summary>
    public List<Track>? Tracks { get; set; }
}

/// <summary>
/// A wrapper for the <see cref="GetArtistsRelatedArtists"/> request
/// </summary>
public class ArtistsRelatedArtists
{
    /// <summary>
    /// A set of artists
    /// </summary>
    public List<Artist>? Artists { get; set; }
}

public class Artist
{
    /// <summary>
    /// Known external URLs for this artist.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// Information about the followers of the artist.
    /// </summary>
    public Followers? Followers { get; set; }

    /// <summary>
    /// A list of the genres the artist is associated with. If not yet classified, the array is empty.
    /// </summary>
    public List<string>? Genres { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the artist.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify ID for the artist.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Images of the artist in various sizes, widest first.
    /// </summary>
    public List<Image>? Images { get; set; }

    /// <summary>
    /// The name of the artist.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular. 
    /// The artist's popularity is calculated from the popularity of all the artist's tracks.
    /// </summary>
    public int? Popularity { get; set; }

    /// <summary>
    /// The object type.
    /// </summary>
    public ItemType Type { get; set; }

    /// <summary>
    /// The Spotify URI for the artist.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// The tracks of the album.
    /// </summary>
    public Pagable<Track>? Tracks { get; set; }
}

