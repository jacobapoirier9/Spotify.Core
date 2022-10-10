using Spotify.Core.Attributes;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Principal;
using static System.Net.Mime.MediaTypeNames;

namespace Spotify.Core.Model;

[Route($"{Configuration.ApiUri}/tracks/{{{nameof(Id)}}}", Verb.Get)]
public class GetTrack : IReturn<Track>
{
    /// <summary>
    /// TThe Spotify ID for the track.
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
/// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
/// </summary>
[Route($"{Configuration.ApiUri}/tracks", Verb.Get)]
public class GetSeveralTracks : IReturn<SeveralTracks>
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
/// Get a list of the songs saved in the current Spotify user's 'Your Music' library.
/// </summary>
[Route($"{Configuration.ApiUri}/me/tracks", Verb.Get)]
public class GetSavedTracks : IReturn<Pagable<SavedTrackContextWrapper>>
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
/// Save one or more tracks to the current user's 'Your Music' library.
/// </summary>
[Route($"{Configuration.ApiUri}/me/tracks", Verb.Put)]
public class SaveTracks : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the albums. Maximum: 20 IDs.
    /// This will be written to the URL
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// A maximum of 50 items can be specified in one request. Note: if the ids parameter is present in the query string, any IDs listed here in the body will be ignored.
    /// </summary>
    [BodyParameter2(true)]
    public List<string>? IdsBody { get; set; }
}

/// <summary>
/// Remove one or more tracks from the current user's 'Your Music' library.
/// </summary>
[Route($"{Configuration.ApiUri}/me/tracks", Verb.Delete)]
public class RemoveSavedTracks : IReturn<HttpStatusCode>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the albums. Maximum: 20 IDs.
    /// This will be written to the URL
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// A maximum of 50 items can be specified in one request. Note: if the ids parameter is present in the query string, any IDs listed here in the body will be ignored.
    /// </summary>
    [BodyParameter2]
    public List<string>? IdsBody { get; set; }
}

/// <summary>
/// Check if one or more tracks is already saved in the current Spotify user's 'Your Music' library.
/// </summary>
[Route($"{Configuration.ApiUri}/me/tracks/constains", Verb.Get)]
public class CheckSavedTracks : IReturn<List<bool>>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs for the albums. Maximum: 20 IDs.
    /// This will be written to the URL
    /// </summary>
    public List<string>? Ids { get; set; }
}

/// <summary>
/// Recommendations are generated based on the available information for a given seed entity and matched against similar artists and tracks.
/// If there is sufficient information about the provided seeds, a list of tracks will be returned together with pool size details.
/// For artists and tracks that are very new or obscure there might not be enough data to generate a list of tracks.
/// </summary>
[Route($"{Configuration.ApiUri}/recommendations", Verb.Get)]
public class GetRecommendations : IReturn<Recommendations>
{
    /// <summary>
    /// A comma separated list of Spotify IDs for seed artists. 
    /// Up to 5 seed values may be provided in any combination of <see cref="SeedArtists"/>, <see cref="SeedTracks"/>, and <see cref="SeedGenres"/>,.
    /// </summary>
    public List<string>? SeedArtists { get; set; }

    /// <summary>
    /// A comma separated list of Spotify IDs for seed genres. 
    /// Up to 5 seed values may be provided in any combination of <see cref="SeedArtists"/>, <see cref="SeedTracks"/>, and <see cref="SeedGenres"/>,.
    /// </summary>
    public List<string>? SeedGenres { get; set; }

    /// <summary>
    /// A comma separated list of Spotify IDs for seed tracks. 
    /// Up to 5 seed values may be provided in any combination of <see cref="SeedArtists"/>, <see cref="SeedTracks"/>, and <see cref="SeedGenres"/>,.
    /// </summary>
    public List<string>? SeedTracks { get; set; }

