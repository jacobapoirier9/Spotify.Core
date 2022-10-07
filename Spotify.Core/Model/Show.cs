using Spotify.Core.Attributes;
using System.Net;

namespace Spotify.Core.Model;

/// <summary>
/// Get Spotify catalog information for a single show identified by its unique Spotify ID.
/// </summary>
[Route($"/shows/{{{nameof(Id)}}}", Verb.Get)]
public class GetShow : IReturn<Show>
{
    /// <summary>
    /// The Spotify ID for the show.
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
/// 
/// </summary>
[Route("/shows", Verb.Get)]
public class GetSeveralShows
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the shows. Maximum: 50 IDs.
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
/// Get Spotify catalog information about an show’s episodes. Optional parameters can be used to limit the number of episodes returned.
/// </summary>
[Route($"/shows/{{{nameof(Id)}}}/episodes", Verb.Get)]
public class GetShowEpisodes : IReturn<PagableResponse<Episode>>
{
    /// <summary>
    /// The Spotify ID for the show.
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
/// Get a list of shows saved in the current Spotify user's library. Optional parameters can be used to limit the number of shows returned.
/// </summary>
[Route("/me/shows", Verb.Get)]
public class GetSavedShows : IReturn<PagableResponse<SavedShowContextWrapper>>
{
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
/// Save one or more shows to current Spotify user's library.
/// </summary>
[Route("/me/shows", Verb.Put)]
public class SaveShows : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the shows. Maximum: 50 IDs.
    /// </summary>
    public List<string>? Ids { get; set; }
}

/// <summary>
/// Delete one or more shows from current Spotify user's library.
/// </summary>
[Route("/me/shows", Verb.Delete)]
public class RemoveSavedShows : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the shows. Maximum: 50 IDs.
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
/// Check if one or more shows is already saved in the current Spotify user's library.
/// </summary>
[Route("/me/shows/contains", Verb.Get)]
public class CheckUsersSavedShows : IReturn<List<bool>>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the shows. Maximum: 50 IDs.
    /// </summary>
    public List<string>? Ids { get; set; }
}

/// <summary>
/// A wrapper for the context of a show
/// </summary>
public class SavedShowContextWrapper
{
    /// <summary>
    /// When an album was added
    /// </summary>
    public DateTime? AddedAt { get; set; }

    /// <summary>
    /// The associated Spotify album
    /// </summary>
    public Show? Show { get; set; }
}

/// <summary>
/// A wrapper for the <see cref="GetSeveralShows"/> request
/// </summary>
public class SeveralShows
{
    /// <summary>
    /// A set of shows
    /// </summary>
    public List<Show>? Shows { get; set; }
}

public class Show
{
    /// <summary>
    /// A list of the countries in which the show can be played, identified by their ISO 3166-1 alpha-2 code.
    /// </summary>
    public List<string>? AvailableMarkets { get; set; }

    /// <summary>
    /// The copyright statements of the show.
    /// </summary>
    public List<Copyright>? Copyrights { get; set; }

    /// <summary>
    /// A description of the show. HTML tags are stripped away from this field, use html_description field in case HTML tags are needed.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// A description of the show. This field may contain HTML tags.
    /// </summary>
    public string? HtmlDescription { get; set; }

    /// <summary>
    /// Whether or not the show has explicit content (true = yes it does; false = no it does not OR unknown).
    /// </summary>
    public bool? Explicit { get; set; }

    /// <summary>
    /// External URLs for this show.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the show.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify ID for the show.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The cover art for the show in various sizes, widest first.
    /// </summary>
    public List<Image>? Images { get; set; }

    /// <summary>
    /// True if all of the shows episodes are hosted outside of Spotify's CDN. This field might be null in some cases.
    /// </summary>
    public bool? IsExternallyHosted { get; set; }

    /// <summary>
    /// A list of the languages used in the show, identified by their ISO 639 code.
    /// </summary>
    public List<string>? Languages { get; set; }

    /// <summary>
    /// The media type of the show.
    /// </summary>
    public string? MediaType { get; set; }

    /// <summary>
    /// The name of the episode.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The publisher of the show.
    /// </summary>
    public string? Publisher { get; set; }

    /// <summary>
    /// The object type.
    /// </summary>
    public ItemType? Type { get; set; }

    /// <summary>
    /// The Spotify URI for the show.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// The episodes of the show.
    /// </summary>
    public PagableResponse<Episode>? Episodes { get; set; }
}
