using Spotify.Core.Attributes;
using Spotify.Core.Model;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Spotify.Core;

public class SpotifyClient
{
    private readonly HttpClient _httpClient;

    private readonly string? _clientId;
    private readonly string? _clientSecret;
    private readonly string? _redirectUri;

    /// <summary>
    /// Initializes a new <see cref="SpotifyClient"/> with default settings
    /// </summary>
    /// <remarks>
    /// <see cref="CodeForAccessToken(string)"/> and <see cref="ValidateAccessToken(Token)"/> will be unavailable.
    /// To enable, please use the <see cref="SpotifyClient"/> constructor with a client id, client secret, and redirect uri.
    /// </remarks>
    public SpotifyClient()
    {
        _httpClient = new HttpClient();
    }

    public SpotifyClient(string clientId, string clientSecret, string redirectUri)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
        _redirectUri = redirectUri;

        _httpClient = new HttpClient();
    }

    public Action<HttpRequestMessage>? LogRequests { get; set; }

    public Action<HttpRequestMessage, HttpResponseMessage>? LogResponses { get; set; }

    public Action<HttpRequestMessage, HttpResponseMessage, Exception>? LogErrors { get; set; }

    public TResponse? Invoke<TResponse>(IReturn<TResponse> requestDto, string? bearerToken = null)
    {
        var httpRequest = BuildMessage(requestDto);

        if (bearerToken is not null)
        {
            httpRequest.Headers.Add("Authorization", "Bearer " + bearerToken);
        }

        LogRequests?.Invoke(httpRequest);

        if (httpRequest.Content is not null)
        {
            var task = httpRequest.Content.ReadAsStringAsync();
            task.Wait();
        }

        return GetResponse<TResponse>(httpRequest);
    }

    public List<TOut> InvokePagable<TResponse, TOut>(IReturnPagable<TResponse> request, Func<TResponse, Pagable<TOut>> func, string bearerToken)
    {
        var list = new List<TOut>();
        var response = default(TResponse);
        var pager = default(Pagable<TOut>);

        do
        {
            // Apply default offset if none is specified
            request.Offset = request.Offset ?? 0;

            // Call API
            response = Invoke(request, bearerToken);

            // Extract the Pagable<TOut>
            pager = func(response!);

            // Append results to list
            list.AddRange(pager.Items!);

            // Incrememt the offset for next request
            request.Offset += pager.Limit;

            //Console.WriteLine("Offset: {0}, Limit: {1}, Total: {2}", pager.Offset, pager.Limit, pager.Total);
        } while (pager.Next is not null && list.Count < pager.Total);
        return list;
    }

    private TResponse? GetResponse<TResponse>(HttpRequestMessage httpRequest)
    {
        var httpResponse = _httpClient.Send(httpRequest);

        if (httpResponse.IsSuccessStatusCode)
        {
            var json = httpResponse.Content.ReadAsStringAsync().Result;

            LogResponses?.Invoke(httpRequest, httpResponse);

            try
            {
                var dto = JsonSerializer.Deserialize<TResponse>(json, Configuration.JsonSerializerOptions);
                return dto;
            }
            catch (Exception ex)
            {
                LogErrors?.Invoke(httpRequest, httpResponse, ex);
                throw;
            }
        }
        else
        {
            var response = httpResponse.Content.ReadAsStringAsync().Result;
            var error = JsonSerializer.Deserialize<ErrorWrapper>(response, options: Configuration.JsonSerializerOptions);

            var ex = new Exception(error?.Error?.Message);
            LogErrors?.Invoke(httpRequest, httpResponse, ex);
            throw ex;
        }
    }

    internal static HttpRequestMessage BuildMessage<T>(T requestDto, string? bearerToken = null)
    {
        var type = requestDto?.GetType()!;
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
            else if (property.GetCustomAttribute<BodyAttribute>() is BodyAttribute bodyParameter)
            {
                var propertyValue = property.GetValue(requestDto);

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
                        var uriParameterName = Configuration.JsonNamingPolicy?.ConvertName(bodyParameter.Alias ?? property.Name);
                        var uriParameterValue = propertyValue.GetUriParameterValue();

                        // Disable the below warning as no combination seems to correct this error even though this will not be null here
                        // Argument cannot be used for parameter due to differences in the nullability of reference types.
                    #pragma warning disable CS8620
                        expandoObject?.TryAdd(uriParameterName, propertyValue);
                        #pragma warning restore CS8620
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
                    uri += $"{uriParameterName}={uriParameterValue}&";
                }
            }
        }

        if (httpRequestMessage.Content is null && expandoObject.Count() > 0)
        {
            httpRequestMessage.Content = JsonContent.Create(expandoObject, options: Configuration.JsonSerializerOptions);
        }

        httpRequestMessage.RequestUri = new Uri(uri.TrimEnd('&', '?'));

        return httpRequestMessage;
    }











    #region Spotify Tokens
    public class Token
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public int? ExpiresIn { get; set; }

        public DateTime? Expiration { get; set; }

        public bool IsExpired() => DateTime.Now >= Expiration;
    }

    [Route($"{Configuration.TokenUri}", Verb.Post)]
    public class ExchangeCodeForToken : IReturn<Token>
    {
        public string? GrantType { get; set; }

        public string? Code { get; set; }

        public string? RedirectUri { get; set; }
    }

    [Route($"{Configuration.TokenUri}", Verb.Post)]
    public class RefreshTokenForToken : IReturn<Token>
    {
    }

    public Token? CodeForAccessToken(string code)
    {
        var request = new ExchangeCodeForToken
        {
            Code = code,
            GrantType = "authorization_code",
            RedirectUri = _redirectUri
        };

        var message = BuildMessage(request);
        message.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}")));
        message.Content = new StringContent(string.Empty, Encoding.Default, "application/x-www-form-urlencoded");

        var token = GetResponse<Token>(message);
        ApplyTokenExpiration(token);
        return token;
    }

    public Token? RefreshToken(string refreshToken)
    {
        var request = new RefreshTokenForToken();

        var message = BuildMessage(request);
        message.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}")));
        message.Content = new StringContent($"grant_type=refresh_token&refresh_token={refreshToken}", Encoding.Default, "application/x-www-form-urlencoded");

        var token = GetResponse<Token>(message);
        ApplyTokenExpiration(token);
        return token;
    }

    private void ApplyTokenExpiration(Token? token)
    {
        if (token?.ExpiresIn is not null)
        {
            token.Expiration = DateTime.Now.AddSeconds((double)token.ExpiresIn);
        }
    }
    #endregion
}
