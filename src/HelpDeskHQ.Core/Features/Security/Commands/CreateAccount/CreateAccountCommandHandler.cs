using System.Runtime.CompilerServices;
using HelpDeskHQ.Core.Contracts;
using MediatR;

namespace HelpDeskHQ.Core.Features.Security.Commands.CreateAccount
{
    internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
                
        }
    }
}
