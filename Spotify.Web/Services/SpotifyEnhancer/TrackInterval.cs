using ServiceStack.DataAnnotations;

namespace Spotify.Web.Services.SpotifyEnhancer;

[Alias("TrackIntervals")]
public class TrackInterval
{
    public string? Username { get; set; }

    public string? TrackId { get; set; }

    public string? DisplayName { get; set; }

    public int? StartMs { get; set; }

    public int? EndMs { get; set; }
}
