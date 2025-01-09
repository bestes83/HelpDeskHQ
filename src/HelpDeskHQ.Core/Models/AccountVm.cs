using HelpDeskHQ.Core.Features.Security.Commands.Dto;

namespace HelpDeskHQ.Core.Models
{
    public class AccountVm
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public Token Token { get; set; }
    }
}
