using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Infrastructure.KeyVault;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskHQ.Infrastructure
{
    public static class Global
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ISecreteService, SecreteService>();
            return services;
        }
    }
}
