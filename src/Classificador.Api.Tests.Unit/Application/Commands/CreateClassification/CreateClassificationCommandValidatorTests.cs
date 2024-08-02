namespace Classificador.Api.Tests.Unit.Application.Commands.CreateClassification;

public class CreateClassificationCommandValidatorTests
{
    private readonly CreateClassificationCommandValidator _validator;

    public CreateClassificationCommandValidatorTests()
    {
        _validator = new CreateClassificationCommandValidator();
    }

    [Fact]
    public void Validate_IdUserIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new CreateClassificationCommand { IdUser = Guid.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.IdUser)
            .WithErrorCode(CommandErrors.CreateClassificationFailures.IdUserIsRequired.Failure)
            .WithErrorMessage(CommandErrors.CreateClassificationFailures.IdUserIsRequired.Description);
    }


    [Fact]
    public void Validate_IdNamedEntityIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new CreateClassificationCommand { IdNamedEntity = Guid.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.IdNamedEntity)
            .WithErrorCode(CommandErrors.CreateClassificationFailures.IdNamedEntityIsRequired.Failure)
            .WithErrorMessage(CommandErrors.CreateClassificationFailures.IdNamedEntityIsRequired.Description);
    }


    [Fact]
    public void Validate_IdCategoryIsEmpty_ShouldHaveError()
    {
        // Arrange
        var command = new CreateClassificationCommand { IdCategory = Guid.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.IdCategory)
            .WithErrorCode(CommandErrors.CreateClassificationFailures.IdCategoryIsRequired.Failure)
            .WithErrorMessage(CommandErrors.CreateClassificationFailures.IdCategoryIsRequired.Description);
    }

    [Fact]
    public void Validate_AllFieldsAreValid_ShouldNotHaveAnyErrors()
    {
        // Arrange
        var command = new CreateClassificationCommand
        {
            IdUser = Guid.NewGuid(),
            IdNamedEntity = Guid.NewGuid(),
            IdCategory = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
