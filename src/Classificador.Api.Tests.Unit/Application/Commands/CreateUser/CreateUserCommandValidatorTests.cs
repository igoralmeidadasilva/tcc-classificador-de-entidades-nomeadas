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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.EmailIsRequired.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.EmailIsRequired.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.EmailMaximumLenght.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.EmailMaximumLenght.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.EmailFormat.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.EmailFormat.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.PasswordIsRequired.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.PasswordIsRequired.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.PasswordMinimumLenght.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.PasswordMinimumLenght.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.PasswordMaximumLenght.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.PasswordMaximumLenght.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidUpperCase.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidUpperCase.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidLowerCase.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidLowerCase.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidNumber.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidNumber.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidNonAlphanumeric.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.PasswordFormatInvalidNonAlphanumeric.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.PasswordsNotEquals.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.PasswordsNotEquals.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.NameIsRequired.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.NameIsRequired.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.NameMaximumLenght.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.NameMaximumLenght.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.ContactMaximumLenght.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.ContactMaximumLenght.Description);
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
            .WithErrorCode(RequestValidationErrors.CreateUserFailures.ContactFormat.Failure)
            .WithErrorMessage(RequestValidationErrors.CreateUserFailures.ContactFormat.Description);
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
            Contact = "(12)34567-8901" 
        };

        // Act 
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Contact);
    }
}

