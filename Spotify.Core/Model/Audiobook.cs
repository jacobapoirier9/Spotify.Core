using Spotify.Core.Attributes;

namespace Spotify.Core.Model;

/// <summary>
/// Get Spotify catalog information for a single audiobook.
/// Note: Audiobooks are only available for the US market.
/// </summary>
[Route($"{Configuration.ApiUri}/audiobooks/{{{nameof(Id)}}}", Verb.Get)]
public class GetAudiobook : IReturn<Audiobook>
{
    /// <summary>
    /// The Spotify ID for the audiobook.
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
/// Get Spotify catalog information for several audiobooks identified by their Spotify IDs.
/// Note: Audiobooks are only available for the US market.
/// </summary>
[Route($"{Configuration.ApiUri}/audiobooks", Verb.Get)]
public class GetSeveralAudiobooks : IReturn<SeveralAudiobooks>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs. Maximum: 50 IDs.
    /// </summary>,
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
/// Get Spotify catalog information for a single chapter.
/// Note: Chapters are only available for the US market.
/// </summary>
[Route($"{Configuration.ApiUri}/chapters/{{{nameof(Id)}}}", Verb.Get)]
public class GetChapter : IReturn<Chapter>
{
    /// <summary>
    /// The Spotify ID for the chapter.
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
/// Get Spotify catalog information for several chapters identified by their Spotify IDs.
/// Note: Chapters are only available for the US market.
/// </summary>
[Route($"{Configuration.ApiUri}/chapters", Verb.Get)]
public class GetSeveralChapters : IReturn<SeveralChapters>
{
    /// <summary>
    /// A comma-separated list of the Spotify IDs. Maximum: 50 IDs.
    /// </summary>,
    public List<string>? Ids { get; set; }

    /// <summary>
    /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only content that is available in that market will be returned.
    /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
    /// Note: If neither market or user country are provided, the content is considered unavailable for the client.
    /// Users can view the country that is associated with their account in the account settings.
    /// </summary>
    public string? Market { get; set; }
}

public class SeveralAudiobooks
{
    /// <summary>
    /// A set of audiobooks
    /// </summary>
    public List<Audiobook>? Audiobooks { get; set; }
}

public class SeveralChapters
{
    /// <summary>
    /// A set of chapters
    /// </summary>
    public List<Chapter>? Chapters { get; set; }
}

public class Audiobook
{
    /// <summary>
    /// The author(s) for the audiobook.
    /// </summary>
    public List<Author>? Authors { get; set; }

    /// <summary>
    /// A list of the countries in which the audiobook can be played, identified by their ISO 3166-1 alpha-2 code.
    /// </summary>
    public List<string>? AvailableMarkets { get; set; }

    /// <summary>
    /// The copyright statements of the audiobook.
    /// </summary>
    public List<Copyright>? Copyrights { get; set; }

    /// <summary>
    /// A description of the audiobook. HTML tags are stripped away from this field, use html_description field in case HTML tags are needed.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// A description of the audiobook. This field may contain HTML tags.
    /// </summary>
    public string? HtmlDescription { get; set; }

    /// <summary>
    /// Whether or not the audiobook has explicit content (true = yes it does; false = no it does not OR unknown).
    /// </summary>
    public bool? Explicit { get; set; }

    /// <summary>
    /// External URLs for this audiobook
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the audiobook.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify ID for the audiobook.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The cover art for the audiobook in various sizes, widest first.
    /// </summary>
    public List<Image>? Images { get; set; }

    /// <summary>
    /// A list of the languages used in the audiobook, identified by their ISO 639 code.
    /// </summary>
    public List<string>? Languages { get; set; }

    /// <summary>
    /// The media type of the audiobook.
    /// </summary>
    public string? MediaType { get; set; }

    /// <summary>
    /// The name of the audiobook.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The narrators of the audiobook
    /// </summary>
    public List<Narrator>? Narrators { get; set; }

    /// <summary>
    /// The publisher of the audiobook.
    /// </summary>
    public string? Publisher { get; set; }

    /// <summary>
    /// The object type.
    /// </summary>
    public ItemType? Type { get; set; }

    /// <summary>
    /// The Spotify URI for the audiobook.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// The number of chapters in this audiobook.
    /// </summary>
    public int? TotalChapters { get; set; }

    /// <summary>
    /// The chapters of the audiobook.
    /// </summary>
    public List<Chapter>? Chapters { get; set; }
}

public class Chapter
{
    /// <summary>
    /// A URL to a 30 second preview (MP3 format) of the episode. null if not available.
    /// </summary>
    public string? AudioPreviewUrl { get; set; }

    /// <summary>
    /// The number of the chapter
    /// </summary>
    public int? ChapterNumber { get; set; }

    /// <summary>
    /// A description of the episode. HTML tags are stripped away from this field, use html_description field in case HTML tags are needed.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// A description of the episode. This field may contain HTML tags.
    /// </summary>
    public string? HtmlDescription { get; set; }

    /// <summary>
    /// The episode length in milliseconds.
    /// </summary>
    public int? DurationMs { get; set; }

    /// <summary>
    /// Whether or not the episode has explicit content (true = yes it does; false = no it does not OR unknown).
    /// </summary>
    public bool? Explicit { get; set; }

    /// <summary>
    /// External URLs for this episode.
    /// </summary>
    public ExternalUrls? ExternalUrls { get; set; }

    /// <summary>
    /// A link to the Web API endpoint providing full details of the episode.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The Spotify ID for the episode.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The cover art for the episode in various sizes, widest first.
    /// </summary>
    public List<Image>? Images { get; set; }

    /// <summary>
    /// True if the episode is playable in the given market. Otherwise false.
    /// </summary>
    public bool? IsPlayable { get; set; }

    /// <summary>
    /// A list of the languages used in the episode, identified by their ISO 639-1 code.
    /// </summary>
    public List<string>? Languages { get; set; }

    /// <summary>
    /// The name of the episode.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The date the chapter was first released.
    /// </summary>
    public string? ReleaseDate { get; set; }

    /// <summary>
    /// The precision with which <see cref="ReleaseDate"/> value is known.
    /// Allowed values: year, month, day
    /// </summary>
    public string? ReleaseDatePrecision { get; set; }

    /// <summary>
    /// The user's most recent position in the episode. Set if the supplied access token is a user token and has the scope 'user-read-playback-position'.
    /// </summary>
    public ResumePoint? ResumePoint { get; set; }

    /// <summary>
    /// The object type.
    /// </summary>
    public ItemType? Type { get; set; }

    /// <summary>
    /// The Spotify URI for the episode.
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// Included in the response when a content restriction is applied. See Restriction Object for more details.
    /// </summary>
    public Restrictions? Restrictions { get; set; }

    /// <summary>
    /// Audiobook for the episode
    /// </summary>
    public Audiobook? Audiobook { get; set; }
}

public class Author
{
    /// <summary>
    /// The name of the author.
    /// </summary>
    public string? Name { get; set; }
}

public class Narrator
{
    /// <summary>
    /// The name of the Narrator.
    /// </summary>
    public string? Name { get; set; }
}
