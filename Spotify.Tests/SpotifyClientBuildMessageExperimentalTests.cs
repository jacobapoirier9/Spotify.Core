using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;

namespace Spotify.Tests;

public class SpotifyClientBuildMessageTests
{
    internal const string BaseUri = "https://jake.com";

    [Fact]
    public void InlineQueryParameter_InsertsValue()
    {
        InlineQueryParameterRequest request = new()
        {
            Id = "ioehtrosadigdsje"
        };

        HttpRequestMessage message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test/ioehtrosadigdsje/tracks", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void InlineQueryParameter_ThrowsExceptionWhenValueNotPresent()
    {
        InlineQueryParameterRequest request = new();


        _ = Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildMessage(request));
    }

    [Fact]
    public void QueryParameter_AppendsString()
    {
        QueryParameterRequest request = new()
        {
            String = "string"
        };

        HttpRequestMessage message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?string=string", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsStringList()
    {
        QueryParameterRequest request = new()
        {
            StringList = new List<string> { "string1", "string2" }
        };

        HttpRequestMessage message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?string_list=string1,string2", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsInt()
    {
        QueryParameterRequest request = new()
        {
            Int = 100
        };

        HttpRequestMessage message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?int=100", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsIntList()
    {
        QueryParameterRequest request = new()
        {
            IntList = new List<int> { 101, 102 }
        };

        HttpRequestMessage message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?int_list=101,102", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsItemType()
    {
        QueryParameterRequest request = new()
        {
            ItemType = ItemType.Track
        };

        HttpRequestMessage message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?item_type=track", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void QueryParameter_AppendsItemTypeList()
    {
        QueryParameterRequest request = new()
        {
            ItemTypeList = new List<ItemType> { ItemType.Artist, ItemType.Album, ItemType.Playlist }
        };

        HttpRequestMessage message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test?item_type_list=artist,album,playlist", message.RequestUri?.ToString());
        Assert.Null(message.Content);
    }

    [Fact]
    public void MissingRoute_ThrowsException()
    {
        MissingRoute request = new();

        _ = Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildMessage(request));
    }

    [Fact]
    public void MalformedRoute_MissingUriThrowsException()
    {
        MalformedRouteMissingUri request = new();

        _ = Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildMessage(request));
    }

    [Fact]
    public void MalformedRoute_MissingVerbThrowsException()
    {
        MalformedRouteMissingVerb request = new();

        _ = Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildMessage(request));
    }

    [Fact]
    public void BodyParameterRequest_WritesString()
    {
        BodyParameterRequest request = new()
        {
            String = "value"
        };

        HttpRequestMessage? message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        string json = GetContentString(message);
        Assert.Equal(@"{""string"":""value""}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesStringList()
    {
        BodyParameterRequest request = new()
        {
            StringList = new List<string> { "string1", "string2" }
        };

        HttpRequestMessage? message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        string json = GetContentString(message);
        Assert.Equal(@"{""string_list"":[""string1"",""string2""]}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesInt()
    {
        BodyParameterRequest request = new()
        {
            Int = 100
        };

        HttpRequestMessage? message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        string json = GetContentString(message);
        Assert.Equal(@"{""int"":100}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesIntList()
    {
        BodyParameterRequest request = new()
        {
            IntList = new List<int> { 101, 102 }
        };

        HttpRequestMessage? message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        string json = GetContentString(message);
        Assert.Equal(@"{""int_list"":[101,102]}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesItemType()
    {
        BodyParameterRequest request = new()
        {
            ItemType = ItemType.Track
        };

        HttpRequestMessage? message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        string json = GetContentString(message);
        Assert.Equal(@"{""item_type"":""track""}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesItemTypeList()
    {
        BodyParameterRequest request = new()
        {
            ItemTypeList = new List<ItemType> { ItemType.Artist, ItemType.Album, ItemType.Playlist }
        };

        HttpRequestMessage? message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        string json = GetContentString(message);
        Assert.Equal(@"{""item_type_list"":[""artist"",""album"",""playlist""]}", json);
    }

    [Fact]
    public void BodyParameterRequest_WritesComplex()
    {
        BodyParameterRequest request = new()
        {
            String = "value",
            StringList = new List<string> { "string1", "string2" },
            Int = 100,
            IntList = new List<int> { 101, 102 },
            ItemType = ItemType.Track,
            ItemTypeList = new List<ItemType> { ItemType.Artist, ItemType.Album, ItemType.Playlist }
        };

        HttpRequestMessage? message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        string json = GetContentString(message);
        Assert.Equal(@"{""string"":""value"",""string_list"":[""string1"",""string2""],""int"":100,""int_list"":[101,102],""item_type"":""track"",""item_type_list"":[""artist"",""album"",""playlist""]}", json);
    }

    [Fact]
    public void BodyParameterValueOnlyRequest_WritesString()
    {
        BodyParameterValueOnlyRequest request = new()
        {
            StringBody = "hello"
        };

        HttpRequestMessage? message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        string json = GetContentString(message);
        Assert.Equal(@"""hello""", json);
    }

    [Fact]
    public void BodyParameterValueOnlyRequest_WritesStringList()
    {
        BodyParameterValueOnlyRequest request = new()
        {
            StringListBody = new List<string> { "string1", "string2" }
        };

        HttpRequestMessage? message = SpotifyClient.BuildMessage(request);

        Assert.NotNull(message);
        Assert.Equal($"{BaseUri}/test", message?.RequestUri?.ToString());

        string json = GetContentString(message);
        Assert.Equal(@"[""string1"",""string2""]", json);
    }

    [Fact]
    public void BodyParameterValueOnlyRequest_MoreThanOneSpecified_ThrowsException()
    {
        BodyParameterValueOnlyRequest request = new()
        {
            StringBody = "hello",
            StringListBody = new List<string> { "string1", "string2" }
        };

        _ = Assert.ThrowsAny<Exception>(() => SpotifyClient.BuildMessage(request));
    }

    private string GetContentString(HttpRequestMessage? message)
    {
        Assert.NotNull(message);
        Assert.NotNull(message?.Content);

        using (Stream? stream = message?.Content?.ReadAsStream())
        using (StreamReader reader = new(stream ?? throw new NullReferenceException()))
        {
            return reader.ReadToEnd();
        }
    }
}

[Route($"{SpotifyClientBuildMessageTests.BaseUri}/test/{{{nameof(Id)}}}/tracks", Verb.Get)]
public class InlineQueryParameterRequest
{
    public string? Id { get; set; }
}

[Route($"{SpotifyClientBuildMessageTests.BaseUri}/test", Verb.Get)]
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

[Route(SpotifyClientBuildMessageTests.BaseUri, null)]
public class MalformedRouteMissingVerb
{

}
#nullable restore

[Route($"{SpotifyClientBuildMessageTests.BaseUri}/test", Verb.Get)]
public class BodyParameterRequest
{
    [Body]
    public string? String { get; set; }

    [Body]
    public List<string>? StringList { get; set; }

    [Body]
    public int? Int { get; set; }

    [Body]
    public List<int>? IntList { get; set; }

    [Body]
    public ItemType? ItemType { get; set; }

    [Body]
    public List<ItemType>? ItemTypeList { get; set; }
}

[Route($"{SpotifyClientBuildMessageTests.BaseUri}/test", Verb.Get)]
public class BodyParameterValueOnlyRequest
{
    [Body(WriteValueOnly = true)]
    public string? StringBody { get; set; }

    [Body(WriteValueOnly = true)]
    public List<string>? StringListBody { get; set; }
}