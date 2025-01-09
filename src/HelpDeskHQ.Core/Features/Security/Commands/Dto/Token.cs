namespace HelpDeskHQ.Core.Features.Security.Commands.Dto
{
    public class Token
    {
        public string Value { get; set; }
        public DateTime Expires { get; set; }
    }
}
