namespace Classificador.Api.Tests.Unit.Application.Commands.CreateSpecialty;

public sealed class CreateSpecialtyCommandValidatorTests
{
    private readonly CreateSpecialtyCommandValidator _validator;

    public CreateSpecialtyCommandValidatorTests()
    {
        _validator = new CreateSpecialtyCommandValidator();
    }

    [Fact]
    public void Validate_NameIsEmpty_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateSpecialtyCommand(string.Empty, string.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
              .WithErrorCode(CommandErrors.CreateSpecialtyFailures.NameIsRequired.Failure)
              .WithErrorMessage(CommandErrors.CreateSpecialtyFailures.NameIsRequired.Description);
    }

    [Fact]
    public void Validate_NameExceedsMaxLength_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateSpecialtyCommand(new string('a', Constants.Constraints.SPECIALTY_NAME_MAX_LENGHT + 1), string.Empty);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorCode(CommandErrors.CreateSpecialtyFailures.NameMaximumLenght.Failure)
            .WithErrorMessage(CommandErrors.CreateSpecialtyFailures.NameMaximumLenght.Description);
    }

    [Fact]
    public void Validate_NameIsValid_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateSpecialtyCommand("Valid Specialty Name", "Description");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Name);
    }
}