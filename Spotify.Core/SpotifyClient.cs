using Spotify.Core.Attributes;
using Spotify.Core.Model;
using System.Dynamic;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

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
        HttpRequestMessage httpRequest = BuildMessage(requestDto, bearerToken);

        Console.WriteLine(httpRequest);
        if (httpRequest.Content is not null)
        {
            Task<string> task = httpRequest.Content.ReadAsStringAsync();
            task.Wait();

            Console.WriteLine(task.Result);
        }

        HttpResponseMessage httpResponse = _httpClient.Send(httpRequest);

        if (httpResponse.IsSuccessStatusCode)
        {
            string json = httpResponse.Content.ReadAsStringAsync().Result;
            Console.WriteLine(json);
            try
            {
                TResponse? dto = JsonSerializer.Deserialize<TResponse>(json, Configuration.JsonSerializerOptions);
                return dto;
            }
            catch (Exception)
            {
                return (TResponse)Convert.ChangeType(httpResponse.StatusCode, typeof(TResponse));
            }
        }
        else
        {
            string error = httpResponse.Content.ReadAsStringAsync().Result;
            Console.WriteLine(error);
            return default;
        }
    }

    internal static HttpRequestMessage BuildMessage<T>(T requestDto, string? bearerToken = null)
    {
        Type? type = requestDto?.GetType();
        List<PropertyInfo> properties = type.GetProperties().ToList();
        RouteAttribute? route = type.GetCustomAttribute<RouteAttribute>();

        // Route validation
        if (route is null)
            throw new NullReferenceException($"You must specify a {nameof(RouteAttribute)} for request type {type.FullName}");

        if (route.Uri is null)
            throw new NullReferenceException($"You must specify {nameof(RouteAttribute.Uri)} for request type {type.FullName} on attribute {nameof(RouteAttribute)}");

        HttpMethod httpMethod = route.Verb switch
        {
            Verb.Get => HttpMethod.Get,
            Verb.Post => HttpMethod.Post,
            Verb.Put => HttpMethod.Put,
            Verb.Delete => HttpMethod.Delete,

            _ => throw new NullReferenceException($"You must specify {nameof(RouteAttribute.Verb)} for request type {type.FullName} on attribute {nameof(RouteAttribute)}")
        };

        HttpRequestMessage httpRequestMessage = new(httpMethod, string.Empty);

        string uri = route.Uri + "?";
        ExpandoObject? expandoObject = new();

        foreach (PropertyInfo? property in properties)
        {
            Match match = Regex.Match(uri, $"{{{property.Name}}}");
            if (match.Success)
            {
                object? propertyValue = property.GetValue(requestDto);

                if (propertyValue is null)
                    throw new NullReferenceException($"{property.Name} is a required field for endpoint {route.Uri}");

                uri = uri.Replace($"{{{property.Name}}}", propertyValue.GetUriParameterValue());
            }
            else if (property.GetCustomAttribute<BodyAttribute>() is BodyAttribute bodyParameter)
            {
                object? propertyValue = property.GetValue(requestDto);

                if (propertyValue is not null)
                {
                    if (bodyParameter.WriteValueOnly)
                    {
                        if (httpRequestMessage.Content is not null)
                            throw new ApplicationException($"You may only specificy one property with {nameof(BodyAttribute)}.{nameof(BodyAttribute.WriteValueOnly)} on type {type.FullName} set to {true}");

                        httpRequestMessage.Content = JsonContent.Create(propertyValue, options: Configuration.JsonSerializerOptions);
                    }
                    else
                    {
                        string? uriParameterName = Configuration.JsonNamingPolicy?.ConvertName(bodyParameter.Alias ?? property.Name);
                        string uriParameterValue = propertyValue.GetUriParameterValue();
                        _ = (expandoObject?.TryAdd(uriParameterName, propertyValue));
                    }
                }
            }
            else
            {
                string? uriParameterName = Configuration.JsonNamingPolicy?.ConvertName(property.Name);
                object? propertyValue = property.GetValue(requestDto);

                if (propertyValue is not null)
                {
                    string uriParameterValue = propertyValue.GetUriParameterValue();
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
