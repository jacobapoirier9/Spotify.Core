using System.Dynamic;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Spotify.Core.Attributes;

namespace Spotify.Core;

internal static class HttpRequestMessageBuilder
{
    internal static HttpRequestMessage BuildRequestMessage<T>(T requestDto, string? bearerToken = null)
    {
        // TODO: Using typeof(T) here does not correctly get the RouteAttribute
        var type = requestDto?.GetType();
        var route = type.GetCustomAttribute<RouteAttribute>() ?? throw new NullReferenceException($"You must specificy a {nameof(RouteAttribute)} on type {type.FullName}.");

        var httpMethod = route.Verb switch
        {
            Verb.Get => HttpMethod.Get,
            Verb.Post => HttpMethod.Post,
            Verb.Put => HttpMethod.Put,
            Verb.Delete => HttpMethod.Delete,
            _ => HttpMethod.Get
        };

        var request = new HttpRequestMessage(httpMethod, string.Empty);

        if (bearerToken is not null)
        {
            request.Headers.Add("Authorization", "Bearer " + bearerToken);
        }

        var url = route?.Uri + "?";

        // Sometimes the query parameter needs to be inserted in the url.
        // /me/{Id}/salray will take the Id property on the request dto and replace {Id} with the value. 
        // if Id = 4, then the result would be /me/4/salary
        var propertyNamesToInsert =
            Regex.Matches(url, @"{\w*}")
            .Cast<Match>()
            .Select(m => m.Value)
            .ToList();

        foreach (var property in type.GetProperties())
        {
            var bodyParameter = property.GetCustomAttribute<BodyParameterAttribute>();

            // Parameter is intended to be the request body
            if (bodyParameter is not null)
            {
                // Make sure the previous body does not get overwritten.
                if (request.Content is not null)
                {
                    throw new ApplicationException($"Only one {nameof(BodyParameterAttribute)} can be specificed on type {type.Name}");
                }

                if (bodyParameter.IncludePropertyName)
                {
                    var dataMember = property.GetCustomAttribute<DataMemberAttribute>();

                    var anon = new ExpandoObject();
                    var convertedName = Configuration.JsonNamingPolicy.ConvertName(dataMember?.Name ?? property.Name);

                    anon.TryAdd(convertedName, property.GetValue(requestDto));

                    request.Content = JsonContent.Create(anon, options: Configuration.JsonSerializerOptions);
                }
                else
                {
                    request.Content = JsonContent.Create(property.GetValue(requestDto), options: Configuration.JsonSerializerOptions);
                }
            }
            // Parameter is intended to be a part of the URI
            else
            {
                var value = property.GetValue(requestDto);
                if (value is not null)
                {
                    if (propertyNamesToInsert.Contains("{" + property.Name + "}"))
                    {
                        url = url.Replace("{" + property.Name + "}", value.GetUriParameterValue());
                    }
                    else
                    {
                        var name = Configuration.JsonNamingPolicy.ConvertName(property.Name) ?? property.Name;
                        url += name + "=" + value.GetUriParameterValue() + "&";
                    }

                }
            }
        }

        request.RequestUri = new Uri(url.TrimEnd('&', '?'));

        Console.WriteLine(request);
        if (request.Content is not null)
        {
            var task = request.Content.ReadAsStringAsync();
            task.Wait();

            Console.WriteLine(task.Result);
        }

        return request;
    }
}