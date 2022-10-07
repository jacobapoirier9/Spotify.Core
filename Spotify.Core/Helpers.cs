using System.Collections;
using System.Text.RegularExpressions;

namespace Spotify.Core;

internal static class Helpers
{
    public static string JoinToString(this IEnumerable<IConvertible> items, IConvertible seperator)
    {
        var toReturn = string.Empty;
        foreach (var item in items)
        {
            toReturn += item.ToString() + seperator;
        }

        return toReturn == string.Empty ? string.Empty : toReturn.Substring(0, toReturn.Length - seperator.ToString().Length);
    }

    public static string GetUriParameterValue(this object value)
    {
        if (value is IConvertible convertible)
        {
            return convertible?.ToString() ?? throw new NullReferenceException();
        }
        else if (value is IEnumerable rawEnumerable && rawEnumerable.Cast<IConvertible>() is IEnumerable<IConvertible> castedEnumerable)
        {
            return castedEnumerable.JoinToString(",");
        }

        throw new ApplicationException($"Value {value} is not supported in uri query strings.");
    }

    public static string FromPascalToSnake(this string value)
    {
        var caseConvertedUnderscorePrefix = Regex.Replace(value, "[A-Z]{1}", m => $"_{m.Value.ToLower()}");
        return caseConvertedUnderscorePrefix.TrimStart('_');
    }

    public static string FromSnakeToPascal(this string snakeCaseValue)
    {
        var caseConverted = Regex.Replace(snakeCaseValue, "(^|_)[a-z]", match => match.Value.ToUpper());
        var underscoresRemoved = Regex.Replace(caseConverted, "_", match => string.Empty);

        return underscoresRemoved;
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