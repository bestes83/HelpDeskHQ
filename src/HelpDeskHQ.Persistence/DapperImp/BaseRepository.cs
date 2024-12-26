using System.Data;
using System.Runtime.CompilerServices;
using HelpDeskHQ.Core.Contracts;
using Microsoft.Data.SqlClient;

namespace HelpDeskHQ.Persistence.DapperImp
{
    public class BaseRepository
    {
        private readonly ISecretService _secretService;

        public BaseRepository(ISecretService secretService)
        {
            _secretService = secretService;
        }

        public virtual IDbConnection GetConnection()
        {
            var connectionString = _secretService.GetConnectionString();
            var dbConnection = new SqlConnection(connectionString);
            return dbConnection;
        }
    }
}
