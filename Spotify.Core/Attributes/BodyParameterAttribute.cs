namespace Spotify.Core.Attributes;

public class BodyParameterAttribute : Attribute 
{
    public bool IncludePropertyName { get; private set; }

    public BodyParameterAttribute(bool includePropertyName = false)
    {
        IncludePropertyName = includePropertyName;
    }
}

public class BodyParameter2Attribute : Attribute
{
    public bool WriteValueAsBody { get; private set; }

    public BodyParameter2Attribute(bool writeValueAsBody = false)
    {
        WriteValueAsBody = writeValueAsBody;
    }
}