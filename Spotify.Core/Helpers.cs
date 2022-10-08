using Spotify.Core.Model;
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
        if (value is ItemType test1 && value is IConvertible test2)
        {

        }

        if (value is ItemType itemType)
        {
            return itemType.ToString().ToLower();
        }
        else if (value is IConvertible convertible)
        {
            return convertible?.ToString() ?? throw new NullReferenceException();
        }
        else if (value is IEnumerable rawEnumerable)
        {
            if (rawEnumerable.Cast<ItemType>() is IEnumerable<ItemType> itemTypes)
            {
                return itemTypes.Select(it => it.ToString().ToLower()).JoinToString(",");
            }
            else if (rawEnumerable.Cast<IConvertible>() is IEnumerable<IConvertible> castedEnumerable)
            {
                return castedEnumerable.JoinToString(",");
            }
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
