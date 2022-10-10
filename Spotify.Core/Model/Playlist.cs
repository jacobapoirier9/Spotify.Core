using Microsoft.VisualBasic;
using Spotify.Core.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spotify.Core.Model;

/// <summary>
/// Get a playlist owned by a Spotify user.
/// </summary>
[Route($"{Configuration.ApiUri}/playlists/{{{nameof(PlaylistId)}}}", Verb.Get)]
[Obsolete($"This needs to be able to handle tracks and episodes in the response")]
public class GetPlaylist : IReturn<Playlist>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// A comma-separated list of item types that your client supports besides the default track type. Valid types are: <see cref="ItemType.Track"/> and <see cref="ItemType.Episode"/>.
    /// Note: This parameter was introduced to allow existing clients to maintain their current behaviour and might be deprecated in the future.
    /// In addition to providing this parameter, make sure that your client properly handles cases of new types in the future by checking against the type field of each object.
    /// </summary>
    public List<ItemType>? AdditionalTypes { get; set; }

    /// <summary>
    /// Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. 
    /// For example, to get just the playlist's description and URI: fields=description,uri. A dot separator can be used to 
    /// specify non-reoccurring fields, while parentheses can be used to specify reoccurring fields within objects. 
    /// For example, to get just the added date and user ID of the adder: fields=tracks.items(added_at,added_by.id). 
    /// Use multiple parentheses to drill down into nested objects, for example: fields=tracks.items(track(name,href,album(name,href))). 
    /// Fields can be excluded by prefixing them with an exclamation mark, for example: fields=tracks.items(track(name,href,album(!name,href)))
    /// </summary>
    public string? Fields { get; set; }

    /// <summary>
    /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only content that is available in that market will be returned.
    /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
    /// Note: If neither market or user country are provided, the content is considered unavailable for the client.
    /// Users can view the country that is associated with their account in the account settings.
    /// </summary>
    public string? Market { get; set; }
}

/// <summary>
/// Change a playlist's name and public/private state. (The user must, of course, own the playlist.)
/// </summary>
[Route($"{Configuration.ApiUri}/playlists/{{{nameof(PlaylistId)}}}", Verb.Put)]
public class ChangePlaylistDetails : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// The new name for the playlist, for example "My New Playlist Title"
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// If true the playlist will be public, if false it will be private.
    /// </summary>
    public bool? Public { get; set; }

    /// <summary>
    /// If true, the playlist will become collaborative and other users will be able to modify the playlist in their Spotify client.
    /// Note: You can only set collaborative to true on non-public playlists.
    /// </summary>
    public bool? Collaborative { get; set; }

    /// <summary>
    /// Value for playlist description as displayed in Spotify Clients and in the Web API.
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Get full details of the items of a playlist owned by a Spotify user.
/// </summary>
[Route($"{Configuration.ApiUri}/playlists/{{{nameof(PlaylistId)}}}/tracks", Verb.Get)]
[Obsolete($"This needs to be able to handle tracks and episodes in the response")]
public class GetPlaylistItems : IReturn<Pagable<Track>>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// A comma-separated list of item types that your client supports besides the default track type. Valid types are: <see cref="ItemType.Track"/> and <see cref="ItemType.Episode"/>.
    /// Note: This parameter was introduced to allow existing clients to maintain their current behaviour and might be deprecated in the future.
    /// In addition to providing this parameter, make sure that your client properly handles cases of new types in the future by checking against the type field of each object.
    /// </summary>
    public List<ItemType>? AdditionalTypes { get; set; }

    /// <summary>
    /// Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. 
    /// For example, to get just the playlist's description and URI: fields=description,uri. A dot separator can be used to 
    /// specify non-reoccurring fields, while parentheses can be used to specify reoccurring fields within objects. 
    /// For example, to get just the added date and user ID of the adder: fields=tracks.items(added_at,added_by.id). 
    /// Use multiple parentheses to drill down into nested objects, for example: fields=tracks.items(track(name,href,album(name,href))). 
    /// Fields can be excluded by prefixing them with an exclamation mark, for example: fields=tracks.items(track(name,href,album(!name,href)))
    /// </summary>
    public string? Fields { get; set; }

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
/// The Spotify ID of the playlist.
/// </summary>
[Route($"{Configuration.ApiUri}/playlists/{{{nameof(PlaylistId)}}}/tracks", Verb.Post)]
public class AddItemsToPlaylist : IReturn<Snapshot>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// The position to insert the items, a zero-based index. For example, to insert the items in the first position: position=0; 
    /// to insert the items in the third position: position=2. If omitted, the items will be appended to the playlist. 
    /// Items are added in the order they are listed in the query string or request body.
    /// </summary>
    public int? Position { get; set; }

    /// <summary>
    /// A comma-separated list of Spotify URIs to add, can be track or episode URIs. For example:
    /// uris=spotify:track:4iV5W9uYEdYUVa79Axb7Rh, spotify:track:1301WleyT98MSxVHPZCA6M, spotify:episode:512ojhOuo1ktJprKbVcKyQ
    /// A maximum of 100 items can be added in one request.
    /// Note: it is likely that passing a large number of item URIs as a query parameter will exceed the maximum length of the request URI. When adding a large number of items, it is recommended to pass them in the request body, see below.
    /// </summary>
    public List<string>? Uris { get; set; }

    [BodyParameter]
    public BodyObject? Body { get; set; }

    public class BodyObject
    {
        /// <summary>
        /// A JSON array of the Spotify URIs to add. 
        /// For example: {"uris": ["spotify:track:4iV5W9uYEdYUVa79Axb7Rh","spotify:track:1301WleyT98MSxVHPZCA6M", "spotify:episode:512ojhOuo1ktJprKbVcKyQ"]}
        /// </summary>
        public List<string>? Uris { get; set; }

        /// <summary>
        /// The position to insert the items, a zero-based index. For example, to insert the items in the first position: position=0 ; 
        /// to insert the items in the third position: position=2. If omitted, the items will be appended to the playlist. 
        /// Items are added in the order they appear in the uris array. For example: {"uris": ["spotify:track:4iV5W9uYEdYUVa79Axb7Rh","spotify:track:1301WleyT98MSxVHPZCA6M"], "position": 3}
        /// </summary>
        public int? Position { get; set; }
    }
}

