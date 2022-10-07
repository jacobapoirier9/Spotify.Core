using Spotify.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Core.Model;

/// <summary>
/// Get Spotify catalog information for a single audiobook.
/// Note: Audiobooks are only available for the US market.
/// </summary>
[Route($"/audiobooks/{{{nameof(Id)}}}", Verb.Get)]
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
[Route("/audiobooks", Verb.Get)]
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

public class SeveralAudiobooks
{
    public List<Audiobook>? Audiobooks { get; set; }
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
    [Obsolete("No documentation for this DTO")]
    public List<object>? Chapters { get; set; }
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