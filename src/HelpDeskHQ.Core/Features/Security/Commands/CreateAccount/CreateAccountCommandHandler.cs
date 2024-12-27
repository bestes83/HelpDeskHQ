using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Core.Extensions;
using HelpDeskHQ.Core.Helpers;
using HelpDeskHQ.Domain.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HelpDeskHQ.Core.Features.Security.Commands.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Response<object>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<CreateAccountCommand> _logger;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, ILogger<CreateAccountCommand> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<Response<object>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateAccountValidator();
                var result = await validator.ValidateAsync(request, cancellationToken);

                if (!result.IsValid)
                {
                    var response = new Response<object>()
                    {
                        Success = false,
                        Message = result.ToString("*"),
                    };
                    return response;
                }

                var salt = CreateSalt();
                var account = new Account()
                {
                    Username = request.Username,
                    Password = $"{request.Password}{salt}".ComputeHash(),
                    Salt = salt,
                };

                await _accountRepository.Create(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating account.");
                return new Response<object>() {Success = false, Message = ex.Message};
            }

            return new Response<object>(){Success = true};
        }

        private string CreateSalt()
        {
            var bytes = new byte[6];
            var rgn = RandomNumberGenerator.Create();
            rgn.GetBytes(bytes);
            var salt = Convert.ToBase64String(bytes);
            return salt;
        }
    }
}
