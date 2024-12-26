using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HelpDeskHQ.Core.Contracts;

namespace HelpDeskHQ.Infrastructure.KeyVault
{
    public class SecreteService : ISecreteService
    {
        private static string? _connectionString;

        public string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = GetAzureSecrete("helpdeskhq-dev-connectionstring");
            }

            return _connectionString;
        }

        private string GetAzureSecrete(string key)
        {
            var keyVaultUrl = "https://helpdeskhq-dev-keys.vault.azure.net/";
            var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            var secrete = client.GetSecret(key);
            return secrete.Value.Value;
        }
    }
}
