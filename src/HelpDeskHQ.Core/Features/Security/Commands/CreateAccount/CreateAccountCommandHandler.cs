using System.Runtime.CompilerServices;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Core.Helpers;
using MediatR;

namespace HelpDeskHQ.Core.Features.Security.Commands.CreateAccount
{
    internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Response<object>>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Response<object>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

            return new Response<object>();
        }
    }
}
