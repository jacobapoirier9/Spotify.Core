using Spotify.Core.Attributes;
using System.Net;

namespace Spotify.Core.Model;

/// <summary>
/// Get information about the user’s current playback state, including track or episode, progress, and active device.
/// </summary>
[Route("/me/player", Verb.Get)]
public class GetPlaybackState : IReturn<PlaybackState>
{
    /// <summary>
    /// A comma-separated list of item types that your client supports besides the default track type. Valid types are: <see cref="ItemType.Track"/> and <see cref="ItemType.Episode"/>.
    /// Note: This parameter was introduced to allow existing clients to maintain their current behaviour and might be deprecated in the future.
    /// In addition to providing this parameter, make sure that your client properly handles cases of new types in the future by checking against 
    /// the type field of each object.
    /// </summary>
    public List<ItemType>? AdditionalTypes { get; set; }

    /// <summary>
    /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only content that is available in that market will be returned.
    /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
    /// Note: If neither market or user country are provided, the content is considered unavailable for the client.
    /// Users can view the country that is associated with their account in the account settings.
    /// </summary>
    public string? Market { get; set; }
}

/// <summary>
/// Transfer playback to a new device and determine if it should start playing.
/// </summary>
[Route("/me/player", Verb.Put)]
public class TransferPlayback : IReturn<HttpStatusCode>
{
    [BodyParameter]
    public BodyObject? Body { get; set; }

    public class BodyObject
    {
        /// <summary>
        /// A JSON array containing the ID of the device on which playback should be started/transferred.
        /// For example:{device_ids:["74ASZWbe4lXaubB36ztrGX"]}
        /// Note: Although an array is accepted, only a single device_id is currently supported.Supplying more than one will return 400 Bad Request
        /// </summary>
        public List<string>? DeviceIds { get; set; }

        /// <summary>
        /// true: ensure playback happens on new device.
        /// false or not provided: keep the current playback state.
        /// </summary>
        public bool? Play { get; set; }
    }
}

/// <summary>
/// Get information about a user’s available devices.
/// </summary>
[Route("/me/player/devices", Verb.Get)]
public class GetAvailableDevices : IReturn<AvailableDevices>
{

}

/// <summary>
/// Get the object currently being played on the user's Spotify account.
/// </summary>
[Route("/me/player/currently-playing", Verb.Get)]
public class GetCurrentlyPlayingTrack : IReturn<PlaybackState>
{
    /// <summary>
    /// A comma-separated list of item types that your client supports besides the default track type. Valid types are: <see cref="ItemType.Track"/> and <see cref="ItemType.Episode"/>.
    /// Note: This parameter was introduced to allow existing clients to maintain their current behaviour and might be deprecated in the future.
    /// In addition to providing this parameter, make sure that your client properly handles cases of new types in the future by checking against the type field of each object.
    /// </summary>
    public List<ItemType>? AdditionalTypes { get; set; }

    /// <summary>
    /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only content that is available in that market will be returned.
    /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
    /// Note: If neither market or user country are provided, the content is considered unavailable for the client.
    /// Users can view the country that is associated with their account in the account settings.
    /// </summary>
    public string? Market { get; set; }
}

/// <summary>
/// Start a new context or resume current playback on the user's active device.
/// </summary>
[Route("/me/player/play", Verb.Put)]
public class StartPlayback : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The id of the device this command is targeting. If not supplied, the user's currently active device is the target.
    /// </summary>
    public string? DeviceId { get; set; }

    [BodyParameter]
    public BodyObject? Body { get; set; }

    public class BodyObject
    {
        /// <summary>
        /// Optional. Spotify URI of the context to play. Valid contexts are albums, artists & playlists. {context_uri:"spotify:album:1Je1IMUlBXcx1Fz0WE7oPT"}
        /// </summary>
        public string? ContextUri { get; set; }

        /// <summary>
        /// Optional. A JSON array of the Spotify track URIs to play. For example: {"uris": ["spotify:track:4iV5W9uYEdYUVa79Axb7Rh", "spotify:track:1301WleyT98MSxVHPZCA6M"]}
        /// </summary>
        public List<string>? Uris { get; set; }

        /// <summary>
        /// Optional. Indicates from where in the context playback should start. Only available when context_uri corresponds to an album or playlist 
        /// object "position" is zero based and can’t be negative. 
        /// Example: "offset": {"position": 5} "uri" is a string representing the uri of the item to start at. 
        /// Example: "offset": {"uri": "spotify:track:1301WleyT98MSxVHPZCA6M"}
        /// </summary>
        public Offset? Offset { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PositionMs { get; set; }
    }
}

