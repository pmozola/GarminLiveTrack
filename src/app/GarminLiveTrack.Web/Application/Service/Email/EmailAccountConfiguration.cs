namespace GarminLiveTrack.Web.Application.Service.Email
{
    public class EmailAccountConfiguration
    {
        public static string EmailAccountConfigurationOptionName = "EmailAccountConfiguration";

        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
