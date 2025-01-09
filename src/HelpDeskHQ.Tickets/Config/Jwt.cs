namespace HelpDeskHQ.Tickets.Config
{
    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecreteKey { get; set; }
        public int TokenExpires { get; set; }
    }
}
