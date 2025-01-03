﻿using HelpDeskHQ.Domain.Security;

namespace HelpDeskHQ.Persistence.DapperImp
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(ISecretService secretService): base(secretService) { }

        public Account? GetByUsernamePassword(string username, string password)
        {
            var sql = "SELECT AccountId, Username FROM Account WHERE Username=@Username";

            using (var connection = GetConnection())
            {
                var account = connection.QuerySingleOrDefault<Account>(sql, new {Username = username, Password = password});
                return account;
            }
        }

        public async Task Create(Account account)
        {
            var sql =
                "INSERT INTO Account (Username, Password, Salt) VALUES (@Username, @Password, @Salt)";

            using (var connection = GetConnection())
            {
                await connection.ExecuteAsync(sql, account);
            }
        }
    }
}