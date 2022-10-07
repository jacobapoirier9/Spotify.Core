using Spotify.Core.Model;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Spotify.Core;

public class ItemTypeJsonConverter : JsonConverter<ItemType>
{
    public override ItemType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var currentToken = reader.GetString();
#pragma warning disable CS8604 // Possible null reference argument.
        var converted = options.PropertyNamingPolicy?.ConvertName(currentToken) ?? currentToken;
        var itemType = (ItemType)Enum.Parse(typeof(ItemType), converted);
#pragma warning restore CS8604 // Possible null reference argument.
        return itemType;
    }

    public override void Write(Utf8JsonWriter writer, ItemType value, JsonSerializerOptions options)
    {
        var stringValue = value switch
        {
            ItemType.Track => nameof(ItemType.Track),
            ItemType.Album => nameof(ItemType.Album),
            ItemType.Artist => nameof(ItemType.Artist),
            ItemType.Playlist => nameof(ItemType.Playlist),
            ItemType.User => nameof(ItemType.User),

            _ => throw new IndexOutOfRangeException(nameof(ItemType))
        };

        var convertedValue = options.PropertyNamingPolicy.ConvertName(stringValue);
        writer.WriteStringValue(convertedValue);
    }
}