using System;
using System.Threading;
using System.Threading.Tasks;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Core.Features.Security.Commands.CreateAccount;
using HelpDeskHQ.Domain.Security;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

[TestClass]
public class CreateAccountCommandHandlerTests
{
    private IAccountRepository _mockAccountRepository;
    private ILogger<CreateAccountCommand> _mockLogger;
    private CreateAccountCommandHandler _handler;

    [TestInitialize]
    public void Setup()
    {
        _mockAccountRepository = Substitute.For<IAccountRepository>();
        _mockLogger = Substitute.For<ILogger<CreateAccountCommand>>();
        _handler = new CreateAccountCommandHandler(_mockAccountRepository, _mockLogger);
    }

    [TestMethod]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new CreateAccountCommand
        {
            Username = "testuser",
            Password = "password123"
        };

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(response.Success);
        await _mockAccountRepository.Received(1).Create(Arg.Any<Account>());
    }

    [TestMethod]
    public async Task Handle_InvalidRequest_ReturnsFailureResponse()
    {
        // Arrange
        var command = new CreateAccountCommand
        {
            Username = "", // Invalid username
            Password = "password123"
        };

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsFalse(response.Success);
        StringAssert.Contains(response.Message, "Username' must not be empty.");
        await _mockAccountRepository.DidNotReceive().Create(Arg.Any<Account>());
    }

    [TestMethod]
    public async Task Handle_ExceptionThrown_ReturnsFailureResponse()
    {
        // Arrange
        var command = new CreateAccountCommand
        {
            Username = "testuser",
            Password = "password123"
        };

        _mockAccountRepository.Create(Arg.Any<Account>()).Returns(Task.FromException(new Exception("Database error")));

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsFalse(response.Success);
        Assert.AreEqual("Database error", response.Message);
        _mockLogger.Received(1).LogError(Arg.Any<Exception>(), "Error creating account.");
    }
}