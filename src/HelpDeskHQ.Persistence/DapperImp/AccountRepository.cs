using HelpDeskHQ.Domain.Security;

namespace HelpDeskHQ.Persistence.DapperImp
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(ISecreteService secreteService): base(secreteService) { }

        public Account? GetByUsernamePassword(string username, string password)
        {
            var sql = "SELECT AccountId, Username FROM Account WHERE Username=@Username && Password=@Password";

            using (var connection = GetConnection())
            {
                var account = connection.QuerySingleOrDefault<Account>(sql, new {Username = username, Password = password});
                return account;
            }
        }

        public async void Create(Account account)
        {
            var sql =
                "INSERT INTO Accounts (AccountId, Username, Password, Salt) VALUES (@AccountId, @Username, @Password, @Salt)";
            using (var connection = GetConnection())
            {
                await connection.ExecuteAsync(sql, account);
            }
        }
    }
}