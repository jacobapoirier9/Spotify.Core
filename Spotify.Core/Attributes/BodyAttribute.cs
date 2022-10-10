namespace Spotify.Core.Attributes;

public class BodyAttribute : Attribute
{
    public string? Alias { get; set; }
    public bool WriteValueOnly { get; set; } = false;

    public BodyAttribute()
    {
    }
}