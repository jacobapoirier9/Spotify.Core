using Spotify.Web.Services.SpotifyEnhancer;

namespace Spotify.Web.Services;

public interface IDataService
{
    public List<TrackInterval> GetTrackIntervals(string username, string trackId);
}