/// <summary>
/// Pause playback on the user's account.
/// </summary>
[Route("/me/player/pause", Verb.Put)]
public class PausePlayback : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The id of the device this command is targeting. If not supplied, the user's currently active device is the target.
    /// </summary>
    public string? DeviceId { get; set; }
}

/// <summary>
/// Skips to next track in the user’s queue.
/// </summary>
[Route("/me/player/next", Verb.Post)]
public class SkipToNext : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The id of the device this command is targeting. If not supplied, the user's currently active device is the target.
    /// </summary>
    public string? DeviceId { get; set; }
}

/// <summary>
/// Skips to previous track in the user’s queue.
/// </summary>
[Route("/me/player/previous", Verb.Post)]
public class SkipToPrevious : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The id of the device this command is targeting. If not supplied, the user's currently active device is the target.
    /// </summary>
    public string? DeviceId { get; set; }
}

/// <summary>
/// Seeks to the given position in the user’s currently playing track.
/// </summary>
[Route("/me/player/seek", Verb.Put)]
public class SeekToPosition : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The id of the device this command is targeting. If not supplied, the user's currently active device is the target.
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// The position in milliseconds to seek to. Must be a positive number. Passing in a position that is greater than the 
    /// length of the track will cause the player to start playing the next song.
    /// </summary>
    public int? PositionMs { get; set; }
}

/// <summary>
/// Set the repeat mode for the user's playback. Options are repeat-track, repeat-context, and off.
/// </summary>
[Route("/me/player/repeat", Verb.Put)]
public class SetRepeatMode : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The id of the device this command is targeting. If not supplied, the user's currently active device is the target.
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// track, context or off.
    /// track will repeat the current track.
    /// context will repeat the current context.
    /// off will turn repeat off.
    /// </summary>
    [Obsolete("Set to enum")]
    public string? State { get; set; }
}

/// <summary>
/// Set the volume for the user’s current playback device.
/// </summary>
[Route("/me/player/volume", Verb.Put)]
public class SetPlaybackVolume : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The id of the device this command is targeting. If not supplied, the user's currently active device is the target.
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// The volume to set. Must be a value from 0 to 100 inclusive.
    /// </summary>
    public int? VolumePercent { get; set; }
}

/// <summary>
/// Toggle shuffle on or off for user’s playback.
/// </summary>
[Route("/me/player/shuffle", Verb.Put)]
public class SetPlaybackShuffle : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The id of the device this command is targeting. If not supplied, the user's currently active device is the target.
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// true : Shuffle user's playback.
    /// false : Do not shuffle user's playback.
    /// </summary>
    public bool? State { get; set; }
}

/// <summary>
/// Get tracks from the current user's recently played tracks. Note: Currently doesn't support podcast episodes.
/// </summary>
[Route("/me/player/recently-played", Verb.Get)]
public class GetRecentlyPlayedTracks : IReturn<PagableResponse<LastPlayedContext>>
{
    /// <summary>
    /// A Unix timestamp in milliseconds. Returns all items after (but not including) this cursor position. If after is specified, before must not be specified.
    /// </summary>
    public int? After { get; set; }

    /// <summary>
    /// A Unix timestamp in milliseconds. Returns all items before (but not including) this cursor position. If before is specified, after must not be specified.
    /// </summary>
    public int? Before { get; set; }

    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// </summary>
    public int? Limit { get; set; }
}

/// <summary>
/// Get the list of objects that make up the user's queue.
/// </summary>
[Route("/me/player/queue", Verb.Get)]
public class GetPlaybackQueue : IReturn<PlaybackQueue>
{
}

/// <summary>
/// Add an item to the end of the user's current playback queue.
/// </summary>
[Route("/me/player/queue", Verb.Post)]
public class AddItemToPlaybackQueue : IReturn<HttpStatusCode>
{
    /// <summary>
    /// The id of the device this command is targeting. If not supplied, the user's currently active device is the target.
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// The uri of the item to add to the queue. Must be a <see cref="ItemType.Track"/> or an <see cref="ItemType.Episode"/> uri.
    /// </summary>
    public string? Uri { get; set; }
}

public class PlaybackQueue
{
    public Track? CurrentlyPlaying { get; set; }

    public List<Track>? Queue { get; set; }
}

public class LastPlayedContext
{
    public Track? Track { get; set; }

