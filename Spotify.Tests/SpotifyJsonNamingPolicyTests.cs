using static Spotify.Core.Configuration;

namespace Spotify.Tests;

public class SpotifyJsonNamingPolicyTests
{
    private readonly SpotifyJsonNamingPolicy _namingPolicy = new();

    [Theory]
    [InlineData("Name", "name")]
    [InlineData("FullName", "full_name")]
    [InlineData("name", "Name")]
    [InlineData("full_name", "FullName")]
    [InlineData("super_long_for_no_reason", "SuperLongForNoReason")]
    [InlineData("SuperLongForNoReason", "super_long_for_no_reason")]
    [InlineData("camelCase", "CamelCase")]
    public void TestConvertName(string input, string expected)
    {
        string actual = _namingPolicy.ConvertName(input);
        Assert.Equal(expected, actual);
    }
}