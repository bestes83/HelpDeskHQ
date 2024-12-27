using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Infrastructure.KeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace HelpDeskHQ.Infrastructure
{
    public static class Global
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfigurationManager config)
        {
            //services.AddSingleton<ISecretService, SecretService>();
            var keyVaultUrl = config.GetSection("AzureKeyVault")["KeyVaultUrl"] ?? String.Empty;

            if(string.IsNullOrEmpty(keyVaultUrl))
                throw new ArgumentNullException("KeyVaultUrl is not configured.", nameof(keyVaultUrl));

            services.AddSingleton<ISecretService>(x => new SecretService(keyVaultUrl));
            return services;
        }
    }
}
