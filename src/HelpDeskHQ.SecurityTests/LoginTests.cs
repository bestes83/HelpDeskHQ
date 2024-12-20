using HelpDeskHQ.Core.Features.Security.Commands;

namespace HelpDeskHQ.SecurityTests
{
    [TestClass]
    public sealed class LoginTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var request = new LoginCommand();
            var loginCommandHandler = new LoginCommandHandler();
            var response = await loginCommandHandler.Handle(request, CancellationToken.None);
            //Assert.
        }
    }
}