/// <summary>
/// Either reorder or replace items in a playlist depending on the request's parameters. 
/// To reorder items, include range_start, insert_before, range_length and snapshot_id in the request's body. 
/// To replace items, include uris as either a query parameter or in the request's body. Replacing items in a playlist will overwrite its existing items. 
/// This operation can be used for replacing or clearing items in a playlist.
/// 
/// Note: Replace and reorder are mutually exclusive operations which share the same endpoint, 
/// but have different parameters.These operations can't be applied together in a single request.
/// </summary>
[Route($"{Configuration.ApiUri}/playlists/{{{nameof(PlaylistId)}}}/tracks", Verb.Put)]
public class UpdatePlaylistItems : IReturn<Snapshot>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }

    /// <summary>
    /// A comma-separated list of Spotify URIs to set, can be track or episode URIs. For example: uris=spotify:track:4iV5W9uYEdYUVa79Axb7Rh,spotify:track:1301WleyT98MSxVHPZCA6M,spotify:episode:512ojhOuo1ktJprKbVcKyQ
    /// A maximum of 100 items can be set in one request.
    /// </summary>
    public List<string>? Uris { get; set; }

    [BodyParameter]
    public BodyObject? Body { get; set; }

    public class BodyObject
    {
        /// <summary>
        /// A comma-separated list of Spotify URIs to set, can be track or episode URIs. For example: uris=spotify:track:4iV5W9uYEdYUVa79Axb7Rh,spotify:track:1301WleyT98MSxVHPZCA6M,spotify:episode:512ojhOuo1ktJprKbVcKyQ
        /// A maximum of 100 items can be set in one request.
        /// </summary>
        public List<string>? Uris { get; set; }

        /// <summary>
        /// The position of the first item to be reordered.
        /// </summary>
        public int? RangeStart { get; set; }

        /// <summary>
        /// The position where the items should be inserted.
        /// To reorder the items to the end of the playlist, simply set insert_before to the position after the last item.
        /// Examples:
        /// To reorder the first item to the last position in a playlist with 10 items, set range_start to 0, and insert_before to 10.
        /// To reorder the last item in a playlist with 10 items to the start of the playlist, set range_start to 9, and insert_before to 0.
        /// </summary>
        public int? InsertBefore { get; set; }

        /// <summary>
        /// The amount of items to be reordered. Defaults to 1 if not set.
        /// The range of items to be reordered begins from the range_start position, and includes the range_length subsequent items.
        /// Example:
        /// To move the items at index 9-10 to the start of the playlist, range_start is set to 9, and range_length is set to 2.
        /// </summary>
        public int? RangeLength { get; set; }

        /// <summary>
        /// The playlist's snapshot ID against which you want to make the changes.
        /// </summary>
        public string? SnapshotId { get; set; }
    }
}

