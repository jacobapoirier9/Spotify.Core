using Spotify.Core.Model;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Spotify.Core;

public static class Configuration
{
    public static readonly JsonSerializerOptions JsonSerializerOptions;

    static Configuration()
    {
        JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SpotifyJsonNamingPolicy()
        };

        JsonSerializerOptions.Converters.Add(new ItemTypeJsonConverter());
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

    public class ItemTypeJsonConverter : JsonConverter<ItemType>
    {
        public override ItemType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var token = reader.GetString();
            var itemType = FromStringToItemType(token, options);
            return itemType;
        }

        public override void Write(Utf8JsonWriter writer, ItemType itemType, JsonSerializerOptions options)
        {
            var token = FromItemTypeToString(itemType, options);
            writer.WriteStringValue(token);
        }

        internal ItemType FromStringToItemType(string? from, JsonSerializerOptions options)
        {
            if (from is null)
                throw new NullReferenceException($"A value for {nameof(from)} must be provided");

            // Need to convert to PascalCase for Enum.Parse to work correctly
            var pascalCase = options.PropertyNamingPolicy?.ConvertName(from) ?? from;
            var itemType = (ItemType)Enum.Parse(typeof(ItemType), pascalCase);
            return itemType;
        }

        internal string FromItemTypeToString(ItemType value, JsonSerializerOptions options)
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

            // Need to convert to snake case
            var convertedValue = options.PropertyNamingPolicy?.ConvertName(stringValue) ?? stringValue;
            return convertedValue;
        }
    }
}