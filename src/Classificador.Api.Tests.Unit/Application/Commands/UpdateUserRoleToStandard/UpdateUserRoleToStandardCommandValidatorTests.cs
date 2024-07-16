namespace Classificador.Api.Tests.Unit.Application.Commands.UpdateUserRoleToStandard;

public sealed class UpdateUserRoleToStandardCommandValidatorTests
{
    private readonly UpdateUserRoleToStandardCommandValidator _validator;

    public UpdateUserRoleToStandardCommandValidatorTests()
    {
        _validator = new UpdateUserRoleToStandardCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        // Arrange
        var command = new UpdateUserRoleToStandardCommand 
        { 
            Id = Guid.Empty 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode(RequestValidationErrors.UpdateUserRoleFailures.UserIdIsRequired.Failure)
            .WithErrorMessage(RequestValidationErrors.UpdateUserRoleFailures.UserIdIsRequired.Description);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Id_Is_Valid()
    {
        // Arrange
        var command = new UpdateUserRoleToStandardCommand 
        { 
            Id = Guid.NewGuid() 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}
