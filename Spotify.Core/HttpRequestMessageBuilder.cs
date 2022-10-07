using System.Net.Http.Json;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Spotify.Core;

internal static class HttpRequestMessageBuilder
{
    internal static HttpRequestMessage BuildRequestMessage<T>(string baseUri, T requestDto, string? bearerToken = null)
    {
        var type = typeof(T);
        var route = type.GetCustomAttribute<RouteAttribute>() ?? throw new NullReferenceException($"You must specificy a {nameof(RouteAttribute)} on type {type.FullName}.");

        var httpMethod = route.Verb switch
        {
            Verb.Get => HttpMethod.Get,
            Verb.Post => HttpMethod.Post,
            _ => HttpMethod.Get
        };

        var request = new HttpRequestMessage(httpMethod, string.Empty);

        if (bearerToken is not null)
        {
            request.Headers.Add("Authorization", "Bearer " + bearerToken);
        }

        var relativePath = route?.Path + "?";

        // Sometimes the query parameter needs to be inserted in the url.
        // /me/{Id}/salray will take the Id property on the request dto and replace {Id} with the value. 
        // if Id = 4, then the result would be /me/4/salary
        var propertyNamesToInsert =
            Regex.Matches(relativePath, @"{\w*}")
            .Cast<Match>()
            .Select(m => m.Value)
            .ToList();

        foreach (var property in type.GetProperties())
        {
            // Parameter is intended to be the request body
            if (property.GetCustomAttribute<BodyParameterAttribute>() is not null)
            {
                // Make sure the previous body does not get overwritten.
                if (request.Content is not null)
                {
                    throw new ApplicationException($"Only one {nameof(BodyParameterAttribute)} can be specificed on type {type.Name}");
                }

                request.Content = JsonContent.Create(property.GetValue(requestDto), options: Configuration.JsonSerializerOptions);
            }
            // Parameter is intended to be a part of the URI
            else
            {
                var value = property.GetValue(requestDto);
                if (value is not null)
                {
                    if (propertyNamesToInsert.Contains("{" + property.Name + "}"))
                    {
                        relativePath = relativePath.Replace("{" + property.Name + "}", value.GetUriParameterValue());
                    }
                    else
                    {
                        var name = Configuration.JsonSerializerOptions?.PropertyNamingPolicy?.ConvertName(property.Name) ?? property.Name;
                        relativePath += name + "=" + value.GetUriParameterValue() + "&";
                    }

                }
            }
        }

        request.RequestUri = new Uri(baseUri + relativePath.TrimEnd('&', '?'));

        return request;
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