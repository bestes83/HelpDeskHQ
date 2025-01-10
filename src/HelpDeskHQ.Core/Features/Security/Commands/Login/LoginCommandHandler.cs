using AutoMapper;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Core.Extensions;
using HelpDeskHQ.Core.Helpers;
using HelpDeskHQ.Core.Helpers.Config;
using HelpDeskHQ.Core.Models;
using HelpDeskHQ.Domain.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using HelpDeskHQ.Core.Features.Security.Commands.Dto;
using HelpDeskHQ.Core.Helpers.Jwt;
using Microsoft.Extensions.Options;

namespace HelpDeskHQ.Core.Features.Security.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<AccountVm>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginCommand> _logger;
        private readonly IConfiguration _config;

        public LoginCommandHandler(
            IAccountRepository accountRepository,
            IMapper mapper,
            ILogger<LoginCommand> logger,
            IConfiguration config
            )
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
            _config = config;
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

                if (!providedPassword.Equals(account.Password))
                {
                    response.Success = false;
                    response.Message = failedLoginMessage;
                    return response;
                }

                var vm = _mapper.Map<AccountVm>(account);
                vm.Token = GenerateToken(account);
                response.Success = true;
                response.Data = vm;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in.");
            }

            return response;
        }

        private Token GenerateToken(Account account)
        {
            var jwtConfig = _config.GetSection(ConfigHelper.JwtSection).Get<Jwt>();

            if (jwtConfig == null)
                return null;

            var jwtIssuer = jwtConfig.Issuer;
            var jwtAudience = jwtConfig.Audience;
            var jwtKey = jwtConfig.SecreteKey;
            var jwtExpireMinutes = jwtConfig.TokenExpires;
            var tokenExpires = DateTime.UtcNow.AddMinutes(jwtExpireMinutes);

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtClaims.AccountId, account.AccountId.ToString())
                }),
                Expires = tokenExpires,
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)), SecurityAlgorithms.HmacSha256Signature),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenStr = tokenHandler.CreateToken(tokenDescription);
            var stringToken = tokenHandler.WriteToken(tokenStr);

            var token = new Token() { Value = stringToken, Expires = tokenExpires };
            return token;
        }
    }
}
