using Spotify.Core.Attributes;
using System.Net;

namespace Spotify.Core.Model;

/// <summary>
/// Get Spotify catalog information for a single album.
/// </summary>
[Route($"{Configuration.ApiUri}/albums/{{{nameof(Id)}}}", Verb.Get)]
public class GetAlbum : IReturn<Album>
{
    /// <summary>
    /// The Spotify ID of the album.
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
/// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
/// </summary>
[Route($"{Configuration.ApiUri}/albums", Verb.Get)]
public class GetSeveralAlbums : IReturn<SeveralAlbums>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the albums. Maximum: 20 IDs.
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only content that is available in that market will be returned.
    /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
    /// Note: If neither market or user country are provided, the content is considered unavailable for the client.
    /// Users can view the country that is associated with their account in the account settings.
    /// </summary>
    public string? Market { get; set; }
}

/// <summary>
/// Get Spotify catalog information about an album’s tracks. Optional parameters can be used to limit the number of tracks returned.
/// </summary>
[Route($"{Configuration.ApiUri}/albums/{{{nameof(Id)}}}/tracks", Verb.Get)]
public class GetAlbumTracks : IReturnPagable<Pagable<Track>>
{
    /// <summary>
    /// The Spotify ID of the album.
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
/// Get a list of the albums saved in the current Spotify user's 'Your Music' library.
/// </summary>
[Route($"{Configuration.ApiUri}/me/albums", Verb.Get)]
public class GetSavedAlbums : IReturnPagable<Pagable<SavedAlbumContextWrapper>>
{
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
/// Save one or more albums to the current user's 'Your Music' library.
/// </summary>
[Route($"{Configuration.ApiUri}/me/albums", Verb.Put)]
public class SaveAlbums : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the albums. Maximum: 20 IDs.
    /// This will be written to the URL
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// A maximum of 50 items can be specified in one request. Note: if the ids parameter is present in the query string, any IDs listed here in the body will be ignored.
    /// </summary>
    [Body(WriteValueOnly = true)]
    public List<string>? IdsBody { get; set; }
}

/// <summary>
/// Remove one or more albums from the current user's 'Your Music' library.
/// </summary>
[Route($"{Configuration.ApiUri}/me/albums", Verb.Delete)]
public class RemoveSavedAlbums : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the albums. Maximum: 20 IDs.
    /// This will be written to the URL
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// A maximum of 50 items can be specified in one request. Note: if the ids parameter is present in the query string, any IDs listed here in the body will be ignored.
    /// </summary>
    [Body(WriteValueOnly = true)]
    public List<string>? IdsBody { get; set; }
}

/// <summary>
/// Check if one or more albums is already saved in the current Spotify user's 'Your Music' library.
/// </summary>
[Route($"{Configuration.ApiUri}/me/albums/contains", Verb.Get)]
public class CheckSavedAlbums : IReturn<List<bool>>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the albums. Maximum: 20 IDs.
    /// </summary>
    public List<string>? Ids { get; set; }
}

/// <summary>
/// Get a list of new album releases featured in Spotify (shown, for example, on a Spotify player’s “Browse” tab).
/// </summary>
[Route($"{Configuration.ApiUri}/browse/new-releases", Verb.Get)]
public class GetNewReleases : IReturnPagable<NewReleases>
{
    /// <summary>
    /// A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want the list of returned items to be relevant to a particular country. 
    /// If omitted, the returned items will be relevant to all countries.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// The index of the first item to return. Default: 0 (the first item). Use with limit to get the next set of items.
    /// </summary>
    public int? Offset { get; set; }
}


/// <summary>
/// A wrapper for the <see cref="GetSeveralAlbums"/> request
/// </summary>
public class SeveralAlbums
{
    /// <summary>
    /// A set of albums
    /// </summary>
    public List<Album>? Albums { get; set; }
}

/// <summary>
/// A wrapper for the <see cref="GetNewReleases"/> request
/// </summary>
public class NewReleases
{
    /// <summary>
    /// A paged set of albums
    /// </summary>
    public Pagable<Album>? Albums { get; set; }
}

/// <summary>
/// A wrapper for the context of an album
/// </summary>
public class SavedAlbumContextWrapper
{
    /// <summary>
    /// When an album was added
    /// </summary>
    public DateTime? AddedAt { get; set; }

    /// <summary>
    /// The associated Spotify album
    /// </summary>
    public Album? Album { get; set; }
}

public class Album
{
    /// <summary>
    /// The type of the album. Album, Single, Compilation
    /// </summary>
    public string? AlbumType { get; set; }

    /// <summary>
    /// The number of tracks in the album.
    /// </summary>
    public int? TotalTracks { get; set; }

    /// <summary>
    /// The markets in which the album is available: ISO 3166-1 alpha-2 country codes. 
    /// NOTE: an album is considered available in a market when at least 1 of its tracks is available in that market.
    /// </summary>
    public List<string>? AvailableMarkets { get; set; }

    /// <summary>
    /// Known external URLs for this album.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the album.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify ID for the album.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The cover art for the album in various sizes, widest first.
    /// </summary>
    public List<Image>? Images { get; set; }

    /// <summary>
    /// The name of the album. In case of an album takedown, the value may be an empty string.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The date the album was first released.
    /// </summary>
    public string? ReleaseDate { get; set; }

    /// <summary>
    /// The precision with which <see cref="ReleaseDate"/> value is known.
    /// Allowed values: year, month, day
    /// </summary>
    public string? ReleaseDatePrecision { get; set; }

    /// <summary>
    /// Included in the response when a content restriction is applied.
    /// </summary>
    public Restrictions? Restrictions { get; set; }

    /// <summary>
    /// The object type.
    /// </summary>
    public ItemType Type { get; set; }

    /// <summary>
    /// The Spotify URI for the album.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// The artists of the album. Each artist object includes a link in href to more detailed information about the artist
    /// </summary>
    public List<Artist>? Artists { get; set; }
}