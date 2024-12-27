using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HelpDeskHQ.Core.Contracts;
using Microsoft.Extensions.Options;

namespace HelpDeskHQ.Infrastructure.KeyVault
{
    public class SecretService : ISecretService
    {
        //private static string? _connectionString;
        private string _keyVaultUrl = string.Empty;
        private readonly SecretClient _secretClient;

        private static string? _connectionString;

        public SecretService(string keyVaultUrl)
        {
            _keyVaultUrl = keyVaultUrl;

            //var keyVaultUri = new Uri(_keyVaultUrl);
            //var credential = new DefaultAzureCredential();

            //_secretClient = new SecretClient(keyVaultUri, credential);
        }

        public string GetConnectionString()
        {
            //if(string.IsNullOrEmpty(_connectionString))
            //    return GetAzureSecret("helpdeskhq-dev-connectionstring");
            _connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            return _connectionString;
        }

        private string GetAzureSecret(string key)
        {
            var secret = _secretClient.GetSecret(key);
            return secret.Value.Value;
        }
    }
}