    /// <summary>
    /// The target size of the list of recommended tracks. For seeds with unusually small pools or when highly restrictive filtering is applied, 
    /// it may be impossible to generate the requested number of recommended tracks. Debugging information for such cases is available in the response. 
    /// Default: 20. Minimum: 1. Maximum: 100.
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
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxAccousticness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxDanceability { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxDurationMs { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxEnergy { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxInstrumentalness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxKey { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxLiveness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxLoudness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxMode { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxPopularity { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxSpeechiness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxTempo { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxTimeSignature { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard ceiling on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, max_instrumentalness=0.35 would filter out most tracks that are likely to be instrumental.
    /// </summary>
    public int? MaxValence { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for 
    /// the list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinAccousticness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinDanceability { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinDurationMs { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for 
    /// the list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinEnergy { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinInstrumentalness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinKey { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinLiveness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinLoudness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinMode { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinPopularity { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinSpeechiness { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinTempo { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinTimeSignature { get; set; }

    /// <summary>
    /// For each tunable track attribute, a hard floor on the selected track attribute’s value can be provided. See tunable track attributes below for the 
    /// list of available options. For example, min_tempo=140 would restrict results to only those tracks with a tempo of greater than 140 beats per minute.
    /// </summary>
    public int? MinValence { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetAccousticness { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetDanceability { get; set; }

    /// <summary>
    /// Target duration of the track (ms)
    /// </summary>
    public int? TargetDurationMs { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetEnergy { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetInstrumentalness { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetKey { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetLiveness { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetLoudness { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetMode { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetPopularity { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred.
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetSpeechiness { get; set; }

    /// <summary>
    /// Target tempo (BPM)
    /// </summary>
    public int? TargetTempo { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred. 
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetTimeSignature { get; set; }

    /// <summary>
    /// For each of the tunable track attributes (below) a target value may be provided. Tracks with the attribute values nearest to the target values will be preferred.
    /// For example, you might request target_energy=0.6 and target_danceability=0.8. All target values will be weighed equally in ranking results.
    /// </summary>
    public int? TargetValence { get; set; }
}

/// <summary>
/// Get audio features for multiple tracks based on their Spotify IDs.
/// </summary>
[Obsolete($"This may be an expensive call, {nameof(GetTrackAudioFeatures)} should be used instead.")]
[Route($"{Configuration.ApiUri}/audio-features", Verb.Get)]
public class GetSeveralTrackAudioFeatures : IReturn<SeveralAudioFeatures>
{

}

[Route($"{Configuration.ApiUri}/audio-features/{{{nameof(Id)}}}", Verb.Get)]
public class GetTrackAudioFeatures : IReturn<AudioFeatures>
{
    /// <summary>
    /// TThe Spotify ID for the track.
    /// </summary>
    public string? Id { get; set; }
}

public class Track
{
    /// <summary>
    /// The album on which the track appears. The album object includes a link in href to full information about the album.
    /// </summary>
    public Album? Album { get; set; }

    /// <summary>
    /// The artists who performed the track. Each artist object includes a link in href to more detailed information about the artist.
    /// </summary>
    public List<Artist>? Artists { get; set; }

    /// <summary>
    /// A list of the countries in which the track can be played, identified by their ISO 3166-1 alpha-2 code.
    /// </summary>
    public List<string>? AvailableMarkets { get; set; }

    /// <summary>
    /// The disc number (usually 1 unless the album consists of more than one disc).
    /// </summary>
    public int? DiscNumber { get; set; }

    /// <summary>
    /// The track length in milliseconds.
    /// </summary>
    public long? DurationMs { get; set; }

    /// <summary>
    /// Whether or not the track has explicit lyrics ( true = yes it does; false = no it does not OR unknown).
    /// </summary>
    public bool? Explicit { get; set; }

    /// <summary>
    /// Known external IDs for the track.
    /// </summary>
    public ExternalIds? ExternalIds { get; set; }

    /// <summary>
    /// Known external URLs for this track.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the track.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify ID for the track.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Whether or not the track is from a local file.
    /// </summary>
    public bool? IsLocal { get; set; }

    /// <summary>
    /// Part of the response when Track Relinking is applied. If true, the track is playable in the given market. Otherwise false.
    /// </summary>
    public bool? IsPlayable { get; set; }

    [Obsolete("Unkown")]
    public Track? LinkedFrom => throw new NotImplementedException();

    /// <summary>
    /// Included in the response when a content restriction is applied. See Restriction Object for more details.
    /// </summary>
    public Restrictions? Restrictions { get; set; }

    /// <summary>
    /// The name of the track.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The popularity of the track. The value will be between 0 and 100, with 100 being the most popular.
    /// The popularity of a track is a value between 0 and 100, with 100 being the most popular.
    /// The popularity is calculated by algorithm and is based, in the most part, on the total number of plays the track has had and how recent those plays are.
    /// Generally speaking, songs that are being played a lot now will have a higher popularity than songs that were played a lot in the past. 
    /// Duplicate tracks (e.g.the same track from a single and an album) are rated independently.Artist and album popularity is derived mathematically 
    /// from track popularity.Note: the popularity value may lag actual popularity by a few days: the value is not updated in real time.
    /// </summary>
    public int? Popularity { get; set; }

    /// <summary>
    /// A link to a 30 second preview (MP3 format) of the track. Can be null
    /// </summary>
    public string? PreviewUrl { get; set; }

    /// <summary>
    /// The number of the track. If an album has several discs, the track number is the number on the specified disc.
    /// </summary>
    public int? TrackNumber { get; set; }

    /// <summary>
    /// The object type: "track".
    /// </summary>
    public ItemType Type { get; set; }

    /// <summary>
    /// The Spotify URI for the track.
    /// </summary>
    public string? Uri { get; set; }
}

public class Recommendations
{
    /// <summary>
    /// An array of recommendation seed objects.
    /// </summary>
    public List<Seed>? Seeds { get; set; }

    /// <summary>
    /// An array of track object (simplified) ordered according to the parameters supplied.
    /// </summary>
    public List<Track>? Tracks { get; set; }
}

[Obsolete("Need to verify the conversion of afterFilteringSize to AfterFilteringSize")]
public class Seed
{
    /// <summary>
    /// The number of tracks available after min_* and max_* filters have been applied.
    /// </summary>
    public int? AfterFilteringSize { get; set; }

    /// <summary>
    /// The number of tracks available after relinking for regional availability.
    /// </summary>
    public int? AfterRelinkingSize { get; set; }

    /// <summary>
    /// A link to the full track or artist data for this seed. For tracks this will be a link to a Track Object. 
    /// For artists a link to an Artist Object. For genre seeds, this value will be null.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The id used to select this seed. This will be the same as the string used in the seed_artists, seed_tracks or seed_genres parameter.
    /// </summary>
    [Obsolete("COrrect summary")]
    public string? Id { get; set; }

    /// <summary>
    /// The number of recommended tracks available for this seed.
    /// </summary>
    public int? InitialPoolSize { get; set; }

    /// <summary>
    /// The entity type of this seed.
    /// One of <see cref="ItemType.Artist"/>, <see cref="ItemType.Track"/> or <see cref="ItemType.Genre"/>
    /// </summary>
    public ItemType? Type { get; set; }
}

public class SeveralAudioFeatures
{
    /// <summary>
    /// A set of audio features
    /// </summary>
    public List<AudioFeatures>? AudioFeatures { get; set; }
}

public class SeveralTracks
{
    /// <summary>
    /// A set of tracks
    /// </summary>
    public List<Track>? Tracks { get; set; }
}

/// <summary>
/// A wrapper for the context of a track
/// </summary>
public class SavedTrackContextWrapper
{
    /// <summary>
    /// When a track was added
    /// </summary>
    public DateTime? AddedAt { get; set; }

    /// <summary>
    /// The associated Spotify track
    /// </summary>
    public Track? Track { get; set; }
}











public class AudioFeatures
{
    /// <summary>
    /// A confidence measure from 0.0 to 1.0 of whether the track is acoustic. 1.0 represents high confidence the track is acoustic.
    /// </summary>
    public decimal? Acousticness { get; set; }

    /// <summary>
    /// A URL to access the full audio analysis of this track. An access token is required to access this data.
    /// </summary>
    public string? AnalysisUrl { get; set; }

    /// <summary>
    /// Danceability describes how suitable a track is for dancing based on a combination of musical elements including tempo, 
    /// rhythm stability, beat strength, and overall regularity. A value of 0.0 is least danceable and 1.0 is most danceable.
    /// </summary>
    public decimal? Danceability { get; set; }

    /// <summary>
    /// The duration of the track in milliseconds.
    /// </summary>
    public long? DurationMs { get; set; }

    /// <summary>
    /// Energy is a measure from 0.0 to 1.0 and represents a perceptual measure of intensity and activity. 
    /// Typically, energetic tracks feel fast, loud, and noisy. For example, death metal has high energy, while a Bach prelude scores low on the scale. 
    /// Perceptual features contributing to this attribute include dynamic range, perceived loudness, timbre, onset rate, and general entropy.
    /// </summary>
    public decimal? Energy { get; set; }

    /// <summary>
    /// The Spotify ID for the track.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Predicts whether a track contains no vocals. "Ooh" and "aah" sounds are treated as instrumental in this context. 
    /// Rap or spoken word tracks are clearly "vocal". The closer the instrumentalness value is to 1.0, the greater likelihood the track contains no vocal content. 
    /// Values above 0.5 are intended to represent instrumental tracks, but confidence is higher as the value approaches 1.0.
    /// </summary>
    public decimal? Instrumentalness { get; set; }

    /// <summary>
    /// The key the track is in. Integers map to pitches using standard Pitch Class notation. E.g. 0 = C, 1 = C♯/D♭, 2 = D, and so on. 
    /// -1 = No key detected
    /// 1 = C Sharp / D Flat
    /// 2 = D
    /// etc..
    /// </summary>
    public int? Key { get; set; }

    /// <summary>
    /// Detects the presence of an audience in the recording. Higher liveness values represent an increased probability that the track was performed live. 
    /// A value above 0.8 provides strong likelihood that the track is live.
    /// </summary>
    public decimal? Liveness { get; set; }

    /// <summary>
    /// The overall loudness of a track in decibels (dB). Loudness values are averaged across the entire track and are useful for comparing relative loudness of tracks. 
    /// Loudness is the quality of a sound that is the primary psychological correlate of physical strength (amplitude). Values typically range between -60 and 0 db.
    /// </summary>
    public decimal? Loudness { get; set; }

    /// <summary>
    /// Mode indicates the modality (major or minor) of a track, the type of scale from which its melodic content is derived. Major is represented by 1 and minor is 0.
    /// 0 = Minor
    /// 1 = Major
    /// </summary>
    public byte? Mode { get; set; }

    /// <summary>
    /// Speechiness detects the presence of spoken words in a track. The more exclusively speech-like the recording (e.g. talk show, audio book, poetry), 
    /// the closer to 1.0 the attribute value. Values above 0.66 describe tracks that are probably made entirely of spoken words. Values between 0.33 and 0.66 
    /// describe tracks that may contain both music and speech, either in sections or layered, including such cases as rap music. Values below 0.33 most likely 
    /// represent music and other non-speech-like tracks.
    /// </summary>
    public decimal? Speechiness { get; set; }

    /// <summary>
    /// The overall estimated tempo of a track in beats per minute (BPM). In musical terminology, tempo is the speed or pace of a given piece and derives 
    /// directly from the average beat duration.
    /// </summary>
    public decimal? Tempo { get; set; }

    /// <summary>
    /// An estimated time signature. The time signature (meter) is a notational convention to specify how many beats are in each bar (or measure). 
    /// The time signature ranges from 3 to 7 indicating time signatures of "3/4", to "7/4".
    /// </summary>
    public byte? TimeSignature { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the track.
    /// </summary>
    public string? TrackHref { get; set; }

    /// <summary>
    /// The object type.
    /// Allowed value:"audio_features"
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The Spotify URI for the track.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// A measure from 0.0 to 1.0 describing the musical positiveness conveyed by a track. 
    /// Tracks with high valence sound more positive (e.g. happy, cheerful, euphoric), 
    /// while tracks with low valence sound more negative (e.g. sad, depressed, angry).
    /// </summary>
    public decimal? Valence { get; set; }
}
public class AudioAnalysis
{
    /// <summary>
    /// </summary>
    public Meta? Meta { get; set; }

    /// <summary>
    /// </summary>
    public TrackAnalysis? Track { get; set; }

    /// <summary>
    /// The time intervals of the bars throughout the track. A bar (or measure) is a segment of time defined as a given number of beats.
    /// </summary>
    public List<Bar>? Bars { get; set; }

    /// <summary>
    /// The time intervals of beats throughout the track. A beat is the basic time unit of a piece of music; 
    /// for example, each tick of a metronome. Beats are typically multiples of tatums
    /// </summary>
    public List<Beat>? Beats { get; set; }

    /// <summary>
    /// Sections are defined by large variations in rhythm or timbre, e.g. chorus, verse, bridge, guitar solo, etc. 
    /// Each section contains its own descriptions of tempo, key, mode, time_signature, and loudness.
    /// </summary>
    public List<Section>? Sections { get; set; }

    /// <summary>
    /// Each segment contains a roughly conisistent sound throughout its duration.
    /// </summary>
    public List<Segment>? Segments { get; set; }

    /// <summary>
    /// A tatum represents the lowest regular pulse train that a listener intuitively infers from the timing of perceived musical events (segments).
    /// </summary>
    public List<Tatum>? Satums { get; set; }
}

public class Bar
{
    /// <summary>
    /// The starting point (in seconds) of the time interval.
    /// </summary>
    public double? Start { get; set; }

    /// <summary>
    /// The duration (in seconds) of the time interval.
    /// </summary>
    public double? Duration { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the interval.
    /// </summary>
    public double? Confidence { get; set; }
}

public class Beat
{
    /// <summary>
    /// The starting point (in seconds) of the time interval.
    /// </summary>
    public double? Start { get; set; }

    /// <summary>
    /// The duration (in seconds) of the time interval.
    /// </summary>
    public double? Duration { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the interval.
    /// </summary>
    public double? Confidence { get; set; }
}

public class Meta
{
    /// <summary>
    /// The version of the Analyzer used to analyze this track.
    /// </summary>
    public string? AnalyzerVersion { get; set; }

    /// <summary>
    /// he platform used to read the track's audio data.
    /// </summary>
    public string? Platform { get; set; }

    /// <summary>
    /// A detailed status code for this track. If analysis data is missing, this code may explain why.
    /// </summary>
    public string? DetailedStatus { get; set; }

    /// <summary>
    /// The return code of the analyzer process. 0 if successful, 1 if any errors occurred.
    /// </summary>
    public int? StatusCode { get; set; }

    /// <summary>
    /// The Unix timestamp (in seconds) at which this track was analyzed.
    /// </summary>
    public int? Timestamp { get; set; }

    /// <summary>
    /// The amount of time taken to analyze this track.
    /// </summary>
    public double? AnalysisTime { get; set; }

    /// <summary>
    /// The method used to read the track's audio data.
    /// </summary>
    public string? Input_Process { get; set; }
}

public class Section
{

    /// <summary>
    /// The starting point (in seconds) of the section.
    /// </summary>
    public int? Start { get; set; }

    /// <summary>
    /// The duration (in seconds) of the section.
    /// </summary>
    public double? Duration { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the section's "designation".
    /// </summary>
    public int? Confidence { get; set; }

    /// <summary>
    /// The overall loudness of the section in decibels (dB). Loudness values are useful for comparing relative loudness of sections within tracks.
    /// </summary>
    public double? Loudness { get; set; }

    /// <summary>
    /// The overall estimated tempo of the section in beats per minute (BPM). In musical terminology, tempo is the speed or pace of a given piece 
    /// and derives directly from the average beat duration.
    /// </summary>
    public double? Tempo { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the <see cref="Tempo"/>. Some tracks contain tempo changes or sounds which don't contain tempo (like pure speech) 
    /// which would correspond to a low value in this field.
    /// </summary>
    public double? TempoConfidence { get; set; }

    /// <summary>
    /// The estimated overall key of the section. The values in this field ranging from 0 to 11 mapping to pitches using 
    /// standard Pitch Class notation (E.g. 0 = C, 1 = C♯/D♭, 2 = D, and so on). If no key was detected, the value is -1.
    /// </summary>
    public int? Key { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the <see cref="Key"/>. Songs with many key changes may correspond to low values in this field.
    /// </summary>
    public double? KeyConfidence { get; set; }

    /// <summary>
    /// Indicates the modality (major or minor) of a section, the type of scale from which its melodic content is derived. 
    /// This field will contain a 0 for "minor", a 1 for "major", or a -1 for no result. Note that the major key (e.g. C major) 
    /// could more likely be confused with the minor key at 3 semitones lower (e.g. A minor) as both keys carry the same pitches.
    /// </summary>
    public int? Mode { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the <see cref="Mode"/>.
    /// </summary>
    public double? ModeConfidence { get; set; }

    /// <summary>
    /// An estimated time signature. The time signature (meter) is a notational convention to specify how many beats are in each bar (or measure). 
    /// The time signature ranges from 3 to 7 indicating time signatures of "3/4", to "7/4".
    /// </summary>
    public int? TimeSignature { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the <see cref="TimeSignature"/>. Sections with time signature changes may correspond to low values in this field.
    /// </summary>
    public int? TimeSignatureConfidence { get; set; }
}

public class Segment
{
    /// <summary>
    /// The starting point (in seconds) of the segment.
    /// </summary>
    public double? Start { get; set; }

    /// <summary>
    /// The duration (in seconds) of the segment.
    /// </summary>
    public double? Duration { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the segmentation. 
    /// Segments of the song which are difficult to logically segment (e.g: noise) may correspond to low values in this field.
    /// </summary>
    public double? Confidence { get; set; }

    /// <summary>
    /// The onset loudness of the segment in decibels (dB). Combined with <see cref="LoudnessMax"/> and <see cref="LoudnessMaxTime"/>, 
    /// these components can be used to describe the "attack" of the segment.
    /// </summary>
    public double? LoudnessStart { get; set; }

    /// <summary>
    /// The peak loudness of the segment in decibels (dB). Combined with loudness_start and loudness_max_time, these components can be used to describe the "attack" of the segment.
    /// </summary>
    public double? LoudnessMax { get; set; }

    /// <summary>
    /// The segment-relative offset of the segment peak loudness in seconds. Combined with loudness_start and loudness_max, these components can be used to desctibe the "attack" of the segment.
    /// </summary>
    public double? LoudnessMaxTime { get; set; }

    /// <summary>
    /// The offset loudness of the segment in decibels (dB). This value should be equivalent to the loudness_start of the following segment.
    /// </summary>
    public int? LoudnessEnd { get; set; }

    /// <summary>
    /// Pitch content is given by a “chroma” vector, corresponding to the 12 pitch classes C, C#, D to B, with values ranging from 0 to 1 that describe 
    /// the relative dominance of every pitch in the chromatic scale. For example a C Major chord would likely be represented by large 
    /// values of C, E and G (i.e. classes 0, 4, and 7).
    /// 
    /// Vectors are normalized to 1 by their strongest dimension, therefore noisy sounds are likely represented by values that are all 
    /// close to 1, while pure tones are described by one value at 1 (the pitch) and others near 0. As can be seen below, the 12 vector 
    /// indices are a combination of low-power spectrum values at their respective pitch frequencies.
    /// </summary>
    public List<double>? Pitches { get; set; }

    /// <summary>
    /// Timbre is the quality of a musical note or sound that distinguishes different types of musical instruments, or voices. 
    /// It is a complex notion also referred to as sound color, texture, or tone quality, and is derived from the shape of a segment’s 
    /// spectro-temporal surface, independently of pitch and loudness. The timbre feature is a vector that includes 12 unbounded values roughly 
    /// centered around 0. Those values are high level abstractions of the spectral surface, ordered by degree of importance.

    /// For completeness however, the first dimension represents the average loudness of the segment; second emphasizes brightness; 
    /// third is more closely correlated to the flatness of a sound; fourth to sounds with a stronger attack; etc.See an image below 
    /// representing the 12 basis functions(i.e.template segments).
    /// </summary>
    public List<double>? Timbre { get; set; }
}

public class Tatum
{
    /// <summary>
    /// The starting point (in seconds) of the time interval.
    /// </summary>
    public double? Start { get; set; }

    /// <summary>
    /// The duration (in seconds) of the time interval.
    /// </summary>
    public double? Duration { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the interval.
    /// </summary>
    public double? Confidence { get; set; }
}

public class TrackAnalysis
{
    /// <summary>
    /// The exact number of audio samples analyzed from this track. See also <see cref="AnalysisSampleRate"/>.
    /// </summary>
    public int? NumSamples { get; set; }

    /// <summary>
    /// Length of the track in seconds.
    /// </summary>
    public double? Duration { get; set; }

    /// <summary>
    /// This field will always contain the empty string.
    /// </summary>
    public string? Samplemd5 { get; set; }

    /// <summary>
    /// An offset to the start of the region of the track that was analyzed. (As the entire track is analyzed, this should always be 0.)
    /// </summary>
    public int? OffsetSeconds { get; set; }

    /// <summary>
    /// The length of the region of the track was analyzed, if a subset of the track was analyzed. (As the entire track is analyzed, this should always be 0.)
    /// </summary>
    public int? WindowSeconds { get; set; }

    /// <summary>
    /// The sample rate used to decode and analyze this track. May differ from the actual sample rate of this track available on Spotify.
    /// </summary>
    public int? AnalysisSampleRate { get; set; }

    /// <summary>
    /// The number of channels used for analysis. If 1, all channels are summed together to mono before analysis.
    /// </summary>
    public int? AnalysisChannels { get; set; }

    /// <summary>
    /// The time, in seconds, at which the track's fade-in period ends. If the track has no fade-in, this will be 0.0.
    /// </summary>
    public int? EndOfFadeIn { get; set; }

    /// <summary>
    /// The time, in seconds, at which the track's fade-out period starts. If the track has no fade-out, this should match the track's length.
    /// </summary>
    public double? StartOfFadeOut { get; set; }

    /// <summary>
    /// The overall loudness of a track in decibels (dB). Loudness values are averaged across the entire track and are useful for comparing 
    /// relative loudness of tracks. Loudness is the quality of a sound that is the primary psychological correlate of physical strength (amplitude). 
    /// Values typically range between -60 and 0 db.
    /// </summary>
    public double? Loudness { get; set; }

    /// <summary>
    /// The overall estimated tempo of a track in beats per minute (BPM). 
    /// In musical terminology, tempo is the speed or pace of a given piece and derives directly from the average beat duration.
    /// </summary>
    public double? Tempo { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the <see cref="Tempo"/>.
    /// </summary>
    public double? TempoConfidence { get; set; }

    /// <summary>
    /// An estimated time signature. The time signature (meter) is a notational convention to specify how many beats are in each bar (or measure). 
    /// The time signature ranges from 3 to 7 indicating time signatures of "3/4", to "7/4".
    /// </summary>
    public int? TimeSignature { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the <see cref="TimeSignature"/>.
    /// </summary>
    public double? TimesignatureConfidence { get; set; }

    /// <summary>
    /// The key the track is in. Integers map to pitches using standard Pitch Class notation. E.g. 0 = C, 1 = C♯/D♭, 2 = D, and so on. If no key was detected, the value is
    /// </summary>
    public int? Key { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the <see cref="Key"/>.
    /// </summary>
    public double? Keyconfidence { get; set; }

    /// <summary>
    /// Mode indicates the modality (major or minor) of a track, the type of scale from which its melodic content is derived. Major is represented by 1 and minor is 0.
    /// </summary>
    public int? Mode { get; set; }

    /// <summary>
    /// The confidence, from 0.0 to 1.0, of the reliability of the <see cref="Mode"/>.
    /// </summary>
    public double? ModeConfidence { get; set; }

    /// <summary>
    /// An Echo Nest Musical Fingerprint (ENMFP) codestring for this track.
    /// https://academiccommons.columbia.edu/doi/10.7916/D8Q248M4
    /// </summary>
    public string? Codestring { get; set; }

    /// <summary>
    /// A version number for the Echo Nest Musical Fingerprint format used in the <see cref="Codestring"/> field.
    /// </summary>
    public double? CodeVersion { get; set; }

    /// <summary>
    /// An EchoPrint codestring for this track.
    /// https://github.com/spotify/echoprint-codegen
    /// </summary>
    public string? Echoprintstring { get; set; }

    /// <summary>
    /// A version number for the EchoPrint format used in the <see cref="Echoprintstring"/> field.
    /// </summary>
    public double? EchoprintVersion { get; set; }

    /// <summary>
    /// A Synchstring for this track.
    /// https://github.com/echonest/synchdata
    /// </summary>
    public string? Synchstring { get; set; }

    /// <summary>
    /// A version number for the Synchstring used in the <see cref="Synchstring"/> field.
    /// </summary>
    public int? SynchVersion { get; set; }

    /// <summary>
    /// A Rhythmstring for this track. The format of this string is similar to the Synchstring.
    /// </summary>
    public string? Rhythmstring { get; set; }

    /// <summary>
    /// A version number for the Rhythmstring used in the <see cref="Rhythmstring"/> field.
    /// </summary>
    public int? RhythmVersion { get; set; }
}