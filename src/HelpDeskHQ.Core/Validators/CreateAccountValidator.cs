using FluentValidation;
using HelpDeskHQ.Core.Features.Security.Commands.CreateAccount;

namespace HelpDeskHQ.Core.Validators
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
        }
    }
}
