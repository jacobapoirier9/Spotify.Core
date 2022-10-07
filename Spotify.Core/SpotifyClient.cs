using Spotify.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spotify.Core;

public class SpotifyClient
{
    private readonly string _baseUri;
    private readonly HttpClient _httpClient;

    public SpotifyClient(string baseUri)
    {
        _baseUri = baseUri;
        _httpClient = new HttpClient();
    }


    public TResponse? Get<TResponse>(IReturn<TResponse> requestDto, string? bearerToken = null)
        => GetHttpResponse(requestDto, bearerToken);

    public TResponse? Put<TResponse>(IReturn<TResponse> requestDto, string? bearerToken = null)
        => GetHttpResponse(requestDto, bearerToken);

    public TResponse? Delete<TResponse>(IReturn<TResponse> requestDto, string? bearerToken = null)
        => GetHttpResponse(requestDto, bearerToken);

    private TResponse? GetHttpResponse<TResponse>(IReturn<TResponse> requestDto, string? bearerToken = null)
    {
        var httpRequest = HttpRequestMessageBuilder.BuildRequestMessage(_baseUri, requestDto, bearerToken);

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
            catch
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
}
