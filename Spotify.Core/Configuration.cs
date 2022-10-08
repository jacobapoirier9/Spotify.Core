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
}