using Spotify.Core;
using Spotify.Core.Attributes;
using Spotify.Core.Model;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Spotify.ConsoleApp;

public class BodyParameter2Attribute : Attribute
{
    public bool WriteValueAsBody { get; private set; }

    public BodyParameter2Attribute(bool writeValueAsBody = false)
    {
        WriteValueAsBody = writeValueAsBody;
    }
}

[Route("https://test.jake.com/test/{Id}", Verb.Get)]
public class Test
{
    public string? Id { get; set; }

    [BodyParameter2(true)]
    public string? Name { get; set; }
}

internal class Program
{
    private const string Token = "BQBz9fq24wob2DlDbl8iHyBif9iPYSbCoQrETPuoTVIp4M9_8Q1iVzq5GjE4PV_C5QmPFhczuMVk0yPLr4ejezvJ0DOXDmIeUv-H642_lMPpFRQRRx6_A6-L7IHAOwmBbiLjZ7k2nfF9KbwF83EpivFk8RCfiNUSHkOPhyc3sUfv9gmak1vUlfzETeux7oPoeT7UaG036Iw5D19LLf8AgvIHXRWezTVm7IP_41wFPffqWM-Is2VZZa_woZQ3Z8XtoVd1rfnlizNxufKHriShg-Qsk-kxUsEv8Yt_kFCcg2WdSzP3VKLkZPQ4l0pk8m5s";

    static void Main(string[] args)
    {
        var client = new SpotifyClient();

        var request = new Test
        {
            Id = "hello",
            Name = "jake"
        };

        var message = Test(request);

        Console.WriteLine(message.RequestUri.ToString());

        using (var stream = message.Content.ReadAsStream())
        using (var reader = new StreamReader(stream))
        {
            Console.WriteLine(reader.ReadToEnd());
        }
    }

    internal static HttpRequestMessage Test<T>(T requestDto, string? bearerToken = null)
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
                    if (bodyParameter.WriteValueAsBody)
                    {
                        if (httpRequestMessage.Content is not null)
                            throw new ApplicationException($"You may only specificy one property with {nameof(BodyParameter2Attribute)}.{nameof(BodyParameter2Attribute.WriteValueAsBody)} on type {type.FullName} set to {true}");

                        httpRequestMessage.Content = JsonContent.Create(propertyValue, options: Configuration.JsonSerializerOptions);
                    }
                    else
                    {
                        var uriParameterName = Configuration.JsonNamingPolicy?.ConvertName(property.Name);
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

        httpRequestMessage.RequestUri = new Uri(uri);

        return httpRequestMessage;
    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}