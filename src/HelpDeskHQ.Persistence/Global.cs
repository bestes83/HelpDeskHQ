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
            var keyVaultUrl = "https://helpdeskhq-dev-keys.vault.azure.net/";
            var key = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            var secrete = key.GetSecret("helpdeskhq-dev-connectionstring");
            var connectionString = secrete.Value.Value;

            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
