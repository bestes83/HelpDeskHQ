using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDeskHQ.Core.Helpers;
using MediatR;

namespace HelpDeskHQ.Core.Features.Security.Commands.CreateAccount
{
    public class CreateAccountCommand : IRequest<Response<object>>
    {
    }
}
