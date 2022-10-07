namespace Spotify.Core;

internal static class Verb
{
    public const string Get = nameof(Get);
    public const string Post = nameof(Post);
}


//public class ItemTypeJsonConverter : JsonConverter<ItemType>
//{
//    public override ItemType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        var currentToken = reader.GetString();
//        var converted = options.PropertyNamingPolicy.ConvertName(currentToken);
//        var itemType = (ItemType)Enum.Parse(typeof(ItemType), converted);
//        return itemType;
//    }

//    public override void Write(Utf8JsonWriter writer, ItemType value, JsonSerializerOptions options)
//    {
//        var stringValue = value switch
//        {
//            ItemType.Track => nameof(ItemType.Track),
//            ItemType.Album => nameof(ItemType.Album),
//            ItemType.Artist => nameof(ItemType.Artist),
//            ItemType.Playlist => nameof(ItemType.Playlist),
//            ItemType.User => nameof(ItemType.User),

//            _ => throw new IndexOutOfRangeException(nameof(ItemType))
//        };

//        var convertedValue = options.PropertyNamingPolicy.ConvertName(stringValue);
//        writer.WriteStringValue(convertedValue);
//    }
//}