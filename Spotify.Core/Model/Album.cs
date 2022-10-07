using Spotify.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Core.Model;

[Route($"/albums/{{{nameof(Id)}}}", Verb.Get)]
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

internal class Album
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
    /// The precision with which <see cref="ReleaseDate" /> value is known.
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