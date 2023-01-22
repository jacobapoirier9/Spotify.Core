using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spotify.Web.Services.SpotifyEnhancer;

namespace Spotify.Web.Services;

public class SqlServerDataService : IDataService
{
    private readonly IDbConnectionFactory _connectionFactory;
    public SqlServerDataService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public List<TrackInterval> GetTrackIntervals(string username, string trackId)
    {
        using (var db = _connectionFactory.Open())
        {
            var intervals = db.Select<TrackInterval>(ti => ti.Username == username && ti.TrackId == trackId);
            return intervals;
        }
    }
}
