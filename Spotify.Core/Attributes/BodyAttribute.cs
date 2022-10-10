namespace Spotify.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class BodyAttribute : Attribute
{
    /// <summary>
    /// Serializes to the request property with the alias rather than the property name
    /// </summary>
    public string? Alias { get; set; }

    /// <summary>
    /// Set to true to serialize as <code>value</code>
    /// Set to false to serialize as <code>name: value</code>
    /// Default is false
    /// </summary>
    public bool WriteValueOnly { get; set; } = false;
}