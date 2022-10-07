namespace Spotify.Core.Model;

public class Track
{
    public Album? Album { get; set; }

    public List<Artist>? Artists { get; set; }

    public int? DiscNumber { get; set; }

    public long? DurationMs { get; set; }

    public bool? Explicit { get; set; }

    public ExternalIds? ExternalIds { get; set; }

    public ExternalUrls? ExternalUrls { get; set; }

    public string? Href { get; set; }

    public string? Id { get; set; }

    public bool? IsLocal { get; set; }

    public bool? IsPlayable { get; set; }

    public string? Name { get; set; }

    public int? Popularity { get; set; }

    public string? PreviewUrl { get; set; }

    public int? TrackNumber { get; set; }

    public ItemType Type { get; set; }

    public string? Uri { get; set; }
}

