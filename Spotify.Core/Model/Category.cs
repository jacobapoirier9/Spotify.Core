using Spotify.Core.Attributes;

namespace Spotify.Core.Model;

/// <summary>
/// Get a list of categories used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
/// </summary>
[Route($"{Configuration.ApiUri}/browse/categories", Verb.Get)]
public class GetSeveralBrowseCategories : IReturnPagable<SeveralBrowseCategories>
{
    /// <summary>
    /// A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want to narrow the list of returned categories to those
    /// relevant to a particular country. If omitted, the returned items will be globally relevant.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// The desired language, consisting of an ISO 639-1 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore. 
    /// For example: es_MX, meaning "Spanish (Mexico)". Provide this parameter if you want the category metadata returned in a particular language.
    /// Note: if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default 
    /// language(American English). The locale parameter, combined with the country parameter, may give odd results if not carefully matched.
    /// For example country=SE&locale=de_DE will return a list of categories relevant to Sweden but as German language strings.
    /// </summary>
    public string? Locale { get; set; }

    /// <summary>
    /// The index of the first item to return. Default: 0 (the first item). Use with limit to get the next set of items.
    /// </summary>
    public int? Offset { get; set; }
}

/// <summary>
/// Get a single category used to tag items in Spotify (on, for example, the Spotify player’s “Browse” tab).
/// </summary>
[Route($"{Configuration.ApiUri}/browse/categories/{{{nameof(CategoryId)}}}", Verb.Get)]
public class GetBrowseCategory : IReturn<Category>
{
    /// <summary>
    /// The Spotify category ID for the category.
    /// </summary>
    public string? CategoryId { get; set; }

    /// <summary>
    /// A country: an ISO 3166-1 alpha-2 country code. Provide this parameter if you want to narrow the list of returned categories to those
    /// relevant to a particular country. If omitted, the returned items will be globally relevant.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// The desired language, consisting of an ISO 639-1 language code and an ISO 3166-1 alpha-2 country code, joined by an underscore. 
    /// For example: es_MX, meaning "Spanish (Mexico)". Provide this parameter if you want the category metadata returned in a particular language.
    /// Note: if locale is not supplied, or if the specified language is not available, all strings will be returned in the Spotify default 
    /// language(American English). The locale parameter, combined with the country parameter, may give odd results if not carefully matched.
    /// For example country=SE&locale=de_DE will return a list of categories relevant to Sweden but as German language strings.
    /// </summary>
    public string? Locate { get; set; }
}

public class SeveralBrowseCategories
{
    /// <summary>
    /// A paged set of categories
    /// </summary>
    public Pagable<Category>? Categories { get; set; }
}

public class Category
{
    /// <summary>
    /// A link to the Web API endpoint returning full details of the category.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// The category icon, in various sizes.
    /// </summary>
    public List<Image>? Icons { get; set; }

    /// <summary>
    /// The Spotify category ID of the category.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// The name of the category.
    /// </summary>
    public string? Name { get; set; }
}
