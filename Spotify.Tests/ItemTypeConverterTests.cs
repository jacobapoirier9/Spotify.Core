using System.Text.Json;
using Spotify.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spotify.Core.Model;

namespace Spotify.Tests;

public class ItemTypeConverterTests
{
    private readonly Configuration.ItemTypeJsonConverter _itemTypeConverter = new Configuration.ItemTypeJsonConverter();

    [Theory]
    [InlineData(ItemType.Track, "track")]
    [InlineData(ItemType.User, "user")]
    [InlineData(ItemType.Artist, "artist")]
    [InlineData(ItemType.Album, "album")]
    [InlineData(ItemType.Playlist, "playlist")]
    public void FromItemTypeToString_Success(ItemType input, string expected)
    {
        var actual = _itemTypeConverter.FromItemTypeToString(input, Configuration.JsonSerializerOptions);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("track", ItemType.Track)]
    [InlineData("user", ItemType.User)]
    [InlineData("artist", ItemType.Artist)]
    [InlineData("album", ItemType.Album)]
    [InlineData("playlist", ItemType.Playlist)]
    public void FromStringToItemType_Success(string input, ItemType expected)
    {
        var actual = _itemTypeConverter.FromStringToItemType(input, Configuration.JsonSerializerOptions);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("failure")]
    public void FromStringToItemType_Failure(string input)
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            var actual = _itemTypeConverter.FromStringToItemType(input, Configuration.JsonSerializerOptions);
        });
    }
}