namespace HelpDeskHQ.Core.Helpers.Config
{
    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecreteKey { get; set; }
    }
}
