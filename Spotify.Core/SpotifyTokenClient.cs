using System.Net;
using System.Text;
using System.Text.Json;

namespace Spotify.Core;

public class SpotifyTokenClient
{
    public class RawAccessTokenResponse
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public string? TokenType { get; set; }

        public double? ExpiresIn { get; set; }
    }

    public class SpotifyToken
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? Expiration { get; set; }

        public bool IsExpired() => DateTime.Now >= Expiration;
    }

    private const string _tokenUri = "https://accounts.spotify.com/api/token";

    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUri;

    public SpotifyTokenClient(string clientId, string clientSecret, string redirectUri)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
        _redirectUri = redirectUri;
    }

    public SpotifyToken CodeForAccessToken(string code)
    {
        var requestBody = new
        {
            grant_type = "authorization_code",
            code = code,
            redirect_uri = _redirectUri
        };

        var request = WebRequest.Create($"{_tokenUri}/?grant_type={requestBody.grant_type}&code={code}&redirect_uri={requestBody.redirect_uri}");
        request.Method = "POST";
        request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}")));

        request.ContentLength = string.Empty.Length;
        request.ContentType = "application/x-www-form-urlencoded";

        try
        {
            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var rawToken = JsonSerializer.Deserialize<RawAccessTokenResponse>(json, options: Configuration.JsonSerializerOptions);

                var token = new SpotifyToken
                {
                    AccessToken = rawToken.AccessToken,
                    RefreshToken = rawToken.RefreshToken,
                    Expiration = DateTime.Now.AddSeconds(rawToken?.ExpiresIn ?? 0)
                };

                //if (_configuration.GetValue<bool>("DeepLogging:API"))
                //{
                //    _logger.Trace("Token exchange was successful. New token: {Token}", rawToken.AccessToken);
                //}

                return token;
            }
        }
        catch (WebException ex)
        {
            using (var stream = ex.Response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEnd());
                return default;
                //var json = reader.ReadToEnd();
                //var error = JsonSerializer.Deserialize<ErrorWrapper>(json).Error;

                ////_logger.Error("Failed to get a spotify token. Spotify Error: {Error}", error?.Message ?? "None");

                //throw new WebException(ex.Message);
            }
        }
    }

    public SpotifyToken ValidateAccessToken(SpotifyToken token)
    {
        if (token.IsExpired())
        {
            var requestObj = new
            {
                grant_type = "refresh_token",
                refresh_token = token.RefreshToken
            };

            var request = WebRequest.Create(_tokenUri);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_clientId}:{_clientSecret}")));

            var body = $"grant_type=request_token&refresh_token=" + token.RefreshToken;

            request.ContentLength = body.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var stream = request.GetRequestStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(body);
            }

            try
            {
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    var rawToken = JsonSerializer.Deserialize<RawAccessTokenResponse>(json);

                    token = new SpotifyToken
                    {
                        AccessToken = rawToken.AccessToken,
                        RefreshToken = rawToken.RefreshToken,
                        Expiration = DateTime.Now.AddSeconds(rawToken?.ExpiresIn ?? 0)
                    };

                    //_logger.Trace("Refresh for token exchange was successful. New token: {Token}", token.AccessToken);

                    return token;
                }
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                    return default;
                    //var json = reader.ReadToEnd();
                    //var error = JsonSerializer.Deserialize<ErrorWrapper>(json).Error;

                    ////_logger.Error("Failed to get a spotify token. Spotify Error: {Error}", error.Message ?? "None");

                    //throw new WebException(ex.Message);
                }
            }
        }
        else
        {
            return token;
        }
    }
}