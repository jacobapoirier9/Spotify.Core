using Spotify.Core.Attributes;
using System.Net;

namespace Spotify.Core.Model;

/// <summary>
/// Get detailed profile information about the current user (including the current user's username).
/// </summary>
[Route($"{Configuration.ApiUri}/me", Verb.Get)]
public class GetCurrentUserProfile : IReturn<UserProfile>
{

}

/// <summary>
/// Get the current user's top artists or tracks based on calculated affinity.
/// </summary>
/// <typeparam name="T">The type of content to return</typeparam>
[Route($"{Configuration.ApiUri}/me/top/{{{nameof(Type)}}}", Verb.Get)]
public class GetUsersTopItems<T> : IReturn<Pagable<T>>
{
    /// <summary>
    /// The type of entity to return. Valid values: artists or tracks
    /// Default to <see cref="T"/>s
    /// </summary>
    public string? Type => Configuration.JsonNamingPolicy.ConvertName(typeof(T).Name) + "s";

    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// The index of the first item to return. Default: 0 (the first item). Use with limit to get the next set of items.
    /// </summary>
    public int? Offset { get; set; }

    /// <summary>
    /// Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including 
    /// all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term
    /// </summary>
    public string? TimeRange { get; set; }
}

/// <summary>
/// Get public profile information about a Spotify user.
/// </summary>
[Route($"{Configuration.ApiUri}/users/{{{nameof(UserId)}}}", Verb.Get)]
public class GetUsersProfile : IReturn<UserProfile>
{
    public string? UserId { get; set; }
}

/// <summary>
/// Add the current user as a follower of a playlist.
/// </summary>
[Route($"{Configuration.ApiUri}/playlists/{{{nameof(PlaylistId)}}}/followers", Verb.Put)]
public class FollowPlaylist : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// Defaults to true. If true the playlist will be included in user's public playlists, if false it will remain private.
    /// </summary>
    [Body]
    public bool? Public { get; set; }
}

/// <summary>
/// Remove the current user as a follower of a playlist.
/// </summary>
[Route($"{Configuration.ApiUri}/playlists/{{{nameof(PlaylistId)}}}/followers", Verb.Delete)]
public class UnfollowPlaylist : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }
}

/// <summary>
/// Get the current user's followed artists.
/// </summary>
[Route($"{Configuration.ApiUri}/me/following", Verb.Get)]
public class GetFollowedArtists : IReturn<FollowedArtists>
{
    /// <summary>
    /// The ID type: currently only artist is supported.
    /// </summary>
    public ItemType? Type => ItemType.Artist;

    /// <summary>
    /// The last artist ID retrieved from the previous request.
    /// </summary>
    public string? After { get; set; }

    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// </summary>
    public int? Limit { get; set; }
}

/// <summary>
/// Add the current user as a follower of one or more artists or other Spotify users.
/// </summary>
[Route($"{Configuration.ApiUri}/me/following", Verb.Put)]
public class FollowArtistsOrUsers : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the artist or the user Spotify IDs. A maximum of 50 IDs can be sent in one request.
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// The ID type. Allowed values: <see cref="ItemType.Artist"/>, <see cref="ItemType.User"/>
    /// </summary>
    public ItemType? Type { get; set; }

    /// <summary>
    /// A JSON array of the artist or user Spotify IDs. For example: {ids:["74ASZWbe4lXaubB36ztrGX", "08td7MxkoHQkXnWAYD8d6Q"]}. 
    /// A maximum of 50 IDs can be sent in one request. Note: if the ids parameter is present in the query string, any IDs listed here in the body will be ignored.
    /// Allowed values: <see cref="ItemType.Artist"/>, <see cref="ItemType.User"/>
    /// This is written to the body.
    /// </summary>
    [Body(Alias = nameof(Ids))]
    public List<string>? IdsBody { get; set; }
}

