namespace Classificador.Api.Tests.Unit.Application.Commands.UpdateUserRoleToAdmin;

public sealed class UpdateUserRoleToAdminCommandValidatorTests
{
    private readonly UpdateUserRoleToAdminCommandValidator _validator;

    public UpdateUserRoleToAdminCommandValidatorTests()
    {
        _validator = new UpdateUserRoleToAdminCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        // Arrange
        var command = new UpdateUserRoleToAdminCommand 
        { 
            Id = Guid.Empty 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id)
            .WithErrorCode(ValidationErrors.UpdateUserRole.UserIdIsRequired.Code)
            .WithErrorMessage(ValidationErrors.UpdateUserRole.UserIdIsRequired.Message);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Id_Is_Valid()
    {
        // Arrange
        var command = new UpdateUserRoleToAdminCommand 
        { 
            Id = Guid.NewGuid() 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}
