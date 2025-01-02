using AutoMapper;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Core.Extensions;
using HelpDeskHQ.Core.Helpers;
using HelpDeskHQ.Core.Models;
using HelpDeskHQ.Domain.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HelpDeskHQ.Core.Features.Security.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<AccountVm>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginCommand> _logger;

        public LoginCommandHandler(
            IAccountRepository accountRepository, 
            IMapper mapper, 
            ILogger<LoginCommand> logger)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<AccountVm>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var failedLoginMessage = "Username and password do not match.";

            var response = new Response<AccountVm>();
            try
            {
                var account = await _accountRepository.GetByUsernamePassword(request.Username, request.Password);

                if (account == null)
                {
                    response.Message = failedLoginMessage;
                    return response;
                }

                var providedPassword = $"{request.Password}{account.Salt}".ComputeHash();

                if(!providedPassword.Equals(account.Password))
                {
                    response.Success = false;
                    response.Message = failedLoginMessage;
                    return response;
                }

                var vm = _mapper.Map<AccountVm>(account);
                response.Success = true;
                response.Data = vm;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in.");
            }

            return response;
        }
    }
}
