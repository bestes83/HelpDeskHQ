using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Core.Helpers;
using HelpDeskHQ.Domain.Security;
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
                //var temp = result.Errors.Select(x => x.ErrorMessage);
            }

            var salt = CreateSalt();
            var account = new Account()
            {
                Username = request.Username,
                Password = CreateHash($"{request.Password}{salt}"),
                Salt = salt,
            };

            await _accountRepository.Create(account);

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

        private string CreateHash(string password)
        {
            var md5 = MD5.Create();
            var bytes = System.Text.Encoding.ASCII.GetBytes(password);
            var hash = md5.ComputeHash(bytes);
            var base64 = Convert.ToBase64String(hash);
            return base64;
        }
    }
}
