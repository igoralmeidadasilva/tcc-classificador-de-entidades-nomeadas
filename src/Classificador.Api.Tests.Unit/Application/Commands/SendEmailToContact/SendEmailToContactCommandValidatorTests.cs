namespace Classificador.Api.Tests.Unit.Application.Commands.SendEmailToContact;

public sealed class SendEmailToContactCommandValidatorTests
{
    private readonly SendEmailToContactCommandValidator _validator;

    public SendEmailToContactCommandValidatorTests()
    {
        _validator = new SendEmailToContactCommandValidator();
    }

    [Fact]
    public void Validate_NameIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new SendEmailToContactCommand { Name = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorCode(CommandErrors.SendEmailToContact.NameIsRequired.Failure)
            .WithErrorMessage(CommandErrors.SendEmailToContact.NameIsRequired.Description);
    }

    [Fact]
    public void Validate_SubjectIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new SendEmailToContactCommand { Subject = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Subject)
            .WithErrorCode(CommandErrors.SendEmailToContact.SubjectIsRequired.Failure)
            .WithErrorMessage(CommandErrors.SendEmailToContact.SubjectIsRequired.Description);
    }

    [Fact]
    public void Validate_EmailIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new SendEmailToContactCommand { Email = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email)
            .WithErrorCode(CommandErrors.SendEmailToContact.EmailIsRequired.Failure)
            .WithErrorMessage(CommandErrors.SendEmailToContact.EmailIsRequired.Description);
    }

    [Fact]
    public void Validate_EmailIsInvalid_ShouldHaveError()
    {
        // Arrange
        var command = new SendEmailToContactCommand { Email = "invalid-email" };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Email)
            .WithErrorCode(CommandErrors.SendEmailToContact.EmailFormat.Failure)
            .WithErrorMessage(CommandErrors.SendEmailToContact.EmailFormat.Description);
    }

    [Fact]
    public void Validate_MessageIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new SendEmailToContactCommand { Message = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Message)
            .WithErrorCode(CommandErrors.SendEmailToContact.MessageIsRequired.Failure)
            .WithErrorMessage(CommandErrors.SendEmailToContact.MessageIsRequired.Description);
    }

    [Fact]
    public void Validate_AllFieldsAreValid_ShouldNotHaveAnyErrors()
    {
        // Arrange
        var command = new SendEmailToContactCommand
        {
            Name = "Test Name",
            Subject = "Test Subject",
            Email = "test@example.com",
            Message = "Test Message"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

