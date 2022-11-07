using Spotify.Core.Model;
using Spotify.Web.Services.SpotifyEnhancer;

namespace Spotify.Web.Models;

public class HomeSingleTrack
{
    public Track? Track { get; set; }

    public List<TrackInterval>? TrackIntervals { get; set; }

    public PlaybackState? PlaybackState { get; set; }
}
