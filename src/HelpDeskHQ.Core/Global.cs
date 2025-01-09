global using HelpDeskHQ.Domain.Security;
global using HelpDeskHQ.Core.Validators;

using HelpDeskHQ.Core.Features.Security.Commands.CreateAccount;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HelpDeskHQ.Core.Helpers.Config;

//using 

namespace HelpDeskHQ.Core
{
    public static class Global
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateAccountCommand>());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddScoped<IConfigurationManager>(s =>
            //{
            //    return configuration;
            //})
            return services;
        }
    }
}
