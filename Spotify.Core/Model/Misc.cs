using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Core.Model;

public interface IReturn<T> { }

public interface IReturnVoid { }

public enum ItemType
{
    Track = 1,
    Album = 2,
    Artist = 3,
    Playlist = 4,
    User = 5
}

public class ExternalUrls
{
    /// <summary>
    /// The Spotify URL for the object.
    /// </summary>
    public string? Spotify { get; set; }
}

public class Image
{
    /// <summary>
    /// The source URL of the image.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// The image height in pixels.
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// The image width in pixels.
    /// </summary>
    public int? Width { get; set; }
}

public class Restrictions
{
    /// <summary>
    /// The reason for the restriction. Albums may be restricted if the content is not available in a given market, to the user's 
    /// subscription type, or when the user's account is set to not play explicit content. Additional reasons may be added in the future.
    /// </summary>
    public string? Reason { get; set; }
}

public class Followers
{
    /// <summary>
    /// This will always be set to null, as the Web API does not support it at the moment.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The total number of followers.
    /// </summary>
    public int? Total { get; set; }
}