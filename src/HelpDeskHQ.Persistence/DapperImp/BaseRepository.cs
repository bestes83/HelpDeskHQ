using System.Data;
using System.Runtime.CompilerServices;
using HelpDeskHQ.Core.Contracts;
using Microsoft.Data.SqlClient;

namespace HelpDeskHQ.Persistence.DapperImp
{
    public class BaseRepository
    {
        private readonly ISecreteService _secreteService;

        public BaseRepository(ISecreteService secreteService)
        {
            _secreteService = secreteService;
        }

        public virtual IDbConnection GetConnection()
        {
            var connectionString = _secreteService.GetConnectionString();
            var dbConnection = new SqlConnection(connectionString);
            return dbConnection;
        }
    }
}
