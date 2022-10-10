using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Tests;

public class SpotifyClientBuildMessageExperimentalTests
{
    internal const string BaseUri = "https://jake.com";

    [Fact]
    public void InlineQueryParameter_InsertsValue()
    {
        var request = new InlineQueryParameterRequest
        {
            Id = "ioehtrosadigdsje"
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test/ioehtrosadigdsje/tracks", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void InlineQueryParameter_ThrowsExceptionWhenValueNotPresent()
    {
        var request = new InlineQueryParameterRequest();


        Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildRequestMessageExperimental(request));
    }

    [Fact]
    public void QueryParameter_AppendsString()
    {
        var request = new QueryParameterRequest
        {
            String = "string"
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?string=string", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsStringList()
    {
        var request = new QueryParameterRequest
        {
            StringList = new List<string> { "string1", "string2" }
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?string_list=string1,string2", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsInt()
    {
        var request = new QueryParameterRequest
        {
            Int = 100
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?int=100", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsIntList()
    {
        var request = new QueryParameterRequest
        {
            IntList = new List<int> { 101, 102 }
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?int_list=101,102", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsItemType()
    {
        var request = new QueryParameterRequest
        {
            ItemType = ItemType.Track
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?item_type=track", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsItemTypeList()
    {
        var request = new QueryParameterRequest
        {
            ItemTypeList = new List<ItemType> { ItemType.Artist, ItemType.Album, ItemType.Playlist }
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?item_type_list=artist,album,playlist", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void MissingRoute_ThrowsException()
    {
        var request = new MissingRoute();

        Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildRequestMessageExperimental(request));
    }

    [Fact]
    public void MalformedRoute_MissingUriThrowsException()
    {
        var request = new MalformedRouteMissingUri();

        Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildRequestMessageExperimental(request));
    }

    [Fact]
    public void MalformedRoute_MissingVerbThrowsException()
    {
        var request = new MalformedRouteMissingVerb();

        Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildRequestMessageExperimental(request));
    }

    [Fact]
    public void BodyParameterRequest_WritesString()
    {
        var request = new BodyParameterRequest
        {
            String = "value"
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        var json = GetContentString(message);
        Assert.Equal(@"{""string"":""value""}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesStringList()
    {
        var request = new BodyParameterRequest
        {
            StringList = new List<string> { "string1", "string2" }
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        var json = GetContentString(message);
        Assert.Equal(@"{""string_list"":[""string1"",""string2""]}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesInt()
    {
        var request = new BodyParameterRequest
        {
            Int = 100
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        var json = GetContentString(message);
        Assert.Equal(@"{""int"":100}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesIntList()
    {
        var request = new BodyParameterRequest
        {
            IntList = new List<int> { 101, 102 }
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        var json = GetContentString(message);
        Assert.Equal(@"{""int_list"":[101,102]}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesItemType()
    {
        var request = new BodyParameterRequest
        {
            ItemType = ItemType.Track
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        var json = GetContentString(message);
        Assert.Equal(@"{""item_type"":""track""}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesItemTypeList()
    {
        var request = new BodyParameterRequest
        {
            ItemTypeList = new List<ItemType> { ItemType.Artist, ItemType.Album, ItemType.Playlist }
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        var json = GetContentString(message);
        Assert.Equal(@"{""item_type_list"":[""artist"",""album"",""playlist""]}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesComplex()
    {
        var request = new BodyParameterRequest
        {
            String = "value",
            StringList = new List<string> { "string1", "string2" },
            Int = 100,
            IntList = new List<int> { 101, 102 },
            ItemType = ItemType.Track,
            ItemTypeList = new List<ItemType> { ItemType.Artist, ItemType.Album, ItemType.Playlist }
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        var json = GetContentString(message);
        Assert.Equal(@"{""string"":""value"",""string_list"":[""string1"",""string2""],""int"":100,""int_list"":[101,102],""item_type"":""track"",""item_type_list"":[""artist"",""album"",""playlist""]}", json);
    }

    [Fact]
    public void BodyParameterValueOnlyRequest_WritesString()
    {
        var request = new BodyParameterValueOnlyRequest
        {
            StringBody = "hello"
        }; 
        
        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        var json = GetContentString(message);
        Assert.Equal(@"""hello""", json);
    }

    [Fact]
    public void BodyParameterValueOnlyRequest_WritesStringList()
    {
        var request = new BodyParameterValueOnlyRequest
        {
            StringListBody = new List<string> { "string1", "string2" }
        };

        var message = SpotifyClient.BuildRequestMessageExperimental(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        var json = GetContentString(message);
        Assert.Equal(@"[""string1"",""string2""]", json);
    }

    [Fact]
    public void BodyParameterValueOnlyRequest_MoreThanOneSpecified_ThrowsException()
    {
        var request = new BodyParameterValueOnlyRequest
        {
            StringBody = "hello",
            StringListBody = new List<string> { "string1", "string2" }
        };

        Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildRequestMessageExperimental(request));
    }

    private string GetContentString(HttpRequestMessage? message)
    {
        Assert.NotNull(message);
        Assert.NotNull(message?.Content);

        using (var stream = message?.Content?.ReadAsStream())
        using (var reader = new StreamReader(stream ?? throw new NullReferenceException()))
        {
            return reader.ReadToEnd();
        }
    }
}

[Route($"{SpotifyClientBuildMessageExperimentalTests.BaseUri}/test/{{{nameof(Id)}}}/tracks", Verb.Get)]
public class InlineQueryParameterRequest
{
    public string? Id { get; set; }
}

[Route($"{SpotifyClientBuildMessageExperimentalTests.BaseUri}/test", Verb.Get)]
public class QueryParameterRequest
{
    public string? String { get; set; }

    public List<string>? StringList { get; set; }

    public int? Int { get; set; }

    public List<int>? IntList { get; set; }

    public ItemType? ItemType { get; set; }

    public List<ItemType>? ItemTypeList { get; set; }
}

public class MissingRoute
{

}

#nullable disable
[Route(null, Verb.Get)]
public class MalformedRouteMissingUri
{

}

[Route(SpotifyClientBuildMessageExperimentalTests.BaseUri, null)]
public class MalformedRouteMissingVerb
{

}
#nullable restore

[Route($"{SpotifyClientBuildMessageExperimentalTests.BaseUri}/test", Verb.Get)]
public class BodyParameterRequest
{
    [BodyParameter2]
    public string? String { get; set; }

    [BodyParameter2]
    public List<string>? StringList { get; set; }

    [BodyParameter2]
    public int? Int { get; set; }

    [BodyParameter2]
    public List<int>? IntList { get; set; }

    [BodyParameter2]
    public ItemType? ItemType { get; set; }

    [BodyParameter2]
    public List<ItemType>? ItemTypeList { get; set; }
}

[Route($"{SpotifyClientBuildMessageExperimentalTests.BaseUri}/test", Verb.Get)]
public class BodyParameterValueOnlyRequest
{
    [BodyParameter2(true)]
    public string? StringBody { get; set; }

    [BodyParameter2(true)]
    public List<string>? StringListBody { get; set; }
}