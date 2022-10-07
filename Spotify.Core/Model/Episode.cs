using Spotify.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Core.Model;

/// <summary>
/// Get Spotify catalog information for a single episode identified by its unique Spotify ID.
/// </summary>
[Route($"/episodes/{{{nameof(Id)}}}", Verb.Get)]
public class GetEpisode : IReturn<Episode>
{
    /// <summary>
    /// The Spotify ID for the episode.
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
/// Get Spotify catalog information for several episodes based on their Spotify IDs.
/// </summary>
[Route("/episodes", Verb.Get)]
public class GetSeveralEpisodes : IReturn<SeveralEpisodes>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the episodes. Maximum: 50 IDs.
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
/// Get a list of the episodes saved in the current Spotify user's library.
/// This API endpoint is in beta and could change without warning.Please share any feedback that you have, or issues that you discover, in our developer community forum.
/// </summary>
[Route("/me/episodes", Verb.Get)]
public class GetSavedEpisodes : IReturn<PagableResponse<SavedEpisodesContextWrapper>>
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
/// Save one or more episodes to the current user's library.
/// This API endpoint is in beta and could change without warning.Please share any feedback that you have, or issues that you discover, in our developer community forum.
/// </summary>
[Route("/me/episodes", Verb.Put)]
public class SaveEpisodes : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs. Maximum: 50 IDs.
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// A maximum of 50 items can be specified in one request. Note: if the ids parameter is present in the query string, any IDs listed here in the body will be ignored.
    /// </summary>
    [BodyParameter]
    public List<string>? IdsBody { get; set; }
}

/// <summary>
/// Remove one or more episodes from the current user's library.
/// This API endpoint is in beta and could change without warning.Please share any feedback that you have, or issues that you discover, in our developer community forum.
/// </summary>
[Route("/me/episodes", Verb.Delete)]
public class RemoveSavedEpisodes : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs. Maximum: 50 IDs.
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// A maximum of 50 items can be specified in one request. Note: if the ids parameter is present in the query string, any IDs listed here in the body will be ignored.
    /// </summary>
    [BodyParameter]
    public List<string>? IdsBody { get; set; }
}

/// <summary>
/// Check if one or more episodes is already saved in the current Spotify user's 'Your Episodes' library.
/// This API endpoint is in beta and could change without warning.Please share any feedback that you have, or issues that you discover, in our developer community forum..
/// </summary>
[Route("/me/episodes/contains", Verb.Get)]
public class CheckSavedEpisodes : IReturn<List<bool>>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the episodes. Maximum: 50 IDs.
    /// </summary>
    public List<string>? Ids { get; set; }
}


/// <summary>
/// A wrapper for the context of an episode
/// </summary>
public class SavedEpisodesContextWrapper
{
    /// <summary>
    /// When an album was added
    /// </summary>
    public DateTime? AddedAt { get; set; }

    /// <summary>
    /// The associated Spotify album
    /// </summary>
    public Episode? Episode { get; set; }
}

/// <summary>
/// A wrapper for the <see cref="GetSeveralEpisodes"/> request
/// </summary>
public class SeveralEpisodes
{
    /// <summary>
    /// A set of episodes
    /// </summary>
    public List<Episode>? Episodes { get; set; }
}

public class Episode
{
    /// <summary>
    /// A URL to a 30 second preview (MP3 format) of the episode. null if not available.
    /// </summary>
    public string? AudioPreviewUrl { get; set; }

    /// <summary>
    /// A description of the episode. HTML tags are stripped away from this field, use html_description field in case HTML tags are needed.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// A description of the episode. This field may contain HTML tags.
    /// </summary>
    public string? HtmlDescription { get; set; }

    /// <summary>
    /// The episode length in milliseconds.
    /// </summary>
    public int? DurationMs { get; set; }

    /// <summary>
    /// Whether or not the episode has explicit content (true = yes it does; false = no it does not OR unknown).
    /// </summary>
    public bool? Explicit { get; set; }

    /// <summary>
    /// External URLs for this episode.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the episode.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify ID for the episode.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The cover art for the episode in various sizes, widest first.
    /// </summary>
    public List<Image>? Images { get; set; }

    /// <summary>
    /// True if the episode is hosted outside of Spotify's CDN.
    /// </summary>
    public bool? IsExternallyHosted { get; set; }

    /// <summary>
    /// True if the episode is playable in the given market. Otherwise false.
    /// </summary>
    public bool IsPlayable { get; set; }

    /// <summary>
    /// The language used in the episode, identified by a ISO 639 code. This field is deprecated and might be removed in the future. Please use the languages field instead.
    /// </summary>
    [Obsolete]
    public string? Language { get; set; }

    /// <summary>
    /// A list of the languages used in the episode, identified by their ISO 639-1 code.
    /// </summary>
    public List<string>? Languages { get; set; }

    /// <summary>
    /// The name of the episode.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The date the episode was first released, for example "1981-12-15". Depending on the precision, it might be shown as "1981" or "1981-12".
    /// </summary>
    public string? ReleaseDate { get; set; }

    /// <summary>
    /// The precision with which <see cref="ReleaseDate"/> value is known.
    /// Allowed values: year, month, day
    /// </summary>
    public string? ReleaseDatePrecision { get; set; }

    /// <summary>
    /// The user's most recent position in the episode. Set if the supplied access token is a user token and has the scope 'user-read-playback-position'.
    /// </summary>
    public ResumePoint? ResumePoint { get; set; }

    /// <summary>
    /// The object type.
    /// </summary>
    public ItemType Type { get; set; }

    /// <summary>
    /// The Spotify URI for the episode.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// Included in the response when a content restriction is applied. See Restriction Object for more details.
    /// </summary>
    public Restrictions? Restrictions { get; set; }

    public Show? Show { get; set; }
}

public class ResumePoint
{
    /// <summary>
    /// Whether or not the episode has been fully played by the user.
    /// </summary>
    public bool? FullyPlayed { get; set; }

    /// <summary>
    /// The user's most recent position in the episode in milliseconds.
    /// </summary>
    public int? ResumePositionMs { get; set; }
}