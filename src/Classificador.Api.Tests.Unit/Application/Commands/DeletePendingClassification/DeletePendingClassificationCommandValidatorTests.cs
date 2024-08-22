namespace Classificador.Api.Tests.Unit.Application.Commands.DeletePendingClassification;

public sealed class DeletePendingClassificationCommandValidatorTests
{
    private readonly DeletePendingClassificationCommandValidator _validator;

    public DeletePendingClassificationCommandValidatorTests()
    {
        _validator = new DeletePendingClassificationCommandValidator();
    }

    [Fact]
    public void Should_HaveValidationError_WhenIdClassificationIsEmpty()
    {
        // Arrange
        var command = new DeletePendingClassificationCommand
        {
            IdClassification = Guid.Empty
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IdClassification)
            .WithErrorCode(CommandErrors.DeletePendingClassificationFailures.ClassificationIdIsRequired.Failure)
            .WithErrorMessage(CommandErrors.DeletePendingClassificationFailures.ClassificationIdIsRequired.Description);
    }

    [Fact]
    public void Should_NotHaveValidationError_WhenIdClassificationIsValid()
    {
        // Arrange
        var command = new DeletePendingClassificationCommand
        {
            IdClassification = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IdClassification);
    }
}
