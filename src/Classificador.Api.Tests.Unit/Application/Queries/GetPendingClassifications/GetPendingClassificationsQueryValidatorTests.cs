using Classificador.Api.Application.Core.Errors;

namespace Classificador.Api.Tests.Unit.Application.Queries.GetPendingClassifications;

public sealed class GetPendingClassificationsQueryValidatorTests
{
    private readonly GetPendingClassificationsQueryValidator _validator;

    public GetPendingClassificationsQueryValidatorTests()
    {
        _validator = new GetPendingClassificationsQueryValidator();
    }

    [Fact]
    public void Validator_IdPrescribingInformationIsEmpty_ShouldHaveError()
    {
        // Arrange
        string idPrescribingInformation = Guid.Empty.ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetPendingClassificationsQuery(idUser, idPrescribingInformation);

        // Act & Assert
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.IdPrescribingInformation)
              .WithErrorCode(QueryErrors.GetPendingClassificationsFailures.PrescribingInformationIdIsRequired.Failure)
              .WithErrorMessage(QueryErrors.GetPendingClassificationsFailures.PrescribingInformationIdIsRequired.Description);
    }

    [Fact]
    public void Validator_IdUserIsEmpty_ShouldHaveError()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.Empty.ToString();

        var query = new GetPendingClassificationsQuery(idUser, idPrescribingInformation);

        // Act & Assert
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.IdUser)
              .WithErrorCode(QueryErrors.GetPendingClassificationsFailures.UserIdIsRequired.Failure)
              .WithErrorMessage(QueryErrors.GetPendingClassificationsFailures.UserIdIsRequired.Description);
    }

    [Fact]
    public void Validator_ValidQuery_ShouldNotHaveErrors()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetPendingClassificationsQuery(idUser, idPrescribingInformation);

        // Act & Assert
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
