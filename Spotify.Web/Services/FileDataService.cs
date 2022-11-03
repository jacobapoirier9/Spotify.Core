using NLog;
using ServiceStack;
using Spotify.Web.Services.SpotifyEnhancer;

namespace Spotify.Web.Services;

public class FileDataService : IDataService
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public List<TrackInterval> GetTrackIntervals(string username, string trackId)
    {
        var fileName = $"D:\\MyDev\\Cache\\{username}.{nameof(GetTrackIntervals)}.{trackId}.json";
        var file = new FileInfo(fileName);

        _logger.Debug("Looking for key file {FileName} ({Exists})", file.FullName, file.Exists);

        using (var stream = file.Exists ? file.OpenRead() : file.Create())
        using (var reader = new StreamReader(stream))
        {
            var json = reader.ReadToEnd();
            var intervals = string.IsNullOrEmpty(json) ? new List<TrackInterval>() : json.FromJson<List<TrackInterval>>();
            return intervals;
        }
    }
}
