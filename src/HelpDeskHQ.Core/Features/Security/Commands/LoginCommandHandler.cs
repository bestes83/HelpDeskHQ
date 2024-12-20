using AutoMapper;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Core.Helpers;
using HelpDeskHQ.Core.Models;
using HelpDeskHQ.Domain.Security;
using MediatR;

namespace HelpDeskHQ.Core.Features.Security.Commands
{
    public class LoginCommandHandler: IRequestHandler<LoginCommand, Response<AccountVm>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response<AccountVm>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<AccountVm>();
            var account = _accountRepository.GetByUsernamePassword(request.Username, request.Password);

            if (account == null)
            {
                response.Message = "Username and password do not match.";
                return response;
            }

            var vm = _mapper.Map<AccountVm>(account);
            response.Success = true;
            response.Data = vm;

            return response;
        }
    }
}
