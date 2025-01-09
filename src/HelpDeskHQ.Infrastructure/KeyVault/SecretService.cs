using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Core.Helpers.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HelpDeskHQ.Infrastructure.KeyVault
{
    public class SecretService : ISecretService
    {
        //private static string? _connectionString;
        private string _keyVaultUrl;
        private ILogger<SecretService> _logger;
        //private readonly SecretClient _secretClient;
        private readonly IConfigurationManager _config;

        private static string _connectionString;

        public SecretService(string keyVaultUrl, ILogger<SecretService> logger, IConfigurationManager config)
        {
            _keyVaultUrl = keyVaultUrl;
            _logger = logger;
            _config = config;

            //try
            //{
            //    var keyVaultUri = new Uri(_keyVaultUrl);
            //    var credential = new DefaultAzureCredential();

            //    _secretClient = new SecretClient(keyVaultUri, credential);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error connecting to the secrete vault");
            //    throw;
            //}
        }

        public string GetConnectionString()
        {
            //if(string.IsNullOrEmpty(_connectionString))
            //    return _connectionString = GetAzureSecret("helpdeskhq-dev-connectionstring");

            _connectionString = Environment.GetEnvironmentVariable(ConfigHelper.ConnectionString) ?? _config.GetConnectionString(ConfigHelper.ConnectionString);

            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentException("Connection String is not defined.");

            return _connectionString;
        }

        //private string GetAzureSecret(string key)
        //{
        //    try
        //    {
        //        var secret = _secretClient.GetSecret(key);
        //        return secret.Value.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error retrieving secrete. {key}");
        //    }

        //    return string.Empty;
        //}
    }
}
