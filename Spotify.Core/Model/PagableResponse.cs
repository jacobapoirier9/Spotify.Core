namespace Spotify.Core.Model;

public class PagableResponse<T>
{
    /// <summary>
    /// A link to the Web API endpoint returning the full result of the request
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The requested content
    /// </summary>
    public List<T>? Items { get; set; }

    /// <summary>
    /// The maximum number of items in the response (as set in the query or by default).
    /// </summary>
    public short? Limit { get; set; }

    /// <summary>
    /// URL to the next page of items. (null if none)
    /// </summary>
    public string? Next { get; set; }

    /// <summary>
    /// The offset of the items returned (as set in the query or by default)
    /// </summary>
    public short? Offset { get; set; }

    /// <summary>
    /// URL to the previous page of items. (null if none)
    /// </summary>
    public string? Previous { get; set; }

    /// <summary>
    /// URL to the previous page of items. (null if none)
    /// </summary>
    public int? Total { get; set; }

    /// <summary>
    /// Only used in <see cref="GetRecentlyPlayedTracks"/>
    /// </summary>
    public Cursors? Cursors { get; set; }
}

public class Cursors
{
    public long? Before { get; set; }

    public long? After { get; set; }
}