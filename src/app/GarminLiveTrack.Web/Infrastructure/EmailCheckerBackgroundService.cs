using Microsoft.Extensions.Options;

namespace GarminLiveTrack.Web.Infrastructure
{
    public class EmailCheckerBackgroundService : BackgroundService
    {
        private readonly TimeSpan _period;
        private readonly ILogger<EmailCheckerBackgroundService> _logger;

        public EmailCheckerBackgroundService(ILogger<EmailCheckerBackgroundService> logger, IOptions<EmailBackgroundServiceOptions> options)
        {
            _logger = logger;
            _period = TimeSpan.FromMinutes(options.Value.CheckEveryMinute);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Executing PeriodicBackgroundTask");
            }
        }
    }
}
