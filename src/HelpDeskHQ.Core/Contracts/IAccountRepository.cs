using HelpDeskHQ.Domain.Security;

namespace HelpDeskHQ.Core.Contracts
{
    public interface IAccountRepository
    {
        Account? GetByUsernamePassword(string username, string password);
    }
}
