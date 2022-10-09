using Spotify.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Core.Model;

/// <summary>
/// Get Spotify catalog information about albums, artists, playlists, tracks, shows, episodes or audiobooks that match a keyword string.
/// Note: Audiobooks are only available for the US market.
/// </summary>
[Route($"{Configuration.ApiUri}/search", Verb.Get)]
public class Search : IReturn<SearchResponse>
{
    /// <summary>
    /// Your search query.
    /// You can narrow down your search using field filters.The available filters are album, artist, track, year, upc, tag:hipster, tag:new, isrc, and genre.Each field filter only applies to certain result types.

    /// The artist and year filters can be used while searching albums, artists and tracks.You can filter on a single year or a range (e.g. 1955-1960).
    /// The album filter can be used while searching albums and tracks.
    /// The genre filter can be used while searching artists and tracks.
    /// The isrc and track filters can be used while searching tracks.
    /// The upc, tag:new and tag:hipster filters can only be used while searching albums.The tag:new filter will return albums released in the past two weeks and tag:hipster can be used to return only albums with the lowest 10% popularity.

    ///  Example value: "remaster%20track:Doxy%20artist:Miles%20Davis"
    /// </summary>
    [Obsolete("Look into a query builder helper method")]
    public string? Q { get; set; }

    /// <summary>
    /// A comma-separated list of item types to search across. Search results include hits from all the specified item types. 
    /// For example: q=abacab&type=album,track returns both albums and tracks matching "abacab".
    /// </summary>
    public List<ItemType>? Type { get; set; }

    /// <summary>
    /// If include_external=audio is specified it signals that the client can play externally hosted audio content, and marks 
    /// the content as playable in the response. By default externally hosted audio content is marked as unplayable in the response.
    /// Allowed value: "audio"
    /// </summary>
    public string? IncludeExternal { get; set; }

    /// <summary>
    /// The maximum number of results to return in each item type.
    /// Default value: 20
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// An ISO 3166-1 alpha-2 country code. If a country code is specified, only content that is available in that market will be returned.
    /// If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter.
    /// Note: If neither market or user country are provided, the content is considered unavailable for the client.
    /// Users can view the country that is associated with their account in the account settings.
    /// </summary>
    public string? Market { get; set; }

    /// <summary>
    /// The index of the first result to return. Use with limit to get the next page of search results.
    /// </summary>
    public int? Offset { get; set; }
}

public class SearchResponse
{
    public PagableResponse<Track>? Tracks { get; set; }

    public PagableResponse<Artist>? Artists { get; set; }

    public PagableResponse<Album>? Albums { get; set; }

    public PagableResponse<object>? Playlists => throw new NotImplementedException("Need to implement Playlist DTO");

    public PagableResponse<Show>? Shows { get; set; }

    public PagableResponse<Episode>? Episodes { get; set; }

    public PagableResponse<Audiobook>? Audiobooks { get; set; }
}

