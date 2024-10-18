using Classificador.Api.Domain.Core.Errors;

namespace Classificador.Api.Tests.Unit.Application.Commands.SendEmailToContact;


public class SendEmailToContactCommandHandlerTests
{
    private readonly Mock<ILogger<SendEmailToContactCommandHandler>> _loggerMock;
    private readonly Mock<IEmailSenderService> _emailSenderServiceMock;
    private readonly SendEmailToContactCommandHandler _handler;

    public SendEmailToContactCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<SendEmailToContactCommandHandler>>();
        _emailSenderServiceMock = new Mock<IEmailSenderService>();
        _handler = new SendEmailToContactCommandHandler(_loggerMock.Object, _emailSenderServiceMock.Object);
    }

    [Fact]
    public async Task Handle_EmailSendFails_ReturnsFailureResult()
    {
        // Arrange
        var request = new SendEmailToContactCommand
        {
            Email = "test@example.com",
            Name = "Test Name",
            Subject = "Test Subject",
            Message = "Test Message"
        };

        _emailSenderServiceMock
            .Setup(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.EmailSend.FailedToSendEmail, result.Error);
    }

    [Fact]
    public async Task Handle_EmailSendSucceeds_ReturnsSuccessResult()
    {
        // Arrange
        var request = new SendEmailToContactCommand
        {
            Email = "test@example.com",
            Name = "Test Name",
            Subject = "Test Subject",
            Message = "Test Message"
        };

        _emailSenderServiceMock
            .Setup(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
