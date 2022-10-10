using Spotify.Core.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Spotify.Core;

public static class Configuration
{
    public const string ApiUri = "https://api.spotify.com/v1";

    public static readonly JsonSerializerOptions JsonSerializerOptions;
    public static readonly ItemTypeJsonConverter ItemTypeConverter;
    public static readonly SpotifyJsonNamingPolicy JsonNamingPolicy;

    static Configuration()
    {
        JsonNamingPolicy = new SpotifyJsonNamingPolicy();
        ItemTypeConverter = new ItemTypeJsonConverter();


        JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy
        };

        JsonSerializerOptions.Converters.Add(ItemTypeConverter);
    }

    public class SpotifyJsonNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (Regex.IsMatch(name, "^[A-Z]"))
            {
                string snakeCase = name.FromPascalToSnake();
                return snakeCase;
            }
            else if (Regex.IsMatch(name, "^[a-z]"))
            {
                string pascalCase = name.FromSnakeToPascal();
                return pascalCase;
            }

            return name;
        }
    }

    public class ItemTypeJsonConverter : JsonConverter<ItemType>
    {
        public override ItemType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? token = reader.GetString();
            ItemType itemType = FromStringToItemType(token, options);
            return itemType;
        }

        public override void Write(Utf8JsonWriter writer, ItemType itemType, JsonSerializerOptions options)
        {
            string token = FromItemTypeToString(itemType, options);
            writer.WriteStringValue(token);
        }

        internal ItemType FromStringToItemType(string? from, JsonSerializerOptions options)
        {
            if (from is null)
                throw new NullReferenceException($"A value for {nameof(from)} must be provided");

            // Need to convert to PascalCase for Enum.Parse to work correctly
            string pascalCase = options.PropertyNamingPolicy?.ConvertName(from) ?? from;
            ItemType itemType = (ItemType)Enum.Parse(typeof(ItemType), pascalCase);
            return itemType;
        }

        internal string FromItemTypeToString(ItemType value, JsonSerializerOptions options)
        {
            string stringValue = value switch
            {
                ItemType.Track => nameof(ItemType.Track),
                ItemType.Album => nameof(ItemType.Album),
                ItemType.Artist => nameof(ItemType.Artist),
                ItemType.Playlist => nameof(ItemType.Playlist),
                ItemType.User => nameof(ItemType.User),

                _ => throw new IndexOutOfRangeException(nameof(ItemType))
            };

            // Need to convert to snake case
            string convertedValue = options.PropertyNamingPolicy?.ConvertName(stringValue) ?? stringValue;
            return convertedValue;
        }
    }
}