namespace Spotify.Core.Attributes;

public class BodyParameter2Attribute : Attribute
{
    public string? Alias { get; set; }
    public bool WriteValueOnly { get; set; } = false;

    public BodyParameter2Attribute()
    {
    }
}