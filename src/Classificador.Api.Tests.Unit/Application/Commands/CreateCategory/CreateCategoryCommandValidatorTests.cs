using Classificador.Api.Application.Core.Errors;

namespace Classificador.Api.Tests.Unit.Application.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidatorTests
{
    private readonly CreateCategoryCommandValidator _validator;

    public CreateCategoryCommandValidatorTests()
    {
        _validator = new CreateCategoryCommandValidator();
    }

    [Fact]
    public void Validate_NameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCategoryCommand(string.Empty, string.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorCode(CommandErrors.CreateCategoryFailures.NameIsRequired.Failure)
              .WithErrorMessage(CommandErrors.CreateCategoryFailures.NameIsRequired.Description);
    }

    [Fact]
    public void Validate_NameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateCategoryCommand(new string('a', Constants.Constraints.CATEGORYS_NAME_MAX_LENGHT + 1), string.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorCode(CommandErrors.CreateCategoryFailures.NameMaximumLenght.Failure)
            .WithErrorMessage(CommandErrors.CreateCategoryFailures.NameMaximumLenght.Description);
    }

    [Fact]
    public void Validate_NameIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateCategoryCommand("Valid Category Name", "Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Name);
    }
}