    public DateTime? PlayedAt { get; set; }

    public object? Context { get; set; }
}

public class Offset
{
    /// <summary>
    /// Optional. Indicates from where in the context playback should start. Only available when context_uri corresponds to an album or playlist 
    /// object "position" is zero based and can’t be negative. 
    /// Example: "offset": {"position": 5} "uri" is a string representing the uri of the item to start at. 
    /// Example: "offset": {"uri": "spotify:track:1301WleyT98MSxVHPZCA6M"}
    /// </summary>
    public int? Position { get; set; }
}

public class AvailableDevices
{
    public List<Device>? Devices { get; set; }
}

public class PlaybackState
{
    /// <summary>
    /// The device that is currently active.
    /// </summary>
    public Device? Device { get; set; }

    /// <summary>
    /// off, track, context
    /// </summary>
    public string? RepeatState { get; set; }

    /// <summary>
    /// If shuffle is on or off.
    /// </summary>
    public bool? ShuffleState { get; set; }

    /// <summary>
    /// A Context Object. Can be null.
    /// </summary>
    public Context? Context { get; set; }

    /// <summary>
    /// Unix Millisecond Timestamp when data was fetched.
    /// </summary>
    public long? Timestamp { get; set; }

    /// <summary>
    /// Progress into the currently playing track or episode. Can be null.
    /// </summary>
    public int? ProgressMs { get; set; }

    /// <summary>
    /// If something is currently playing, return true.
    /// </summary>
    public bool? IsPlaying { get; set; }

    /// <summary>
    /// Currently playing item
    /// </summary>
    public Track? Item { get; set; }

    /// <summary>
    /// The object type of the currently playing item. Can be one of <see cref="ItemType.Track"/>, <see cref="ItemType.Episode"/>, ad or <see cref="ItemType.Unknown"/>.
    /// </summary>
    public ItemType? CurrentlyPlayingType { get; set; }

    /// <summary>
    /// Allows to update the user interface based on which playback actions are available within the current context.
    /// </summary>
    public Actions? Actions { get; set; }
}

[Obsolete("Need to figure out a consistancy of referencing this object")]
public class Actions
{
    /// <summary>
    /// Interrupting playback. Optional field.
    /// </summary>
    public bool? InterruptingPlayback { get; set; }

    /// <summary>
    /// Pausing. Optional field.
    /// </summary>
    public bool? Pausing { get; set; }

    /// <summary>
    /// Resuming. Optional field.
    /// </summary>
    public bool? Resuming { get; set; }

    /// <summary>
    /// Seeking playback location. Optional field.
    /// </summary>
    public bool? Seeking { get; set; }

    /// <summary>
    /// Skipping to the next context. Optional field.
    /// </summary>
    public bool? SkippingNext { get; set; }

    /// <summary>
    /// Skipping to the previous context. Optional field.
    /// </summary>
    public bool? SkippingPrev { get; set; }

    /// <summary>
    /// Toggling repeat context flag. Optional field.
    /// </summary>
    public bool? TogglingRepeatContext { get; set; }

    /// <summary>
    /// Toggling shuffle flag. Optional field.
    /// </summary>
    public bool? TogglingShuffle { get; set; }

    /// <summary>
    /// Toggling repeat track flag. Optional field.
    /// </summary>
    public bool? TogglingRepeatTrack { get; set; }

    /// <summary>
    /// Transfering playback between devices. Optional field.
    /// </summary>
    public bool? TransferringPlayback { get; set; }
}

public class Context
{
    /// <summary>
    /// The object type, e.g. "artist", "playlist", "album", "show".
    /// </summary>
    public ItemType? Type { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the track.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// External URLs for this context.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// The Spotify URI for the context.
    /// </summary>
    public string? Uri { get; set; }
}

public class Device
{
    /// <summary>
    /// The device ID.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// If this device is the currently active device.
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// If this device is currently in a private session.
    /// </summary>
    public bool? IsPrivateSession { get; set; }

    /// <summary>
    /// Whether controlling this device is restricted. At present if this is "true" then no Web API commands will be accepted by this device.
    /// </summary>
    public bool? IsRestricted { get; set; }

    /// <summary>
    /// A human-readable name for the device. Some devices have a name that the user can configure (e.g. "Loudest speaker") 
    /// and some devices have a generic name associated with the manufacturer or device model.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Device type, such as "computer", "smartphone" or "speaker".
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The current volume in percent.
    /// </summary>
    public int? VolumePercent { get; set; }
}

public class Player
{
}
