using System.Text.RegularExpressions;

namespace GarminLiveTrack.Web.Application.Service
{
    public class GarminUrlParser
    {
        private const string UrlPattern = @"(http.*livetrack\.garmin\.com[a-zA-Z0-9\/-]*)";

        public string GetLiveTrackUrl(string message)
        {
            var result = Regex.Match(message, UrlPattern);

            return result.Success ? result.Groups[0].Value : string.Empty;
        }
    }
}
