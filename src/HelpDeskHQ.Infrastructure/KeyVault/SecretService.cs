using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HelpDeskHQ.Core.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HelpDeskHQ.Infrastructure.KeyVault
{
    public class SecretService : ISecretService
    {
        //private static string? _connectionString;
        private string _keyVaultUrl;
        private ILogger<SecretService> _logger;
        private readonly SecretClient _secretClient;

        private static string _connectionString;

        public SecretService(string keyVaultUrl, ILogger<SecretService> logger)
        {
            _keyVaultUrl = keyVaultUrl;
            _logger = logger;

            try
            {
                var keyVaultUri = new Uri(_keyVaultUrl);
                var credential = new DefaultAzureCredential();

                _secretClient = new SecretClient(keyVaultUri, credential);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error connecting to the secrete vault");
                throw ex;
            }
        }

        public string GetConnectionString()
        {
            if(string.IsNullOrEmpty(_connectionString))
                return _connectionString = GetAzureSecret("helpdeskhq-dev-connectionstring");

            //_connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            
            return _connectionString;
        }

        private string GetAzureSecret(string key)
        {
            try
            {
                var secret = _secretClient.GetSecret(key);
                return secret.Value.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving secrete. {key}");
            }

            return string.Empty;
        }
    }
}
