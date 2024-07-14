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
            .WithErrorCode(ValidationErrors.LoginUser.EmailIsRequired.Code)
            .WithErrorMessage(ValidationErrors.LoginUser.EmailIsRequired.Message);
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
            .WithErrorCode(ValidationErrors.LoginUser.EmailFormat.Code)
            .WithErrorMessage(ValidationErrors.LoginUser.EmailFormat.Message);
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
            .WithErrorCode(ValidationErrors.LoginUser.PasswordIsRequired.Code)
            .WithErrorMessage(ValidationErrors.LoginUser.PasswordIsRequired.Message);
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
            .WithErrorCode(ValidationErrors.LoginUser.PasswordMinimumLenght.Code)
            .WithErrorMessage(ValidationErrors.LoginUser.PasswordMinimumLenght.Message);
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
            .WithErrorCode(ValidationErrors.LoginUser.PasswordMaximumLenght.Code)
            .WithErrorMessage(ValidationErrors.LoginUser.PasswordMaximumLenght.Message);
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
            .WithErrorCode(ValidationErrors.LoginUser.PasswordFormatInvalidUpperCase.Code)
            .WithErrorMessage(ValidationErrors.LoginUser.PasswordFormatInvalidUpperCase.Message);
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
            .WithErrorCode(ValidationErrors.LoginUser.PasswordFormatInvalidLowerCase.Code)
            .WithErrorMessage(ValidationErrors.LoginUser.PasswordFormatInvalidLowerCase.Message);
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
            .WithErrorCode(ValidationErrors.LoginUser.PasswordFormatInvalidNumber.Code)
            .WithErrorMessage(ValidationErrors.LoginUser.PasswordFormatInvalidNumber.Message);
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
            .WithErrorCode(ValidationErrors.LoginUser.PasswordFormatInvalidNonAlphanumeric.Code)
            .WithErrorMessage(ValidationErrors.LoginUser.PasswordFormatInvalidNonAlphanumeric.Message);
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
