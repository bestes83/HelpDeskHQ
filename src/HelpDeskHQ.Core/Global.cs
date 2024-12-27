global using HelpDeskHQ.Domain.Security;
global using HelpDeskHQ.Core.Validators;

using HelpDeskHQ.Core.Features.Security.Commands.CreateAccount;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskHQ.Core
{
    public static class Global
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateAccountCommand>());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            

            return services;
        }
    }
}
