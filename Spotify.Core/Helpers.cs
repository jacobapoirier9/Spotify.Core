using Spotify.Core.Model;
using System.Collections;
using System.Text.RegularExpressions;

namespace Spotify.Core;

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }

    internal static string GetUriParameterValue(this object value)
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
            var enumerator = enumerable.GetEnumerator();
            List<string> strings = new();

            while (enumerator.MoveNext())
            {
                strings.Add(GetUriParameterValue(enumerator.Current));
            }

            return string.Join(',', strings);
        }

        throw new ApplicationException($"Value {value} is not supported in uri query strings.");
    }
}
