﻿namespace Spotify.Core.Model;

public interface IReturn<T> { }

public enum ItemType
{
    Unknown,
    Track,
    Album,
    Artist,
    Playlist,
    User,
    Episode,
    Show,
    Audiobook,
    Chapter,
    Genre
}

public class ExternalIds
{
    public string? Isrc { get; set; }

    public string? Ean { get; set; }

    public string? Upc { get; set; }
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
    /// market - The content item is not available in the given market.
    /// product - The content item is not available for the user's subscription type.
    /// explicit - The content item is explicit and the user's account is set to not play explicit content.
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

public class Copyright
{
    /// <summary>
    /// The copyright text for this content.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// The type of copyright: C = the copyright, P = the sound recording (performance) copyright.
    /// </summary>
    public string? Type { get; set; }
}

public class ErrorWrapper
{
    public Error? Error { get; set; }
}

public class Error
{
    public string? Message { get; set; }

    public int? Status { get; set; }
}

