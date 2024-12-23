using Dapper;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Domain.Security;
using Microsoft.Data.SqlClient;

namespace HelpDeskHQ.Persistence.DapperImp
{
    public class AccountRepository : IAccountRepository
    {
        public Account? GetByUsernamePassword(string username, string password)
        {
            var sql = "SELECT AccountId, Username FROM Account WHERE Username=@Username && Password=@Password";

            using (var connection = new SqlConnection(sql))
            {
                var account = connection.QuerySingleOrDefault<Account>(sql, new {Username = username, Password = password});
                return account;
            }
        }
    }
}