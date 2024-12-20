using HelpDeskHQ.Core.Helpers;
using HelpDeskHQ.Core.Models;
using MediatR;

namespace HelpDeskHQ.Core.Features.Security.Commands
{
    public class LoginCommand : IRequest<Response<AccountVm>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
