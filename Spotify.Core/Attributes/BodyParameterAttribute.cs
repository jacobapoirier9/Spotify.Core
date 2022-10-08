namespace Spotify.Core.Attributes;

public class BodyParameterAttribute : Attribute 
{
    public bool IncludePropertyName { get; private set; }

    public BodyParameterAttribute(bool includePropertyName = false)
    {
        IncludePropertyName = includePropertyName;
    }
}