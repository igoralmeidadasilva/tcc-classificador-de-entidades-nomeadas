namespace Classificador.Api.Tests.Unit.Application.Commands.CreateUser;

public sealed class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _validator;

    public CreateUserCommandValidatorTests()
    {
        _validator = new CreateUserCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Email = string.Empty 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorCode(CommandErrors.CreateUserFailures.EmailIsRequired.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.EmailIsRequired.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Exceeds_Max_Length()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Email = new string('a', Constants.Constraints.USER_EMAIL_MAX_LENGHT + 1) 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorCode(CommandErrors.CreateUserFailures.EmailMaximumLenght.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.EmailMaximumLenght.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid_Format()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Email = "invalidemail" 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorCode(CommandErrors.CreateUserFailures.EmailFormat.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.EmailFormat.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        // Arrange
        var model = new CreateUserCommand 
        {
            Password = string.Empty 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.CreateUserFailures.PasswordIsRequired.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.PasswordIsRequired.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Short()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Password = new string('a', Constants.Constraints.USER_PASSWORD_MIN_LENGHT - 1) 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.CreateUserFailures.PasswordMinimumLenght.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.PasswordMinimumLenght.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Long()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Password = new string('a', Constants.Constraints.USER_PASSWORD_MAX_LENGHT + 1) 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.CreateUserFailures.PasswordMaximumLenght.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.PasswordMaximumLenght.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Does_Not_Contain_Uppercase()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Password = "password1@" 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.CreateUserFailures.PasswordFormatInvalidUpperCase.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.PasswordFormatInvalidUpperCase.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Does_Not_Contain_Lowercase()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Password = "PASSWORD1@" 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.CreateUserFailures.PasswordFormatInvalidLowerCase.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.PasswordFormatInvalidLowerCase.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Does_Not_Contain_Digit()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Password = "Password@" 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.CreateUserFailures.PasswordFormatInvalidNumber.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.PasswordFormatInvalidNumber.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Does_Not_Contain_NonAlphanumeric()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Password = "Password1" 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorCode(CommandErrors.CreateUserFailures.PasswordFormatInvalidNonAlphanumeric.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.PasswordFormatInvalidNonAlphanumeric.Description);
    }

    [Fact]
    public void Should_Have_Error_When_ConfirmPassword_Does_Not_Match_Password()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Password = "Password1@", 
            ConfirmPassword = "DifferentPassword1@" 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword)
            .WithErrorCode(CommandErrors.CreateUserFailures.PasswordsNotEquals.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.PasswordsNotEquals.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Name = string.Empty 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode(CommandErrors.CreateUserFailures.NameIsRequired.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.NameIsRequired.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Exceeds_Max_Length()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Name = new string('a', Constants.Constraints.USER_FIRST_NAME_MAX_LENGHT + 1) 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorCode(CommandErrors.CreateUserFailures.NameMaximumLenght.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.NameMaximumLenght.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Contact_Exceeds_Max_Length()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Contact = new string('a', Constants.Constraints.USER_CONTACT_MAX_LENGHT + 1) 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Contact)
            .WithErrorCode(CommandErrors.CreateUserFailures.ContactMaximumLenght.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.ContactMaximumLenght.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Contact_Is_Invalid_Format()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Contact = "invalidcontact" 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Contact)
            .WithErrorCode(CommandErrors.CreateUserFailures.ContactFormat.Failure)
            .WithErrorMessage(CommandErrors.CreateUserFailures.ContactFormat.Description);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Contact_Is_Empty()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Contact = string.Empty 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Contact);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Contact_Is_Valid()
    {
        // Arrange
        var model = new CreateUserCommand 
        { 
            Contact = "(12) 34567-8901" 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Contact);
    }
}

