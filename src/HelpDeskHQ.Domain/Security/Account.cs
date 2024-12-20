namespace HelpDeskHQ.Domain.Security
{
    public class Account
    {
        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
    }
}
