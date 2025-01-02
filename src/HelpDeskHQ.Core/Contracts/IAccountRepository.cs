using HelpDeskHQ.Domain.Security;

namespace HelpDeskHQ.Core.Contracts
{
    public interface IAccountRepository
    {
        Task<Account?> GetByUsernamePassword(string username, string password);
        Task Create(Account account);
    }
}
