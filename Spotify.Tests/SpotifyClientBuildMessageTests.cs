using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Tests;

public class HttpMessageBuilderTests
{
    internal const string BaseUri = "https://jake.test.com";

    [Fact]
    public void BuildsCorrectRequest_QueryParametersOnly()
    {
        var request = new QueryParametersOnly
        {
            Id = "testid",
            Limit = 20
        };

        var httpRequest = SpotifyClient.BuildRequestMessage(request);

        Assert.NotNull(httpRequest);
        Assert.Null(httpRequest.Content);

        Assert.Equal($"{BaseUri}/test?id=testid&limit=20", httpRequest.RequestUri?.ToString());
    }

    [Fact]
    public void BuildsCorrectRequest_SimpleBodyParametersOnly()
    {
        var request = new SimpleBodyParametersOnly
        {
            Ids = new List<string> { "id1", "id2", "id3" }
        };

        var httpRequest = SpotifyClient.BuildRequestMessage(request);

        Assert.NotNull(httpRequest);
        Assert.Equal($"{BaseUri}/test", httpRequest.RequestUri?.ToString());

        Assert.NotNull(httpRequest.Content);
        using (var stream = httpRequest.Content?.ReadAsStream())
        using (var reader = new StreamReader(stream ?? throw new NullReferenceException(nameof(stream))))
        {
            var value = reader.ReadToEnd();
            Assert.Equal("[\"id1\",\"id2\",\"id3\"]", value);
        }
    }

    [Fact]
    public void ThrowsException_HasTwoBodyParameters()
    {
        var request = new HasTwoBodyParameters
        {
            Id = "not",
            Name = "used"
        };

        Assert.ThrowsAny<Exception>(() =>
        {
            var httpRequest = SpotifyClient.BuildRequestMessage(request);
        });
    }

    [Fact]
    public void ThrowsExcepction_MissingRouteAttribute()
    {
        var request = new MissingRouteAttribute();

        Assert.ThrowsAny<Exception>(() =>
        {
            var httpRequest = SpotifyClient.BuildRequestMessage(request);
        });
    }

    [Fact]
    public void BuildsCorrectRequest_WritesItemTypeToQuery()
    {
        var request = new HasItemTypeInQuery
        {
            Type = ItemType.Track
        };

        var httpRequest = SpotifyClient.BuildRequestMessage(request);

        Assert.NotNull(httpRequest);
        Assert.Equal($"{BaseUri}/test?type=track", httpRequest.RequestUri?.ToString());

        Assert.Null(httpRequest.Content);
    }

    [Fact]
    public void BuildsCorrectRequest_WritesItemTypeToBody()
    {
        var request = new HasItemTypeInBody
        {
            Types = new List<ItemType> { ItemType.Track, ItemType.Playlist }
        };

        var httpRequest = SpotifyClient.BuildRequestMessage(request);

        Assert.NotNull(httpRequest);
        Assert.Equal($"{BaseUri}/test", httpRequest.RequestUri?.ToString());

        Assert.NotNull(httpRequest.Content);
        using (var stream = httpRequest.Content?.ReadAsStream())
        using (var reader = new StreamReader(stream ?? throw new NullReferenceException(nameof(stream))))
        {
            var value = reader.ReadToEnd();
            Assert.Equal("[\"track\",\"playlist\"]", value);
        }
    }

    [Fact]
    public void BuildsCorrectRequest_WritesComplexRequest()
    {
        var request = new ComplexRequest
        {
            Id = "testid",
            Body = new()
            {
                Type = ItemType.Playlist,
                AdditionalTypes = new List<ItemType> { ItemType.Track, ItemType.Artist },
                Name = "jake",
                Limit = 10,
                ContinuePlaying = true
            }
        };

        var httpRequest = SpotifyClient.BuildRequestMessage(request);

        Assert.NotNull(httpRequest);
        Assert.Equal($"{BaseUri}/test/testid", httpRequest.RequestUri?.ToString());

        Assert.NotNull(httpRequest.Content);
        using (var stream = httpRequest.Content?.ReadAsStream())
        using (var reader = new StreamReader(stream ?? throw new NullReferenceException(nameof(stream))))
        {
            var value = reader.ReadToEnd();
            Assert.Equal(@"{""name"":""jake"",""limit"":10,""type"":""playlist"",""additional_types"":[""track"",""artist""],""continue_playing"":true}", value);
        }
    }
}

[Route($"{HttpMessageBuilderTests.BaseUri}/test", Verb.Get)]
public class QueryParametersOnly
{
    public string? Id { get; set; }

    public int? Limit { get; set; }
}

[Route($"{HttpMessageBuilderTests.BaseUri}/test", Verb.Get)]
public class SimpleBodyParametersOnly
{
    [BodyParameter]
    public List<string>? Ids { get; set; }
}

[Route($"{HttpMessageBuilderTests.BaseUri}/test", Verb.Get)]
public class HasTwoBodyParameters
{
    [BodyParameter]
    public string? Id { get; set; }

    [BodyParameter]
    public string? Name { get; set; }
}

public class MissingRouteAttribute
{

}

[Route($"{HttpMessageBuilderTests.BaseUri}/test", Verb.Get)]
public class HasItemTypeInQuery
{
    public ItemType Type { get; set; }
}

[Route($"{HttpMessageBuilderTests.BaseUri}/test", Verb.Get)]
public class HasItemTypeInBody
{
    [BodyParameter]
    public List<ItemType>? Types { get; set; }
}

[Route($"{HttpMessageBuilderTests.BaseUri}/test/{{{nameof(Id)}}}", Verb.Get)]
public class ComplexRequest
{
    public string? Id { get; set; }

    [BodyParameter]
    public BodyObject? Body { get; set; }

    public class BodyObject
    {
        public string? Name { get; set; }

        public int? Limit { get; set; }

        public ItemType? Type { get; set; }

        public List<ItemType>? AdditionalTypes { get; set; }

        public bool? ContinuePlaying { get; set; }
    }
}