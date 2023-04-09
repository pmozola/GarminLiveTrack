using Microsoft.Extensions.Options;
using MailKit.Net.Imap;
using MailKit.Search;

namespace GarminLiveTrack.Web.Application.Service.Email
{
    public class EmailServiceChangeName
    {
        private readonly ILogger<EmailServiceChangeName> _logger;
        private readonly EmailAccountConfiguration _emailAccountConfiguration;

        public EmailServiceChangeName(IOptions<EmailAccountConfiguration> options,
            ILogger<EmailServiceChangeName> logger)
        {
            _logger = logger;
            _emailAccountConfiguration = options.Value;
        }

        public async Task<string> DoSomething(CancellationToken cancellationToken)
        {
            var query = SearchQuery.DeliveredAfter(DateTime.Today.AddDays(-1))
                .And(SearchQuery.FromContains("garmin.com"));
            var orderBy = new[] { OrderBy.ReverseDate };

            using var client = new ImapClient();
            try
            {
                await client.ConnectAsync(_emailAccountConfiguration.SmtpServer, _emailAccountConfiguration.Port, true,
                    cancellationToken: cancellationToken);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailAccountConfiguration.UserName, _emailAccountConfiguration.Password,
                    cancellationToken: cancellationToken);
                var lastGarminEmail =
                    (await client.Inbox.SortAsync(query, orderBy, cancellationToken: cancellationToken))
                    .FirstOrDefault();
                return lastGarminEmail.IsValid
                    ? (await client.Inbox.GetMessageAsync(lastGarminEmail, cancellationToken: cancellationToken))
                    .HtmlBody
                    : string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true, cancellationToken);
                client.Dispose();
            }
        }
    }
}