/// <summary>
/// Remove one or more items from a user's playlist.
/// </summary>
[Route($"{Configuration.ApiUri}/playlists/{{{nameof(PlaylistId)}}}/tracks", Verb.Delete)]
public class RemovePlaylistItems : IReturn<Snapshot>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }

    [BodyParameter]
    public BodyObject? Body { get; set; }

    public class BodyObject
    {
        /// <summary>
        /// An array of objects containing Spotify URIs of the tracks or episodes to remove. 
        /// For example: { "tracks": [{ "uri": "spotify:track:4iV5W9uYEdYUVa79Axb7Rh" },{ "uri": "spotify:track:1301WleyT98MSxVHPZCA6M" }] }. 
        /// A maximum of 100 objects can be sent at once.
        /// </summary>
        public List<Track>? Tracks { get; set; }

        /// <summary>
        /// The playlist's snapshot ID against which you want to make the changes. The API will validate that the specified items exist and in the 
        /// specified positions and make the changes, even if more recent changes have been made to the playlist.
        /// </summary>
        public string? SnapshotId { get; set; }
    }
}

/// <summary>
/// Get a list of the playlists owned or followed by the current Spotify user.
/// </summary>
[Route($"{Configuration.ApiUri}/me/playlists", Verb.Get)]
public class GetCurrentUsersPlaylists : IReturn<Pagable<Playlist>>
{
    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// The index of the first playlist to return. Default: 0 (the first object). Maximum offset: 100.000. Use with limit to get the next set of playlists.'
    /// </summary>
    public int? Offset { get; set; }
}

/// <summary>
/// Get a list of the playlists owned or followed by a Spotify user.
/// </summary>
[Route($"{Configuration.ApiUri}/users/{{{nameof(UserId)}}}/playlists", Verb.Get)]
public class GetUsersPlaylist : IReturn<Pagable<Playlist>>
{
    /// <summary>
    /// The user's Spotify user ID.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// The index of the first playlist to return. Default: 0 (the first object). Maximum offset: 100.000. Use with limit to get the next set of playlists.
    /// </summary>
    public int? Offset { get; set; }
}

/// <summary>
/// Create a playlist for a Spotify user. (The playlist will be empty until you add tracks.)
/// </summary>
[Route($"{Configuration.ApiUri}/users/{{{nameof(UserId)}}}/playlists", Verb.Post)]
public class CreatePlaylist : IReturn<Playlist>
{
    /// <summary>
    /// The user's Spotify user ID.
    /// </summary>
    public string? UserId { get; set; }

    [BodyParameter]
    public BodyObject? Body { get; set; }

    public class BodyObject
    {
        /// <summary>
        /// The name for the new playlist, for example "Your Coolest Playlist". This name does not need to be unique; a user may have several playlists with the same name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Defaults to true. If true the playlist will be public, if false it will be private. To be able to create private playlists, the user must have granted the playlist-modify-private scope
        /// </summary>
        public bool? Public { get; set; }

        /// <summary>
        /// Defaults to false. If true the playlist will be collaborative. Note: to create a collaborative playlist you must also set public to false. 
        /// To create collaborative playlists you must have granted playlist-modify-private and playlist-modify-public scopes.
        /// </summary>
        public bool? Collaborative { get; set; }

        /// <summary>
        /// value for playlist description as displayed in Spotify Clients and in the Web API.
        /// </summary>
        public string? Description { get; set; }
    }
}

/// <summary>
/// Get a list of Spotify featured playlists (shown, for example, on a Spotify player's 'Browse' tab).
/// </summary>
[Route("/browse/featured-playlists", Verb.Get)]
public class GetFeaturedPlaylists : IReturn<FeaturedPlaylists>
{
    /// <summary>
    /// A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want the list of returned items to be relevant to a particular country. If omitted, the returned items will be relevant to all countries.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// The desired language, consisting of a lowercase ISO 639-1 language code and an uppercase ISO 3166-1 alpha-2 country code, joined by an underscore. For example: es_MX, meaning "Spanish (Mexico)". Provide this parameter if you want the results returned in a particular language (where available).
    /// Note: if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default language(American English). The locale parameter, combined with the country parameter, may give odd results if not carefully matched.For example country=SE&locale=de_DE will return a list of categories relevant to Sweden but as German language strings.
    /// </summary>
    public string? Locale { get; set; }

