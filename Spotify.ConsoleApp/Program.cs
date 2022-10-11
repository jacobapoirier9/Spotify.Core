using Spotify.Core;
using Spotify.Core.Model;

namespace Spotify.ConsoleApp;

internal class Program
{
    private const string Token = "BQDR3sSX-Wa3ukD2Om7DxGw85LbI-06PMGpOxaydG_DP7WyWZzuOpKsLN5vrlqewc0BAt25D2jRhihtuTIw03EcdkJskRgGpm_9ec5jW1JrCXW5788cZQX6rIWuqPU94IFNL3tXIQOXRwr81LpfTBKqplaSTR8vv2qWUr1HWsUk3GyRi5u7WpIp6WRpLIMsDEc4SCI_7fb3MP2ws7YAO0sWQSStkkQ11OEXr0dcYUCjfmD4fB9LatzqrDD2G3Z4Rn7O8t6fI2OWXijUGssU4as6xMYC5TkA175fKzlF3GyW-4MTAqJNPAC3UnVyQJX09";

    private static void Main(string[] args)
    {
        var client = new SpotifyClient();

        var tokenClient = new SpotifyTokenClient("9ab0ae26aa664f30b9f901999105b83d", "d3aa4adc651040e3afa59125887bc55f", "https://localhost:5001/Auth/Login");
        var token = tokenClient.CodeForAccessToken("AQB9AvZFP5tTXstIYmKDzfT1pfoaXhKoL4zCm73OoAF8ukb8yjqypExPbjlVs5bMzR0DYMkimFsdN3fGuySdCvrTlHi-xSQFso0NNrbP8CCkEXGLLy8jtL11ghMpXo3cYurvZ_yTaBAuSKeQXFyy-4M13CsnLGnpIp_4EcCYLhwPLcLsbaIMDL0k9ZPOW0aGnEJPxy7q9HqfEHe_0sUEogQXDBVIh2T0ouJuIqNltKaApGjQtixj9dcqJJtdM8TBOgvuIpbwbH0Kwq5AR9d4W06TsUL065z7piI09FKFdkT7ITvGz37VEp1fUhT8TGnCGonuLH1SZWt1bUbkRPmHrFHSONsB4I_4CoqsCMMmAVT4eGfXEfPYY8VspI9E_oxSMfTl6meteM_ICsJHbvdwCfwpE2pVjBXQng_RUFRRACp2vK4I-LlXM5fVTnc__691qMMOm5a0_MV-Z7kcIWa3V2AoWN_D7LsZHggTNs70L-WoKptZRMh-fWxfeHcCQ8_iCaUTex5u2_IX25bwCF-E3Wghp04Jwu3SpLCAs3YBrSyZOPweRIRCY3HrBVi3ZHyLJIOxeIAT4dGBVTwjPsTqvC9BCQF32kJpGcDDtn7lj2xcbOvZyy7BNn35zF-nLpPZcN4pOZUvd8bvZCat9amqB6iF1wabpaYL1j_sjDfojuBelPeefb1DEQ");
    }
}

public static class Helpers
{
    public static List<T> AsList<T>(this T item)
    {
        return new List<T> { item };
    }
}