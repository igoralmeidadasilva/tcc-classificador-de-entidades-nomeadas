using Classificador.Api.Application.Core.Errors;

namespace Classificador.Api.Tests.Unit.Application.Commands.LoginUser;

public sealed class LoginUserCommandValidatorTests
{
    private readonly LoginUserCommandValidator _validator;

    public LoginUserCommandValidatorTests()
    {
        _validator = new LoginUserCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Email = string.Empty,
            Password = "@ValidPassword123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorCode(CommandErrors.LoginUserFailures.EmailIsRequired.Failure)
            .WithErrorMessage(CommandErrors.LoginUserFailures.EmailIsRequired.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Email = "invalid-email" ,
            Password = "@ValidPassword123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorCode(CommandErrors.LoginUserFailures.EmailFormat.Failure)
            .WithErrorMessage(CommandErrors.LoginUserFailures.EmailFormat.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Password = string.Empty 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.LoginUserFailures.PasswordIsRequired.Failure)
            .WithErrorMessage(CommandErrors.LoginUserFailures.PasswordIsRequired.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Short()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Password = "Short1!"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.LoginUserFailures.PasswordMinimumLenght.Failure)
            .WithErrorMessage(CommandErrors.LoginUserFailures.PasswordMinimumLenght.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Long()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Email = "valid.email@example.com",
            Password = new string('a', Constants.Constraints.USER_PASSWORD_MAX_LENGHT + 1) 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.LoginUserFailures.PasswordMaximumLenght.Failure)
            .WithErrorMessage(CommandErrors.LoginUserFailures.PasswordMaximumLenght.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Has_No_UpperCase()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Password = "password1!"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.LoginUserFailures.PasswordFormatInvalidUpperCase.Failure)
            .WithErrorMessage(CommandErrors.LoginUserFailures.PasswordFormatInvalidUpperCase.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Has_No_LowerCase()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Password = "PASSWORD1!" 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.LoginUserFailures.PasswordFormatInvalidLowerCase.Failure)
            .WithErrorMessage(CommandErrors.LoginUserFailures.PasswordFormatInvalidLowerCase.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Has_No_Digit()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Password = "Password!" 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.LoginUserFailures.PasswordFormatInvalidNumber.Failure)
            .WithErrorMessage(CommandErrors.LoginUserFailures.PasswordFormatInvalidNumber.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Has_No_NonAlphanumeric()
    {
        // Arrange
        var command = new LoginUserCommand 
        { 
            Password = "Password1" 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.LoginUserFailures.PasswordFormatInvalidNonAlphanumeric.Failure)
            .WithErrorMessage(CommandErrors.LoginUserFailures.PasswordFormatInvalidNonAlphanumeric.Description);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_Command()
    {
        // Arrange
        var command = new LoginUserCommand
        {
            Email = "valid.email@example.com",
            Password = "@ValidPassword123"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }
}
