namespace Spotify.Core.Model;

public class Artist
{
    /// <summary>
    /// Known external URLs for this artist.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// Information about the followers of the artist.
    /// </summary>
    public Followers? Followers { get; set; }

    /// <summary>
    /// A list of the genres the artist is associated with. If not yet classified, the array is empty.
    /// </summary>
    public List<string>? Genres { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the artist.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify ID for the artist.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Images of the artist in various sizes, widest first.
    /// </summary>
    public List<Image>? Images { get; set; }

    /// <summary>
    /// The name of the artist.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most popular. 
    /// The artist's popularity is calculated from the popularity of all the artist's tracks.
    /// </summary>
    public int? Popularity { get; set; }

    /// <summary>
    /// The object type.
    /// </summary>
    public ItemType Type { get; set; }

    /// <summary>
    /// The Spotify URI for the artist.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// The tracks of the album.
    /// </summary>
    public PagableResponse<Track>? Tracks { get; set; }
}

