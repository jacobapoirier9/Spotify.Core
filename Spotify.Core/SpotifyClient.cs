using Spotify.Core.Attributes;
using Spotify.Core.Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spotify.Core;

public class SpotifyClient
{
    private readonly HttpClient _httpClient;

    public SpotifyClient()
    {
        _httpClient = new HttpClient();
    }

    public TResponse? Request<TResponse>(IReturn<TResponse> requestDto, string? bearerToken = null)
    {
        var httpRequest = BuildRequestMessageExperimental(requestDto, bearerToken);

        Console.WriteLine(httpRequest);
        if (httpRequest.Content is not null)
        {
            var task = httpRequest.Content.ReadAsStringAsync();
            task.Wait();

            Console.WriteLine(task.Result);
        }

        var httpResponse = _httpClient.Send(httpRequest);

        if (httpResponse.IsSuccessStatusCode)
        {
            var json = httpResponse.Content.ReadAsStringAsync().Result;
            Console.WriteLine(json);

            try
            {
                var dto = JsonSerializer.Deserialize<TResponse>(json, Configuration.JsonSerializerOptions);
                return dto;
            }
            catch (Exception ex)
            {
                return (TResponse)Convert.ChangeType(httpResponse.StatusCode, typeof(TResponse));
            }
        }
        else
        {
            var error = httpResponse.Content.ReadAsStringAsync().Result;
            Console.WriteLine(error);
            return default;
        }
    }

    internal static HttpRequestMessage BuildRequestMessageExperimental<T>(T requestDto, string? bearerToken = null)
    {
        var type = requestDto?.GetType();
        var properties = type.GetProperties().ToList();
        var route = type.GetCustomAttribute<RouteAttribute>();

        // Route validation
        if (route is null)
            throw new NullReferenceException($"You must specify a {nameof(RouteAttribute)} for request type {type.FullName}");

        if (route.Uri is null)
            throw new NullReferenceException($"You must specify {nameof(RouteAttribute.Uri)} for request type {type.FullName} on attribute {nameof(RouteAttribute)}");

        var httpMethod = route.Verb switch
        {
            Verb.Get => HttpMethod.Get,
            Verb.Post => HttpMethod.Post,
            Verb.Put => HttpMethod.Put,
            Verb.Delete => HttpMethod.Delete,

            _ => throw new NullReferenceException($"You must specify {nameof(RouteAttribute.Verb)} for request type {type.FullName} on attribute {nameof(RouteAttribute)}")
        };

        var httpRequestMessage = new HttpRequestMessage(httpMethod, string.Empty);

        var uri = route.Uri + "?";
        var expandoObject = new ExpandoObject();

        foreach (var property in properties)
        {
            var match = Regex.Match(uri, $"{{{property.Name}}}");
            if (match.Success)
            {
                var propertyValue = property.GetValue(requestDto);

                if (propertyValue is null)
                    throw new NullReferenceException($"{property.Name} is a required field for endpoint {route.Uri}");

                uri = uri.Replace($"{{{property.Name}}}", propertyValue.GetUriParameterValue());
            }
            else if (property.GetCustomAttribute<BodyParameter2Attribute>() is BodyParameter2Attribute bodyParameter)
            {
                var propertyValue = property.GetValue(requestDto);

                if (propertyValue is not null)
                {
                    if (bodyParameter.WriteValueOnly)
                    {
                        if (httpRequestMessage.Content is not null)
                            throw new ApplicationException($"You may only specificy one property with {nameof(BodyParameter2Attribute)}.{nameof(BodyParameter2Attribute.WriteValueOnly)} on type {type.FullName} set to {true}");

                        httpRequestMessage.Content = JsonContent.Create(propertyValue, options: Configuration.JsonSerializerOptions);
                    }
                    else
                    {
                        var uriParameterName = Configuration.JsonNamingPolicy?.ConvertName(bodyParameter.Alias ?? property.Name);
                        var uriParameterValue = propertyValue.GetUriParameterValue();
                        expandoObject?.TryAdd(uriParameterName, propertyValue);
                    }
                }
            }
            else
            {
                var uriParameterName = Configuration.JsonNamingPolicy?.ConvertName(property.Name);
                var propertyValue = property.GetValue(requestDto);

                if (propertyValue is not null)
                {
                    var uriParameterValue = propertyValue.GetUriParameterValue();
                    uri += $"{uriParameterName}={uriParameterValue}";
                }
            }
        }

        if (httpRequestMessage.Content is null && expandoObject.Count() > 0)
        {
            httpRequestMessage.Content = JsonContent.Create(expandoObject, options: Configuration.JsonSerializerOptions);
        }

        if (bearerToken is not null)
        {
            httpRequestMessage.Headers.Add("Authorization", "Bearer " + bearerToken);
        }

        httpRequestMessage.RequestUri = new Uri(uri.TrimEnd('&', '?'));

        return httpRequestMessage;
    }
}