    /// <summary>
    /// The index of the first item to return. Default: 0 (the first item). Use with limit to get the next set of items.
    /// </summary>
    public int? Offset { get; set; }

    /// <summary>
    /// A timestamp in ISO 8601 format: yyyy-MM-ddTHH:mm:ss. Use this parameter to specify the user's local time to get results tailored for that specific date and time in the day. If not provided, the response defaults to the current UTC time. Example: "2014-10-23T09:00:00" for a user whose local time is 9AM. If there were no featured playlists (or there is no data) at the specified time, the response will revert to the current UTC time.
    /// Example value: "2014-10-23T09:00:00"
    /// </summary>
    public string? Timestamp { get; set; }
}

/// <summary>
/// Get a list of Spotify playlists tagged with a particular category.
/// </summary>
[Route($"/browse/categories/{{{nameof(CategoryId)}}}/playlists", Verb.Get)]
public class GetCategorysPlaylists : IReturn<CategoryPlaylists>
{
    /// <summary>
    /// The Spotify category ID for the category.
    /// </summary>
    public string? CategoryId { get; set; }

    /// <summary>
    /// A country: an ISO 3166-1 alpha-2 country code. Provide this parameter to ensure that the category exists for a particular country.
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
/// Get the current image associated with a specific playlist.
/// </summary>
[Route($"/playlists/{{{nameof(PlaylistId)}}}/images", Verb.Get)]
public class GetPlaylistCoverImage : IReturn<List<Image>>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }
}

/// <summary>
/// Replace the image used to represent a specific playlist.
/// </summary>
[Route($"/playlists/{{{nameof(PlaylistId)}}}/images", Verb.Put)]
[Obsolete("How do you add the image?")]
public class AddCustomPlaylistCoverImage : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The Spotify ID of the playlist.
    /// </summary>
    public string? PlaylistId { get; set; }
}

public class CategoryPlaylists
{
    public Pagable<Playlist>? Playlists { get; set; }
}

public class FeaturedPlaylists
{
    public string? Message { get; set; }

    public Pagable<Playlist>? Playlists { get; set; }
}

public class Snapshot
{
    /// <summary>
    /// A snapshot ID for the playlist
    /// </summary>
    public string? SnapshotId { get; set; }
}

public class Playlist
{
    /// <summary>
    /// true if the owner allows other users to modify the playlist.
    /// </summary>
    public bool? Collaborative { get; set; }

    /// <summary>
    /// The playlist description. Only returned for modified, verified playlists, otherwise null.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Known external URLs for this playlist.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// Information about the followers of the playlist.
    /// </summary>
    public Followers? Followers { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the playlist.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify ID for the playlist.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Images for the playlist. The array may be empty or contain up to three images. The images are returned by size in descending order. 
    /// See Working with Playlists. Note: If returned, the source URL for the image (url) is temporary and will expire in less than a day.
    /// </summary>
    public List<Image>? Images { get; set; }

    /// <summary>
    /// The name of the playlist.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The user who owns the playlist
    /// </summary>
    public UserProfile? Owner { get; set; }

    /// <summary>
    /// The playlist's public/private status: true the playlist is public, false the playlist is private, null the playlist status is not relevant. 
    /// For more about public/private status, see Working with Playlists
    /// </summary>
    public bool? Public { get; set; }

    /// <summary>
    /// The version identifier for the current playlist. Can be supplied in other requests to target a specific playlist version
    /// </summary>
    public string? SnapshotId { get; set; }

    /// <summary>
    /// The tracks of the playlist.
    /// </summary>
    public Pagable<Track>? Tracks { get; set; }

    /// <summary>
    /// The object type: <see cref="ItemType.Playlist"/>
    /// </summary>
    public ItemType? Type { get; set; }

    /// <summary>
    /// The Spotify URI for the playlist.
    /// </summary>
    public string? Uri { get; set; }
}
