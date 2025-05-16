namespace EventBooking.Infrastructure.Settings
{
    public class EmailSettings
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}
