using Classificador.Api.Application.Core.Errors;

namespace Classificador.Api.Tests.Unit.Application.Commands.UpdateClassificationToCompleted;

public sealed class UpdateClassificationToCompletedCommandValidatorTests
{
    private readonly UpdateClassificationToCompletedCommandValidator _validator;

    public UpdateClassificationToCompletedCommandValidatorTests()
    {
        _validator = new UpdateClassificationToCompletedCommandValidator();
    }

    [Fact]
    public void ShouldHaveValidationErrorWhenIdPrescribingInformationIsEmpty()
    {
        // Arrange
        var command = new UpdateClassificationToCompletedCommand
        {
            IdPrescribingInformation = Guid.Empty,
            IdUser = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IdPrescribingInformation)
            .WithErrorCode(CommandErrors.UpdateClassificationToCompletedFailures.PrescribingInformationIdIsRequired.Failure)
            .WithErrorMessage(CommandErrors.UpdateClassificationToCompletedFailures.PrescribingInformationIdIsRequired.Description);
    }

    [Fact]
    public void ShouldHaveValidationErrorWhenIdUserIsEmpty()
    {
        // Arrange
        var command = new UpdateClassificationToCompletedCommand
        {
            IdPrescribingInformation = Guid.NewGuid(),
            IdUser = Guid.Empty
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IdUser)
            .WithErrorCode(CommandErrors.UpdateClassificationToCompletedFailures.UserIdIsRequired.Failure)
            .WithErrorMessage(CommandErrors.UpdateClassificationToCompletedFailures.UserIdIsRequired.Description);
    }

    [Fact]
    public void ShouldNotHaveValidationErrorWhenIdsAreValid()
    {
        // Arrange
        var command = new UpdateClassificationToCompletedCommand
        {
            IdPrescribingInformation = Guid.NewGuid(),
            IdUser = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IdPrescribingInformation);
        result.ShouldNotHaveValidationErrorFor(x => x.IdUser);
    }

}