/// <summary>
/// Remove the current user as a follower of one or more artists or other Spotify users.
/// </summary>
[Route($"{Configuration.ApiUri}/me/following", Verb.Delete)]
public class UnfollowArtistsOrUsers : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the artist or the user Spotify IDs. A maximum of 50 IDs can be sent in one request.
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// The ID type. Allowed values: <see cref="ItemType.Artist"/>, <see cref="ItemType.User"/>
    /// </summary>
    public ItemType? Type { get; set; }

    /// <summary>
    /// A JSON array of the artist or user Spotify IDs. For example: {ids:["74ASZWbe4lXaubB36ztrGX", "08td7MxkoHQkXnWAYD8d6Q"]}. 
    /// A maximum of 50 IDs can be sent in one request. Note: if the ids parameter is present in the query string, any IDs listed here in the body will be ignored.
    /// Allowed values: <see cref="ItemType.Artist"/>, <see cref="ItemType.User"/>
    /// This is written to the body.
    /// </summary>
    [Body(Alias = nameof(Ids))]
    public List<string>? IdsBody { get; set; }
}

/// <summary>
/// Check to see if the current user is following one or more artists or other Spotify users.
/// </summary>
[Route($"{Configuration.ApiUri}/me/following/contains", Verb.Get)]
public class CheckIfUserFollowsArtistsOrUsers : IReturn<List<bool>>
{
    /// <summary>
    /// A comma-separated list of the artist or the user Spotify IDs. A maximum of 50 IDs can be sent in one request.
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// The ID type.Allowed values: <see cref="ItemType.Artist"/>, <see cref="ItemType.User"/>
    /// </summary>
    public ItemType? Type { get; set; }
}

/// <summary>
/// Check to see if one or more Spotify users are following a specified playlist.
/// </summary>
[Route($"{Configuration.ApiUri}/playlists/{{{nameof(PlaylistId)}}}/followers/contains", Verb.Get)]
public class CheckIfUsersFollowPlaylist : IReturn<List<bool>>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// A comma-separated list of Spotify User IDs ; the ids of the users that you want to check to see if they follow the playlist. Maximum: 5 ids.
    /// </summary>
    public List<string>? Ids { get; set; }
}

public class FollowedArtists
{
    /// <summary>
    /// A paged set of artists
    /// </summary>
    public Pagable<Artist>? Artists { get; set; }
}

public class UserProfile
{
    /// <summary>
    /// The country of the user, as set in the user's account profile. An ISO 3166-1 alpha-2 country code. 
    /// This field is only available when the current user has granted access to the user-read-private scope.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// The name displayed on the user's profile. null if not available.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The user's email address, as entered by the user when creating their account. 
    /// Important! This email address is unverified; there is no proof that it actually belongs to the user. 
    /// This field is only available when the current user has granted access to the user-read-email scope.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The user's explicit content settings. This field is only available when the current user has granted access to the user-read-private scope.
    /// </summary>
    public ExplicitContentSettings? ExplicitContent { get; set; }

    /// <summary>
    /// Known external URLs for this user.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// Information about the followers of the user.
    /// </summary>
    public Followers? Followers { get; set; }

    /// <summary>
    /// A link to the Web API endpoint for this user.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify user ID for the user.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The user's profile image.
    /// </summary>
    public List<Image>? Images { get; set; }

    /// <summary>
    /// The user's Spotify subscription level: "premium", "free", etc. (The subscription level "open" can be considered the same as "free".) 
    /// This field is only available when the current user has granted access to the user-read-private scope.
    /// </summary>
    public string? Product { get; set; }

    /// <summary>
    /// The object type: <see cref="ItemType.User"/>
    /// </summary>
    public ItemType? Type { get; set; }

    /// <summary>
    /// The Spotify URI for the user.
    /// </summary>
    public string? Uri { get; set; }
}

public class ExplicitContentSettings
{
    /// <summary>
    /// When true, indicates that explicit content should not be played.
    /// </summary>
    public bool? FilterEnabled { get; set; }

    /// <summary>
    /// When true, indicates that the explicit content setting is locked and can't be changed by the user.
    /// </summary>
    public bool? FilterLocked { get; set; }
}