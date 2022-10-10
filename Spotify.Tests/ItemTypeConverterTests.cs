using Spotify.Core;
using Spotify.Core.Model;

namespace Spotify.Tests;

public class ItemTypeConverterTests
{
    private readonly Configuration.ItemTypeJsonConverter _itemTypeConverter = new();

    [Theory]
    [InlineData(ItemType.Track, "track")]
    [InlineData(ItemType.User, "user")]
    [InlineData(ItemType.Artist, "artist")]
    [InlineData(ItemType.Album, "album")]
    [InlineData(ItemType.Playlist, "playlist")]
    public void FromItemTypeToString_Success(ItemType input, string expected)
    {
        string actual = _itemTypeConverter.FromItemTypeToString(input, Configuration.JsonSerializerOptions);
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
        ItemType actual = _itemTypeConverter.FromStringToItemType(input, Configuration.JsonSerializerOptions);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("failure")]
    public void FromStringToItemType_Failure(string input)
    {
        _ = Assert.ThrowsAny<Exception>(() =>
        {
            ItemType actual = _itemTypeConverter.FromStringToItemType(input, Configuration.JsonSerializerOptions);
        });
    }
}