namespace GarminLiveTrack.Web.Infrastructure;

public class EmailBackgroundServiceOptions
{
    public static readonly string EmailBackgroundServiceOptionName = "EmailBackgroundService";

    public int CheckEveryMinute { get; set; }
}