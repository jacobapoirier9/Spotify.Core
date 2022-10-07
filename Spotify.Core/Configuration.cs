using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Spotify.Core;

public static class Configuration
{
    [NotNull]
    public static readonly JsonSerializerOptions JsonSerializerOptions;

    static Configuration()
    {
        JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SpotifyJsonNamingPolicy()
        };

        //JsonSerializerOptions.Converters.Add(new ItemTypeJsonConverter());
    }

    public class SpotifyJsonNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (Regex.IsMatch(name, "^[A-Z]"))
            {
                var snakeCase = name.FromPascalToSnake();
                return snakeCase;
            }
            else if (Regex.IsMatch(name, "^[a-z]"))
            {
                var pascalCase = name.FromSnakeToPascal();
                return pascalCase;
            }

            return name;
        }
    }

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