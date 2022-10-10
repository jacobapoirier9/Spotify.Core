using Spotify.Core.Model;
using System.Collections;
using System.Text.RegularExpressions;

namespace Spotify.Core;

internal static class Helpers
{
    public static string JoinToString(this IEnumerable<IConvertible> items, IConvertible seperator)
    {
        string toReturn = string.Empty;
        foreach (IConvertible item in items)
        {
            toReturn += item.ToString() + seperator;
        }

        return toReturn == string.Empty ? string.Empty : toReturn.Substring(0, toReturn.Length - seperator.ToString().Length);
    }

    public static string GetUriParameterValue(this object value)
    {
        if (value is ItemType itemType)
        {
            return Configuration.ItemTypeConverter.FromItemTypeToString(itemType, Configuration.JsonSerializerOptions);
        }
        else if (value is IConvertible convertible)
        {
            return convertible?.ToString() ?? throw new NullReferenceException();
        }
        else if (value is IEnumerable enumerable)
        {
            IEnumerator enumerator = enumerable.GetEnumerator();
            List<string> strings = new();

            while (enumerator.MoveNext())
            {
                strings.Add(GetUriParameterValue(enumerator.Current));
            }

            return strings.JoinToString(",");
        }

        throw new ApplicationException($"Value {value} is not supported in uri query strings.");
    }

    public static string FromPascalToSnake(this string value)
    {
        string caseConvertedUnderscorePrefix = Regex.Replace(value, "[A-Z]{1}", m => $"_{m.Value.ToLower()}");
        return caseConvertedUnderscorePrefix.TrimStart('_');
    }

    public static string FromSnakeToPascal(this string snakeCaseValue)
    {
        string caseConverted = Regex.Replace(snakeCaseValue, "(^|_)[a-z]", match => match.Value.ToUpper());
        string underscoresRemoved = Regex.Replace(caseConverted, "_", match => string.Empty);

        return underscoresRemoved;
    }
}
