using System.Diagnostics;
using AutoMapper;
using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Core.Features.Security.Commands.Login;
using HelpDeskHQ.Core.Models;
using HelpDeskHQ.Domain.Security;
using Microsoft.Extensions.Logging;
using NSubstitute;

[TestClass]
public class LoginCommandHandlerTests
{
    private IAccountRepository _accountRepository;
    private IMapper _mapper;
    private ILogger<LoginCommand> _logger;
    private LoginCommandHandler _handler;

    [TestInitialize]
    public void Setup()
    {
        _accountRepository = Substitute.For<IAccountRepository>();
        _logger = Substitute.For<ILogger<LoginCommand>>();
        _mapper = Substitute.For<IMapper>();

        _handler = new LoginCommandHandler(_accountRepository, _mapper, _logger);
        //Debugger.Launch();
    }

    [TestMethod]
    public async Task Handle_ShouldReturnAccountVm_WhenCredentialsAreValid()
    {
        // Arrange
        LoginCommand request = new() { Username = "testuser", Password = "password" };
        Account account = new() { AccountId = 1, Username = "testuser", Password = "vch7nIlNpRaAWeAOv/uQdw==", Salt="1234" };
        AccountVm accountVm = new() { AccountId = 1, Username = "testuser" };

        _accountRepository.GetByUsernamePassword(request.Username, request.Password).Returns(account);
        _mapper.Map<AccountVm>(account).Returns(accountVm);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
        Assert.AreEqual(accountVm.Username, result?.Data?.Username);
    }


    [TestMethod]
    public async Task Handle_ShouldReturnFaildResponse_WhenCredentialsAreNotValid()
    {
        // Arrange
        LoginCommand request = new() { Username = "testuser", Password = "password" };
        Account account = null;
        AccountVm accountVm = new() { Username = "testuser" };

        _accountRepository.GetByUsernamePassword(request.Username, request.Password).Returns(account);
        _mapper.Map<AccountVm>(account).Returns(accountVm);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnFailResponse_WhenPasswordsDoNotMatch()
    {
        // Arrange
        LoginCommand request = new() { Username = "testuser", Password = "password" };
        Account account = new() { Username = "testuser", Password = "1232322", Salt = "1234"};
        AccountVm accountVm = new() { Username = "testuser",};

        _accountRepository.GetByUsernamePassword(request.Username, request.Password).Returns(account);
        _mapper.Map<AccountVm>(account).Returns(accountVm);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success);
    }
}