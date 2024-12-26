using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Persistence.DapperImp;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskHQ.Persistence
{
    public static class Global
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            

            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
