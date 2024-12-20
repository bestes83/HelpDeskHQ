using MediatR;

namespace HelpDeskHQ.Core.Features.Security.Commands
{
    public class LoginCommandHandler: IRequestHandler<LoginCommand>
    {

        public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
